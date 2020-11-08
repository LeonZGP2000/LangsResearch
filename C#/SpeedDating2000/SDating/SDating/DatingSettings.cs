using System;
using System.IO;

namespace SDating
{
    public interface IDatingSettings
    {
        int GetNewSessionNumber();
        SmtpSetting GetSMTPSettings();
    }

    public class DatingSettings : IDatingSettings
    {
        public string smtpSettingsName = "smtpSettings.json";

        public int GetNewSessionNumber()
        {
            //где-то уже сделано
            throw new NotImplementedException();
        }

        public SmtpSetting GetSMTPSettings()
        {
            if (!File.Exists(smtpSettingsName)) return new SmtpSetting();

            var settings = Newtonsoft.Json.JsonConvert.DeserializeObject<SmtpSetting>(File.ReadAllText(smtpSettingsName));
            return settings ?? new SmtpSetting();
        }
    }

    /// <summary>
    /// SMTP setting for bulk emails
    /// </summary>
    public class SmtpSetting
    {
        public string host { get; set; }
        public int Port { get; set; }
        public string MailBoxLogin { get; set; }
        public string MailBoxPassword { get; set; } 
        public string From { get; set; }
        public string EmailSubjectpatterntBoys { get; set; }
        public string EmailSubjectpatterntGirls { get; set; }

        public SmtpSetting()
        {
            host = "smtp.gmail.com";
            Port = 587;
            MailBoxLogin = "login";
            MailBoxPassword = "password";
            From = "speeddating@gmail.com";
            EmailSubjectpatterntBoys = $"Быстрые свидания, результаты. @DATE@ Удачи!"; ;
            EmailSubjectpatterntGirls = $"Привет, красавица! Рзультаты быстрых свидани!!! @DATE@ Удачи!";
        }
    }
}
