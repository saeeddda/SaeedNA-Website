using System.Threading.Tasks;
using SaeedNA.Data.Entities.Contact;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class ContactUsService:IContactUsService
    {
        public readonly IGenericRepository<ContactUs> _contactUsRepository;

        public ContactUsService(IGenericRepository<ContactUs> contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }
    }
}