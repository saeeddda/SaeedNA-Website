using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.MService;

namespace SaeedNA.Service.Interfaces
{
    public interface ICounterService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewCounter(CounterCreateDTO counter);
        Task<ServiceResult> EditCounter(CounterEditDTO counter);
        Task<ServiceResult> DeleteCounter(long counterId);
        Task<CounterEditDTO> GetCounterById(long counterId);
        Task<CounterFilterDTO> FilterCounter(CounterFilterDTO filter);
    }
}
