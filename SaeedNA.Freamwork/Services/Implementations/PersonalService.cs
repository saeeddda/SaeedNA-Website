using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Models.Settings;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class PersonalService:IPersonalService
    {
        public readonly IGenericRepository<PersonalInfo> _personalInfoRepository;

        public PersonalService(IGenericRepository<PersonalInfo> personalInfoRepository)
        {
            _personalInfoRepository = personalInfoRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _personalInfoRepository.DisposeAsync();
        }

        public async Task<PersonalInfo> GetDefaultInfo()
        {
            return await _personalInfoRepository.GetQuery().SingleOrDefaultAsync(s => s.IsDefault);
        }
    }
}