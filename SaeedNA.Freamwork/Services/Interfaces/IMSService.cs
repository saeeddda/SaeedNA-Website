using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.MService;

namespace SaeedNA.Service.Interfaces
{
    public interface IMSService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewService(MyServiceCreateDTO myService);
        Task<ServiceResult> EditService(MyServiceEditDTO myService);
        Task<ServiceResult> DeleteService(long myServiceId);
        Task<MyServiceEditDTO> GetMyServiceById(long myServiceId);
        Task<MyServiceFilterDTO> FilterService(MyServiceFilterDTO filter);
    }
}
