using SaeedNA.Domain.Models.Email;
using System.Threading.Tasks;

namespace SaeedNA.Framework.Email
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(EmailAuth auth, EmailProvider provider, EmailContent content,bool ssl=false);
    }
}
