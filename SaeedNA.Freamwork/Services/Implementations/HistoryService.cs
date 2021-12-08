using SaeedNA.Data.Models.Resume;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class HistoryService : IHistoryService
    {
        public readonly IGenericRepository<History> _HistoryRepository;

        public HistoryService(IGenericRepository<History> historyRepository)
        {
            _HistoryRepository = historyRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _HistoryRepository.DisposeAsync();
        }
    }
}
