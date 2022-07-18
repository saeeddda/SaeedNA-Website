﻿using Microsoft.EntityFrameworkCore;
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
    public class PersonalInfoService : IPersonalInfoService
    {
        private readonly IGenericRepository<PersonalInfo> _personalInfoRepository;

        public PersonalInfoService(IGenericRepository<PersonalInfo> personalInfoRepository)
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

        public async Task<ServiceResult> EditInfo(PersonalInfoGetSetDTO info)
        {
            try
            {
                var entity = await _personalInfoRepository.GetEntityById(info.PersonalInfoId);
                if (entity == null) return ServiceResult.NotFond;

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
                if (entity == null) return ServiceResult.NotFond;

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

            query = query.Where(s => s.IsDelete == filter.IsDelete);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetPersonalInfo(allEntities).SetPaging(pager);
        }

        public async Task<PersonalInfoGetSetDTO> GetDefaultInfo()
        {
            var query = await _personalInfoRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete);

            if (query == null) return null;

            return new PersonalInfoGetSetDTO
            {
                AboutMe = query.AboutMe,
                Address = query.Address,
                Age = query.Age,
                AvatarImage = query.AvatarImage,
                Birthday = query.Birthday,
                Email = query.Email,
                FullName = query.FullName,
                IsDefault = query.IsDefault,
                Language = query.Language,
                Mobile = query.Mobile,
                Nationality = query.Nationality,
                PhoneNumber = query.PhoneNumber,
                ResumeFile = query.ResumeFile,
                ResumeImage = query.ResumeImage,
                PersonalInfoId = query.Id
            };
        }

        public async Task<ServiceResult> SetDefaultPersonalInfo(PersonalInfoGetSetDTO personalInfo)
        {
            try
            {
                ServiceResult isEdidted = ServiceResult.NotFond;
                var backupInfo = new PersonalInfoGetSetDTO();

                #region Delete Old Data

                if (personalInfo.PersonalInfoId > 0)
                {
                    var infoData = await _personalInfoRepository.GetQuery().AsQueryable()
                        .SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete && s.Id == personalInfo.PersonalInfoId);

                    var oldInfo = new PersonalInfoGetSetDTO
                    {
                        PersonalInfoId = infoData.Id,
                        IsDefault = false,
                        AboutMe = infoData.AboutMe,
                        Address = infoData.Address,
                        Age = infoData.Age,
                        AvatarImage = infoData.AvatarImage,
                        Birthday = infoData.Birthday,
                        Email = infoData.Email,
                        FullName = infoData.FullName,
                        Language = infoData.Language,
                        Mobile = infoData.Mobile,
                        Nationality = infoData.Nationality,
                        PhoneNumber = infoData.PhoneNumber,
                        ResumeFile = infoData.ResumeFile,
                        ResumeImage = infoData.ResumeImage
                    };

                    backupInfo = oldInfo;

                    isEdidted =  await EditInfo(oldInfo) == ServiceResult.Success &&
                        await DeleteInfo(oldInfo.PersonalInfoId) == ServiceResult.Success ? ServiceResult.Success : ServiceResult.Error;
                }

                #endregion

                #region Add New Data

                var newInfo = new PersonalInfoCreateDTO
                {
                    AboutMe = string.IsNullOrEmpty(personalInfo.AboutMe) ? backupInfo.AboutMe : personalInfo.AboutMe,
                    Address = string.IsNullOrEmpty(personalInfo.Address) ? backupInfo.Address : personalInfo.Address,
                    Age = string.IsNullOrEmpty(personalInfo.Age) ? backupInfo.Age : personalInfo.Age,
                    AvatarImage = string.IsNullOrEmpty(personalInfo.AvatarImage) ? backupInfo.AvatarImage : personalInfo.AvatarImage,
                    Birthday = string.IsNullOrEmpty(personalInfo.Birthday) ? backupInfo.Birthday : personalInfo.Birthday,
                    Email = string.IsNullOrEmpty(personalInfo.Email) ? backupInfo.Email : personalInfo.Email,
                    FullName = string.IsNullOrEmpty(personalInfo.FullName) ? backupInfo.FullName : personalInfo.FullName,
                    Language = string.IsNullOrEmpty(personalInfo.Language) ? backupInfo.Language : personalInfo.Language,
                    Mobile = string.IsNullOrEmpty(personalInfo.Mobile) ? backupInfo.Mobile : personalInfo.Mobile,
                    Nationality = string.IsNullOrEmpty(personalInfo.Nationality) ? backupInfo.Nationality : personalInfo.Nationality,
                    PhoneNumber = string.IsNullOrEmpty(personalInfo.PhoneNumber) ? backupInfo.PhoneNumber : personalInfo.PhoneNumber,
                    ResumeFile = string.IsNullOrEmpty(personalInfo.ResumeFile) ? backupInfo.ResumeFile : personalInfo.ResumeFile,
                    ResumeImage = string.IsNullOrEmpty(personalInfo.ResumeImage) ? backupInfo.ResumeImage : personalInfo.ResumeImage,
                    IsDefault = true
                };

                var addResult = await AddNewInfo(newInfo) == ServiceResult.Success ? ServiceResult.Success : ServiceResult.Error;

                #endregion

                return isEdidted == ServiceResult.Success && addResult == ServiceResult.Success ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }
    }
}