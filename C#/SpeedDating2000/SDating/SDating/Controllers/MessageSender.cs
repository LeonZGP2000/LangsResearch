using System;
using System.Net;
using System.Net.Mail;

namespace SDating.Controllers
{
    public interface IMessageSender
    {
        public bool SendResults(MailAddress recipient, string subject, string body);
    }

    public class MessageSender : IMessageSender
    {
        private SmtpSetting settings { get; set; }
        public MessageSender(SmtpSetting settings)
        {
            this.settings = settings;
        }
        public bool SendResults(MailAddress recipient, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(settings.From);
            message.To.Add(recipient);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            smtp.Port = settings.Port;
            smtp.Host = settings.host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(settings.MailBoxLogin, settings.MailBoxPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

            return true;
        }
    }
}
