using SaeedNA.Domain.Models.Email;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface IEmail
    {
        void AddEmail(Email email);
        ICollection<Email> GetAllEmail();
        Email GetEmailById(int id);
    }
}
