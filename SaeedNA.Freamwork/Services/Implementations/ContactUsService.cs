using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Contact;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Contact;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class ContactUsService : IContactUsService
    {
        private readonly IGenericRepository<ContactUs> _contactUsRepository;

        public ContactUsService(IGenericRepository<ContactUs> contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _contactUsRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddContactUs(ContactUsCreateDTO contactUs)
        {
            try
            {
                var entity = new ContactUs
                {
                    FullName = contactUs.FullName,
                    Email = contactUs.Email,
                    Mobile = contactUs.Mobile,
                    Subject = contactUs.Subject,
                    Text = contactUs.Text
                };

                var result = await _contactUsRepository.AddEntity(entity);
                await _contactUsRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ContactUsFilterDTO> FilterContactUs(ContactUsFilterDTO filter)
        {
            var query = _contactUsRepository.GetQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.FullName))
                query = query.Where(s => EF.Functions.Like(s.FullName, $"%{filter.FullName}%"));
            if(!string.IsNullOrEmpty(filter.Email))
                query = query.Where(s => EF.Functions.Like(s.Email, $"%{filter.Email}%"));
            if(!string.IsNullOrEmpty(filter.Mobile))
                query = query.Where(s => EF.Functions.Like(s.Mobile, $"%{filter.Mobile}%"));

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity,filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetContactUs(allEntities).SetPaging(pager);
        }
    }
}