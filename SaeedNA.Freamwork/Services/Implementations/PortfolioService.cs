using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Service.Implementations
{
    public class PortfolioService:IPortfolioService
    {
        private readonly IGenericRepository<Portfolio> _portfolioRepository;

        public PortfolioService(IGenericRepository<Portfolio> portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _portfolioRepository.DisposeAsync();
        }

        public async Task<ServiceResult> AddNewPortfolio(PortfolioCreateDTO portfolio)
        {
            try
            {
                var entity = new Portfolio
                {
                    Title = portfolio.Title,
                    ShortDescription = portfolio.ShortDescription,
                    CategoryId = portfolio.CategoryId,
                    Image = portfolio.Image,
                    State = portfolio.State,
                    Tags = portfolio.Tags,
                    ProjectAddress = portfolio.ProjectAddress,
                    ProjectCustomer = portfolio.ProjectCustomer,
                    ProjectLanguage = portfolio.ProjectLanguage
                };

                var result = await _portfolioRepository.AddEntity(entity);
                await _portfolioRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> EditPortfolio(PortfolioEditDTO portfolio)
        {
            try
            {
                var entity = await _portfolioRepository.GetEntityById(portfolio.PortfolioId);
                if(entity == null) return ServiceResult.NotFond;

                entity.Title = portfolio.Title;
                entity.ShortDescription = portfolio.ShortDescription;
                entity.CategoryId = portfolio.CategoryId;
                entity.Image = portfolio.Image;
                entity.State = portfolio.State;
                entity.Tags = portfolio.Tags;
                entity.ProjectAddress = portfolio.ProjectAddress;
                entity.ProjectCustomer = portfolio.ProjectCustomer;
                entity.ProjectLanguage = portfolio.ProjectLanguage;
                
                var result = _portfolioRepository.EditEntity(entity);
                await _portfolioRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<ServiceResult> DeletePortfolio(long portfolioId)
        {
            try
            {
                var entity = await _portfolioRepository.GetEntityById(portfolioId);
                if(entity == null) return ServiceResult.NotFond;

                var result = _portfolioRepository.DeleteEntity(entity);
                await _portfolioRepository.SaveChanges();

                return result ? ServiceResult.Success : ServiceResult.Error;
            }
            catch
            {
                return ServiceResult.Error;
            }
        }

        public async Task<PortfolioFilterDTO> FilterPortfolio(PortfolioFilterDTO filter)
        {
            var query = _portfolioRepository.GetQuery().AsQueryable();

            switch(filter.State)
            {
                case PagesPublishState.All:
                    query = query.Where(s => s.IsDelete == filter.IsDelete);
                    break;
                case PagesPublishState.Draft:
                    query = query.Where(s => s.State == PortfolioPublishingState.Draft && s.IsDelete == filter.IsDelete);
                    break;
                case PagesPublishState.Published:
                    query = query.Where(s => s.State == PortfolioPublishingState.Published && s.IsDelete == filter.IsDelete);
                    break;
            }

            if(filter.IsDescending)
                query = query.OrderByDescending(s => s.CreateDate);

            if(!string.IsNullOrEmpty(filter.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filter.Title}%"));

            if(filter.CategoryId >= 0)
                query = query.Where(s => s.CategoryId == filter.CategoryId);

            var pager = Pager.Build(filter.PageId, await query.CountAsync(), filter.TakeEntity, filter.HowManyBeforeAndAfter);
            var allEntities = await query.Paging(pager).ToListAsync();

            return filter.SetPortfolio(allEntities).SetPaging(pager);
        }

        public async Task<PortfolioEditDTO> GetPortfolioForEdit(long portfolioId)
        {
            var query = await _portfolioRepository.GetEntityById(portfolioId);

            if (query == null) return null;

            return new PortfolioEditDTO
            {
                Image = query.Image,
                CategoryId = query.CategoryId,
                ProjectAddress = query.ProjectAddress,
                ProjectCustomer = query.ProjectCustomer,
                ProjectLanguage= query.ProjectLanguage, 
                ShortDescription = query.ShortDescription,
                State = query.State,
                Tags = query.Tags,
                Title = query.Title
            };
        }
    }
}