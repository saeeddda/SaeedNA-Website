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

        public async Task<bool> SendEmail(string subject,  string text, string email)
        {
            try
            {
                using(var client = new SmtpClient())
                {
                    var credential = new NetworkCredential()
                    {
                        UserName = _configuration.GetSection("MailServer")["Username"],
                        Password = _configuration.GetSection("MailServer")["Password"]
                    };

                    client.Credentials = credential;
                    client.Host = _configuration.GetSection("MailServer")["Server"];
                    client.Port = int.Parse(_configuration.GetSection("MailServer")["Port"]);
                    client.EnableSsl = true;

                    using var emailMessage = new MailMessage()
                    {
                        To = { new MailAddress(email) },
                        From = new MailAddress(_configuration.GetSection("MailServer")["Email"]),
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