using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDating.Models
{
    public interface ISessionOperations
    {
        DatingSession StartSession(int MansCount, int GirlsCount);
        object GetResults(int sessionId);
        DatingSession GetSessionById(int id);
        string GetRoot();
    }

    public class SessionFactory : ISessionOperations
    {       
        public string rootFolder { get; set; }
        private IDatingSettings settings { get; set; }

        public SessionFactory(IDatingSettings settings)
        {
            this.rootFolder = Directory.GetCurrentDirectory() + "//DAL//Sessions//";
            this.settings = settings;
        }

        /// <summary>
        /// Load Speed Dating Session
        /// </summary>
        /// <returns></returns>
        public object GetResults(int sessionId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Create new Speed Dating Session
        /// </summary>
        /// <returns></returns>
        public DatingSession StartSession(int MansCount, int GirlsCount)
        {
            var session = new DatingSession
            {
                SessionID = GetlatestId() + 1,
                Dt = DateTime.Now,
                PersonalBlancs = new List<PersonalBlanc>(),
                MansCount = MansCount,
                GirlsCount = GirlsCount
            };

            return session;
        }

        public int GetlatestId()
        {
            int latestId = 0;

            string[] subFolders = Directory.GetDirectories(rootFolder);


            if (subFolders.Length > 0)
            {
                var sessionsList = subFolders.Select(f => f.Split("/").Last()).ToList();
                var idList = sessionsList.Select(f => f.Split("_").Last()).OrderBy(f => f).ToList();
                latestId = idList.Max(f => Convert.ToInt32(f));
            }

            CreateFolder(latestId + 1);

            return latestId;
        }

        public DatingSession GetSessionById(int id)
        {
            string[] subFolders = Directory.GetDirectories(rootFolder);

            foreach(var folder in subFolders)
            {
                if (folder.Contains($"Session_{id}"))
                {
                    string[] filePaths = Directory.GetFiles(folder, "*.json");

                    //1 session = 1 json file
                    var text = File.ReadAllText(filePaths.First());

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<DatingSession>(text);
                }
            }

            return null;
        }

        private void CreateFolder(int id)
        {            
            rootFolder = $"{rootFolder}//Session_{id}";
            Directory.CreateDirectory(rootFolder);
        }

        public string GetRoot()
        {
            return rootFolder;
        }
    }
}
