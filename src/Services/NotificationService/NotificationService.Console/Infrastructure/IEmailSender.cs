using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Console.Infrastructure
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string subject, string message);
    }
}
