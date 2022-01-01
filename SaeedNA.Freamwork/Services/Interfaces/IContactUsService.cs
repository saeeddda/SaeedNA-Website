using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Contact;
using SaeedNA.Data.Entities.Contact;

namespace SaeedNA.Service.Interfaces
{
    public interface IContactUsService:IAsyncDisposable
    {
        Task<ServiceResult> AddContactUs(ContactUsCreateDTO contactUs);
        Task<ContactUsFilterDTO> FilterContactUs(ContactUsFilterDTO filter);
        Task<ContactUs> GetContactUs(long id);
    }
}