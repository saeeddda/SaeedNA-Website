using SaeedNA.Data.DTOs.Account;
using SaeedNA.Data.DTOs.Common;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SaeedNA.Data.Entities.Account;

namespace SaeedNA.Service.Interfaces
{
    public interface IUserService:IAsyncDisposable
    {
        Task<ServiceResult> RegisterUser(RegisterUserDTO user);
        Task<ServiceResult> GetUserForLogin(LoginUserDTO user);
        Task<ServiceResult> ForgotPassword(ForgotUserDTO forgot, IConfiguration configuration);
        Task<ServiceResult> IsUserExistByUserName(string userName);
        Task<ServiceResult> IsUserExistByEmail(string email);
        Task<ServiceResult> RecoveryPassword(RecoveryPasswordDTO user);
        Task<User> GetUserByUserName(string userName);
    }
}