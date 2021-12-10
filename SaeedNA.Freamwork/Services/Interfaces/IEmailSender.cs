using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string subject, string text, string email);
    }
}