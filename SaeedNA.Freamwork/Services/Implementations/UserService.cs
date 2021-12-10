using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Authentication;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.Entities.Account;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IEmailSender _emailSender;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _emailSender = emailSender;
        }

        public async ValueTask DisposeAsync()
        {
            await _userRepository.DisposeAsync();
        }

        public async Task<ServiceResult> RegisterUser(RegisterUserDTO user)
        {
            try
            {
                if (await IsUserExistByUserName(user.UserName) == ServiceResult.Exist) return ServiceResult.Exist;

                var entity = new User
                {
                    UserName = user.UserName,
                    Password = _passwordHelper.EncodePasswordMd5(user.Password),
                    Email = user.Email,
                    ForgotToken = ""
                };

                var result = await _userRepository.AddEntity(entity);
                await _userRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;

            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> GetUserForLogin(LoginUserDTO user)
        {
            try
            {
                var entity = await _userRepository.GetQuery().SingleOrDefaultAsync(s => s.UserName == user.UserName);
                if (entity == null) return ServiceResult.NotFond;
                return entity.Password != _passwordHelper.EncodePasswordMd5(user.Password) ? ServiceResult.NotFond : ServiceResult.Success;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> ForgotPassword(ForgotUserDTO forgot)
        {
            try
            {
                var entity = await _userRepository.GetQuery().SingleOrDefaultAsync(s => s.Email == forgot.Email);
                if (entity == null) return ServiceResult.NotFond;

                var token = new Random().Next(100000, 999999).ToString();
                entity.ForgotToken = token;
                
                var result =  _userRepository.EditEntity(entity);
                await _userRepository.SaveChanges();

                await _emailSender.SendEmail("بازیابی کلمه عبور", $"کد بازیابی رمز عبور شما : {token}", forgot.Email);

                return result ? ServiceResult.Success : ServiceResult.Error;

            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> IsUserExistByUserName(string userName)
        {
            try
            {
                var entity = await _userRepository.GetQuery().AsQueryable().AnyAsync(s => s.UserName == userName);
                return entity ? ServiceResult.Exist : ServiceResult.NotFond;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> IsUserExistByEmail(string email)
        {
            try
            {
                var entity = await _userRepository.GetQuery().AsQueryable().AnyAsync(s => s.Email == email);
                return entity ? ServiceResult.Exist : ServiceResult.NotFond;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> RecoveryPassword(RecoveryPasswordDTO user)
        {
            try
            {
                var entity = await _userRepository.GetQuery().SingleOrDefaultAsync(s => s.ForgotToken == user.ForgotToken);
                if (entity == null) return ServiceResult.NotFond;

                entity.Password = _passwordHelper.EncodePasswordMd5(user.Password);

                var result = _userRepository.EditEntity(entity);
                await _userRepository.SaveChanges();
                
                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }
    }
}