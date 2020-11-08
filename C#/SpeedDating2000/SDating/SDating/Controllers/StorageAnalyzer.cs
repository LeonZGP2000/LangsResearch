using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SDating.Controllers
{
    public interface IStorageAnalyzer
    {
        /// <summary>
        /// IMG Report
        /// </summary>
        string GetReportOFSessions();

        /// <summary>
        /// Sessions Reort
        /// </summary>
        string GetReportOFImages();
    }

    public class StorageAnalyzer : IStorageAnalyzer
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public StorageAnalyzer(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string GetReportOFImages()
        {
            var report = default(string);
            var root = Path.Combine(hostingEnvironment.WebRootPath, "img");
            report = $"Root folder:\n{root}\n\n";
            AnalyzeFolder(root, ref report);

            report += "=====================================================";

            return report;
        }

        public string GetReportOFSessions()
        {
            var report = default(string);
            var root = Directory.GetCurrentDirectory() + "//DAL//Sessions//";
            report = $"Root folder:\n{root}\n\n";
            AnalyzeFolder(root, ref report);

            report += "=====================================================";

            return report;
        }

        private string AnalyzeFolder(string root, ref string report)
        {           
            var subfolders = Directory.GetDirectories(root);

            foreach (var s in subfolders)
            {
                //only session folders
                if (!s.Contains("Session")) continue;

                var di = new DirectoryInfo(s);
                report += $" -> {di.Name} [Created at: {di.CreationTime}]\n";

                var files = di.GetFiles();

                foreach (var fi in files)
                {
                    report += $" ---> {fi.Name} [ Size: {fi.Length / 1024} Kb.]\n";
                }                
            }

            return report;
        }
    }
}
