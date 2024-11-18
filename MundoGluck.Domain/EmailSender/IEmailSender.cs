using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoGluck.Domain.EmailSender;
public interface IEmailSender
{
    Task<System.Net.HttpStatusCode> SendEmailAsync(string subject, string message, string toEmail);
}
