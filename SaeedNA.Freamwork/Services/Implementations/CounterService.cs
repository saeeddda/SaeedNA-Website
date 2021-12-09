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

        public async Task<ServiceResult> AddNewCounter(CounterCreateDTO counter)
        {
            try
            {
                var entity = new Counter
                {
                    Title = counter.Title,
                    Number = counter.Number,
                    Icon = counter.Icon
                };

                var result = await _counterRepository.AddEntity(entity);
                await _counterRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditCounter(CounterEditDTO counter)
        {
            try
            {
                var entity = await _counterRepository.GetEntityById(counter.CounterId);
                if (entity == null) return ServiceResult.NotFond;

                entity.Title = counter.Title;
                entity.Number = counter.Number;
                entity.Icon = counter.Icon;

                var result = _counterRepository.EditEntity(entity);
                await _counterRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteCounter(long counterId)
        {
            try
            {
                var entity = await _counterRepository.GetEntityById(counterId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _counterRepository.DeleteEntity(entity);
                await _counterRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<CounterFilterDTO> FilterCounter(CounterFilterDTO filter)
        {
            var query = _counterRepository.GetQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities =  await query.Paging(pager).ToListAsync();

            return filter.SetCounter(allEntities).SetPaging(pager);
        }
    }
}
