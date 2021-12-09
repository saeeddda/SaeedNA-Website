using SaeedNA.Data.DTOs.Authentication;
using SaeedNA.Data.DTOs.Common;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IUserService:IAsyncDisposable
    {
        Task<ServiceResult> RegisterUser(RegisterUserDTO user);
        Task<ServiceResult> GetUserForLogin(LoginUserDTO user);
        Task<ServiceResult> ForgotPassword(ForgotUserDTO forgot);
        Task<ServiceResult> IsUserExistByUserName(string userName);
        Task<ServiceResult> IsUserExistByEmail(string email);
        Task<ServiceResult> RecoveryPassword(RecoveryPasswordDTO user);
    }
}