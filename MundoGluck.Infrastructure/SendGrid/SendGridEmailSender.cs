using MundoGluck.Domain.EmailSender;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MundoGluck.Infrastructure.SendGrid;
public class SendGridEmailSender : IEmailSender
{
    public async Task<System.Net.HttpStatusCode> SendEmailAsync(string subject, string message, string toEmail)
    {
        var apiKey = "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";
        var fromEmail = "jpazuredemo@digitalplusplus.com";
        
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        return response.StatusCode;
    }
}

    
