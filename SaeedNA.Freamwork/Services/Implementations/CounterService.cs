using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;
using SaeedNA.Data.Models.MService;
using SaeedNA.Domain.Repository;

namespace SaeedNA.Service.Implementations
{
    public class CounterService : ICounterService
    {
        public readonly IGenericRepository<Counter> _counterRepository;

        public CounterService(IGenericRepository<Counter> counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _counterRepository.DisposeAsync();
        }
    }
}
