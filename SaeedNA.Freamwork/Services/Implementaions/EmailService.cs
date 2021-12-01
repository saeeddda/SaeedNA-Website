using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.Email;
using SaeedNA.Service.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class EmailService : IEmail
    {
        private readonly SaeedNAContext _context;

        public EmailService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddEmail(Email email)
        {
            _context.Emails.Add(email);
            _context.SaveChanges();
        }

        public ICollection<Email> GetAllEmail()
        {
            return _context.Emails.ToList();
        }

        public Email GetEmailById(int id)
        {
            return _context.Emails.FirstOrDefault(e => e.EmailId == id);
        }
    }
}
