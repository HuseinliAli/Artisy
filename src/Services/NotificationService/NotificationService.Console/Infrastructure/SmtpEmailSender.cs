using System.Net.Mail;
using System.Net;

namespace NotificationService.Console.Infrastructure
{
    public class SmtpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential("test@gmail.com", "password"),
                EnableSsl = true
            };
            return client.SendMailAsync(
                new MailMessage("test@gmail.com", "test@gmail.com", subject, message) { IsBodyHtml = true });
        }
    }
}
