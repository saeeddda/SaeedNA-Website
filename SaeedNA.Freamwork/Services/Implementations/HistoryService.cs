using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.DTOs.Resume;
using SaeedNA.Data.Entities.Resume;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SaeedNA.Service.Implementations
{
    public class HistoryService : IHistoryService
    {
        private readonly IGenericRepository<History> _historyRepository;

        public HistoryService(IGenericRepository<History> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _historyRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddNewHistory(HistoryCreateDTO history)
        {
            try
            {
                var entity = new History
                {
                    Title = history.Title,
                    Description = history.Description,
                    Date = history.Date,
                    WorkPlace = history.WorkPlace
                };

                var result = await _historyRepository.AddEntity(entity);
                await _historyRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditHistory(HistoryEditDTO history)
        {
            try
            {
                var entity = await _historyRepository.GetEntityById(history.HistoryId);
                if (entity == null) return ServiceResult.NotFond;

                entity.Title = history.Title;
                entity.Description = history.Description;
                entity.Date = history.Date;
                entity.WorkPlace = history.WorkPlace;
                
                var result = _historyRepository.EditEntity(entity);
                await _historyRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeleteHistory(long historyId)
        {
            try
            {
                var entity = await _historyRepository.GetEntityById(historyId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _historyRepository.DeleteEntity(entity);
                await _historyRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<HistoryFilterDTO> FilterHistory(HistoryFilterDTO filter)
        {
            var query = _historyRepository.GetQuery().AsQueryable();

            if(filter.IsDescending)
                query = query.OrderByDescending(s => s.Date);

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetHistory(allEntities).SetPaging(pager);
        }

        public async Task<HistoryEditDTO> GetHistoryById(long historyId)
        {
            var query = await _historyRepository.GetQuery()
                .SingleOrDefaultAsync(s => s.Id == historyId && !s.IsDelete);

            if (query == null) return null;

            return new HistoryEditDTO
            {
                Title = query.Title,
                Date = query.Date,
                Description = query.Description,
                WorkPlace = query.WorkPlace
            };
        }

    }
}
