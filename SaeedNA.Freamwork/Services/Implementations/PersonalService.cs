using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.DTOs.Site;
using SaeedNA.Data.Entities.Settings;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class PersonalService : IPersonalService
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

        public async Task<ServiceResult> AddNewInfo(PersonalInfoCreateDTO info)
        {
            try
            {
                var entity = new PersonalInfo
                {
                    FullName = info.FullName,
                    Birthday = info.Birthday,
                    Mobile = info.Mobile,
                    AboutMe = info.AboutMe,
                    Address = info.Address,
                    PhoneNumber = info.PhoneNumber,
                    Email = info.Email,
                    Age = info.Age,
                    Language = info.Language,
                    Nationality = info.Nationality,
                    ResumeImage = info.ResumeImage,
                    ResumeFile = info.ResumeFile,
                    AvatarImage = info.AvatarImage,
                    IsDefault = info.IsDefault

                };

                var result = await _personalInfoRepository.AddEntity(entity);
                await _personalInfoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditInfo(PersonalInfoEditDTO info)
        {
            try
            {
                var entity = await _personalInfoRepository.GetEntityById(info.PersonalInfoId);
                if(entity == null) return ServiceResult.NotFond;

                entity.FullName = info.FullName;
                entity.Birthday = info.Birthday;
                entity.Mobile = info.Mobile;
                entity.AboutMe = info.AboutMe;
                entity.Address = info.Address;
                entity.PhoneNumber = info.PhoneNumber;
                entity.Email = info.Email;
                entity.Age = info.Age;
                entity.Language = info.Language;
                entity.Nationality = info.Nationality;
                entity.ResumeImage = info.ResumeImage;
                entity.ResumeFile = info.ResumeFile;
                entity.AvatarImage = info.AvatarImage;
                entity.IsDefault = info.IsDefault;

                var result = _personalInfoRepository.EditEntity(entity);
                await _personalInfoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteInfo(long infoId)
        {
            try
            {
                var entity = await _personalInfoRepository.GetEntityById(infoId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _personalInfoRepository.DeleteEntity(entity);
                await _personalInfoRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<PersonalInfoFilterDTO> FilterInfo(PersonalInfoFilterDTO filter)
        {
            var query = _personalInfoRepository.GetQuery().AsQueryable();

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetPersonalInfo(allEntities).SetPaging(pager);
        }

        public async Task<PersonalInfoEditDTO> GetDefaultInfo()
        {
            return await _personalInfoRepository.GetQuery()
                .Select(s => new PersonalInfoEditDTO())
                .SingleOrDefaultAsync(s => s.IsDefault);
        }
    }
}