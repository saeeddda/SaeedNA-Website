using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Contact;

namespace SaeedNA.Service.Interfaces
{
    public interface IContactUsService:IAsyncDisposable
    {
        Task<ServiceResult> AddContactUs(ContactUsCreateDTO contactUs);
        Task<ContactUsFilterDTO> FilterContactUs(ContactUsFilterDTO filter);
    }
}