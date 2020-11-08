using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDating.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SDating.Controllers
{
    [Authorize]
    public class DatingController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private ISessionStorage sessionStorage = new SessionStorage();
        private DatingSettings ds = new DatingSettings();
        private ISessionOperations sessionFactory;
        private IMatchAnalyser matchAnalyser = new MatchAnalyser();
        private IMessageSender messageSender;
        private IPersonalResultToHtmlParsert resultParser = new PersonalResultToHtmlParser();
        public DatingController(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
            sessionFactory = new SessionFactory(ds);
            messageSender = new MessageSender(ds.GetSMTPSettings());
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            //check auth
            if (!User.Identity.IsAuthenticated)
            {
                //redirect to auth
                return RedirectToAction("index", "auth");
            }

            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(int MansCount, int GirlsCount)
        {
            //if (MansCount <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(MansCount));
            //if (GirlsCount <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(GirlsCount));

            var session = sessionFactory.StartSession(MansCount, GirlsCount);

            sessionStorage.SaveInSession(HttpContext, session);

            return View("Session", session);
        }

        /// <summary>
        /// Add new blank to the Session
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int tableId, string name, string sex, string foto, string email, string phone, int age)
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            bool alreadyExists = session.PersonalBlancs.Any(f => f.Name == name && f.TableId == tableId);

            if (!alreadyExists)
            {
                session.PersonalBlancs.Add(
                    new PersonalBlanc
                    {
                        TableId = tableId,
                        Name = name,
                        isMan = (sex == "Boy"),
                        Picture = foto,
                        Email = email,
                        Phone = phone,
                        Age = age
                    });

                sessionStorage.SaveInSession(HttpContext, session);
            }
            return View("Session", session);
        }

        /// <summary>
        /// Upload foto in profile
        /// </summary>
        [HttpGet]
        public IActionResult Foto(string name, string tableId, string sessionId)
        {
            ViewBag.name = name;
            ViewBag.tableId = tableId;
            ViewBag.sessionId = sessionId;

            return View();
        }


        [HttpPost]
        public IActionResult Foto(string name, int tableId, int sessionId)
        {
            var uploadFile = Request.Form.Files.FirstOrDefault();
            if (uploadFile == null)
                RedirectToAction("Foto", "Dating", new { name = name, tableId = tableId, sessionId = sessionId });

            //var pathToSave = $"{Directory.GetCurrentDirectory().Replace("\\", "/")}/DAL/Sessions/Session_{sessionId}";
            //var filePath = $"{pathToSave}/{name}_{tableId}_{((FormFile)uploadFile).FileName.Split("\\").Last()}";

            //save in www root
            var uniqueFileName = $"{name}_{tableId}_{((FormFile)uploadFile).FileName.Split("\\").Last()}";
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img", $"Session_{sessionId}");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, uniqueFileName);


            if (!System.IO.File.Exists(filePath))
                System.IO.File.Create(filePath).Close();

            using (var stream = System.IO.File.Create(filePath))
            {
                uploadFile.CopyTo(stream);
            }

            var session = sessionStorage.LoadFromSession(HttpContext);

            //update person's blanc
            var person = session.PersonalBlancs.Where(b => b.Name == name && b.TableId == tableId).FirstOrDefault();
            if (person == null)
                throw new Exception($"Person {name} with Table number {tableId} does not exists");

            //img URL for pictures
            var index = filePath.IndexOf("img") - 1;
            person.Picture = filePath.Substring(index);
            person.PictureFullPath = filePath;

            sessionStorage.SaveInSession(HttpContext, session);

            return View("Session", session);
        }

        /// <summary>
        /// Show uploaded foto (from wwwroot/img)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OpenFoto(string url)
        {
            int indexStart = url.IndexOf("img");

            ViewBag.url = $"/{url.Substring(indexStart)}";

            return View();
        }

        [HttpPost]
        public IActionResult OpenFoto()
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            return View("Session", session);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            //save+
            var path = sessionFactory.GetRoot();
            var fileName = $"{session.Dt.ToString("yyyy-MM-dd")}_{session.SessionID}.json";
            var fullPath = System.IO.Path.Combine(path, $"Session_{session.SessionID}", fileName);

            var str = Newtonsoft.Json.JsonConvert.SerializeObject(session);

            System.IO.File.WriteAllText(fullPath, str);

            ViewBag.SaveMessage = $"Session saved in file {fullPath}";
            //save-


            return View("Session", session);
        }

        [HttpGet]
        public IActionResult Load()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Load(int id)
        {
            var session = sessionFactory.GetSessionById(id);

            sessionStorage.SaveInSession(HttpContext, session);

            return View("Session", session);
        }


        /* Matchings */
        [HttpGet]
        public IActionResult Matching(string user, string tableId, bool isMale)
        {
            ViewData["UserHost"] = $"{user}, стол № {tableId}";
            ViewData["name"] = user;
            ViewData["tableId"] = tableId;
            ViewData["isMale"] = isMale;

            var session = sessionStorage.LoadFromSession(HttpContext);

            var persons = session.PersonalBlancs
                .Where(a => a.isMan != isMale)
                .OrderBy(a => a.TableId)
                .ToList();

            return View(persons);
        }

        [HttpPost]
        public IActionResult SaveMatching(string Name, int TableId, string[] tags, bool isMale)
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            var hostPerson = session.PersonalBlancs
                .Where(p => p.isMan == isMale && p.TableId == TableId)
                .First();

            hostPerson.PersonalChoose = new System.Collections.Generic.List<PersonalBlanc>();

            var contrPersons = session.PersonalBlancs.Where(p => p.isMan != isMale);

            //save changes
            foreach (var selectedTableId in Request.Form["tags"])
            {
                var contrId = Convert.ToInt32(selectedTableId);
                var foundPerson = contrPersons.Where(p => p.TableId == contrId).First();
                
                hostPerson.PersonalChoose.Add(foundPerson);
            }

            //save in session
            sessionStorage.SaveInSession(HttpContext, session);

            return View("Session", session);
        }

        /* Results */
        [HttpGet]
        public IActionResult Results()
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            var sessionResult = matchAnalyser.GetMatchingResult(session);
            
            return View(sessionResult);
        }

        /* Remove person */
        [HttpGet]
        public IActionResult Remove(string name, int tableId, int sessionId)
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            //remove
            var person = session.PersonalBlancs.Single(p => p.Name == name && p.TableId == tableId);
            
            //remove picture
            if (!String.IsNullOrEmpty(person.PictureFullPath))
            {
                System.IO.File.Delete(person.PictureFullPath);
            }

            session.PersonalBlancs.Remove(person);

            //save in session
            sessionStorage.SaveInSession(HttpContext, session);

            return View("Session", session);
        }

        /* Send results via Email */
        [HttpGet]
        public IActionResult SendResultsViaEmail()
        {
            var session = sessionStorage.LoadFromSession(HttpContext);

            //dublication
            var result = matchAnalyser.GetMatchingResult(session);

            var persons = new List<PersonalResult>();
            persons.AddRange(result.Boys);
            persons.AddRange(result.Girls);

            //Customize emails ???

            //Check SMTP Settings ??

            foreach (PersonalResult boy in persons)
            {
                var subject =$"Быстрые всвидания. Совпадени [{session.Dt.ToString("yyyy.MM.dd")}] Удачи!";
                messageSender.SendResults( new System.Net.Mail.MailAddress(boy.Email), 
                    subject, resultParser.ToHTML(boy));
            }

            foreach (PersonalResult girl in result.Girls)
            {
                var subject = $"Привет, красавица! Рзультаты быстрых свидани!!! [{session.Dt.ToString("yyyy.MM.dd")}] Удачи!";
                messageSender.SendResults(new System.Net.Mail.MailAddress(girl.Email), 
                    subject, resultParser.ToHTML(girl));
            }

            return RedirectToAction("Results");
        }
    }
}
