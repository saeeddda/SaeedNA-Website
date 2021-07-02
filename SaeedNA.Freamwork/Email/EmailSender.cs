using SaeedNA.Domain.Models.Email;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SaeedNA.Framework.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(EmailAuth auth,EmailProvider provider,EmailContent content,bool ssl=false)
        {
            using(var client = new SmtpClient())
            {
                var crediental = new NetworkCredential()
                {
                    UserName = auth.UserName,
                    Password = auth.Password
                };

                client.Credentials = crediental;
                client.Host = provider.HostName;
                client.Port = int.Parse(provider.HostPort);
                client.EnableSsl = ssl;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(content.ToEmail) },
                    From = new MailAddress(provider.Sender),
                    Subject = content.Subject,
                    Body = content.Message,
                    IsBodyHtml = content.IsHtml
                };

                client.Send(emailMessage);
            }

            return Task.CompletedTask;
        }
    }
}
