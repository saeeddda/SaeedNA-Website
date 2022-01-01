using System;
using System.Threading.Tasks;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Resume;

namespace SaeedNA.Service.Interfaces
{
    public interface IHistoryService:IAsyncDisposable
    {
        Task<ServiceResult> AddNewHistory(HistoryCreateDTO history);
        Task<ServiceResult> EditHistory(HistoryEditDTO history);
        Task<ServiceResult> DeleteHistory(long historyId);
        Task<HistoryEditDTO> GetHistoryById(long historyId);
        Task<HistoryFilterDTO> FilterHistory(HistoryFilterDTO filter);
    }
}
