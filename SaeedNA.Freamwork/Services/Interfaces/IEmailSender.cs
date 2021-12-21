using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string subject, string text, string toEmail,string fromEmail,string host, int port, string userName, string password);
    }
}