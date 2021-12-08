using System.Threading.Tasks;
using SaeedNA.Data.Entities.Authentication;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class UserService:IUserService
    {
        public readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }
    }
}