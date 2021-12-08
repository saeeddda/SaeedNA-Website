using SaeedNA.Data.Models.MService;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class MyServiceService : IMSService
    {
        public readonly IGenericRepository<MyService> _myServiceRepository;

        public MyServiceService(IGenericRepository<MyService> myServiceRepository)
        {
            _myServiceRepository = myServiceRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _myServiceRepository.DisposeAsync();
        }
    }
}
