using Microsoft.Extensions.Configuration;
using SaeedNA.Service.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class EmailSender:IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string subject, string text, string toEmail, string fromEmail, string host, int port, string userName, string password)
        {
            try
            {
                using(var client = new SmtpClient())
                {
                    var credential = new NetworkCredential()
                    {
                        UserName = userName,
                        Password = password
                    };

                    client.Credentials = credential;
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;

                    using var emailMessage = new MailMessage()
                    {
                        To = { new MailAddress(toEmail) },
                        From = new MailAddress(fromEmail),
                        Subject = subject,
                        Body = text,
                        IsBodyHtml = true
                    };

                    await client.SendMailAsync(emailMessage);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}