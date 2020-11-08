using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using file = System.IO.File;

namespace SDating.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IStorageAnalyzer stAnalyser;
        private  IDatingSettings settngs = new DatingSettings();
        private  IMessageSender messageSender;

        public SettingsController(IWebHostEnvironment hostingEnvironment)
        {
            stAnalyser = new StorageAnalyzer(hostingEnvironment);
        }

        [HttpGet]
        public IActionResult Init()
        {
            return View(settngs.GetSMTPSettings());
        }

        [HttpPost]
        public IActionResult Save(SmtpSetting settings)
        {
            var name = "smtpSettings.json";

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(settings);

            if (!file.Exists(name))
                file.Create(name).Close();

            file.WriteAllText(name, json);

            return RedirectToAction("index", "dating");
        }

        [HttpPost]
        public IActionResult Test(string testReceiverEmail)
        {
            var testSubject = ">>> Проверка SMTP настроек (БС)";

            try
            {
                var settings = settngs.GetSMTPSettings();
                messageSender = new MessageSender(settings);

                messageSender.SendResults(new System.Net.Mail.MailAddress(settings.From)
                    , testSubject, $" <b> Проверка SMTP <br/><br/>URL:{this.Url}<br/> Sender:{this.User.Identity.Name} </b>");

                ViewData["tmg"] = $"Сообщение отправлено получателю  {testReceiverEmail}";
            }
            catch (System.Exception ex)
            {
                ViewData["tmg"] = $"Ошибка: {ex.Message}";
            }

            return View("Init", settngs.GetSMTPSettings());
        }

        [HttpGet]
        public IActionResult Stat()
        {
            var reportSessions = stAnalyser.GetReportOFSessions();
            var reportImg = stAnalyser.GetReportOFImages();

            ViewData["statmsg"] = "1";
            @ViewData["reportSessions"] = reportSessions;
            @ViewData["reportImg"] = reportImg;
            
            //not partial view
            return View("Init", settngs.GetSMTPSettings());
        }
    }
}
