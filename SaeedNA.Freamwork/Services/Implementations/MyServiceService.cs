using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.MService;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.MService;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
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

        public async Task<ServiceResult> AddNewService(MyServiceCreateDTO myService)
        {
            try
            {
                var entity = new MyService
                {
                    Title = myService.Title,
                    Text = myService.Text,
                    Icon = myService.Icon
                };

                var result = await _myServiceRepository.AddEntity(entity);
                await _myServiceRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditService(MyServiceEditDTO myService)
        {
            try
            {
                var entity = await _myServiceRepository.GetEntityById(myService.MyServiceId);
                if (entity == null) return ServiceResult.NotFond;

                entity.Title = myService.Title;
                entity.Text = myService.Text;
                entity.Icon = myService.Icon;

                var result = _myServiceRepository.EditEntity(entity);
                await _myServiceRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteService(long myServiceId)
        {
            try
            {
                var entity = await _myServiceRepository.GetEntityById(myServiceId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _myServiceRepository.DeleteEntity(entity);
                await _myServiceRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<MyServiceFilterDTO> FilterService(MyServiceFilterDTO filter)
        {
            var query = _myServiceRepository.GetQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title,$"%{filter.Title}%"));

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetMyService(allEntities).SetPaging(pager);
        }
    }
}
