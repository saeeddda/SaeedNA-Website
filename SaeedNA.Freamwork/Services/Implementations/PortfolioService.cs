using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Entities.Pages;
using SaeedNA.Domain.Repository;
using SaeedNA.Service.Interfaces;

namespace SaeedNA.Service.Implementations
{
    public class PortfolioService:IPortfolioService
    {
        public readonly IGenericRepository<Portfolio> _portfolioRepository;

        public PortfolioService(IGenericRepository<Portfolio> portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _portfolioRepository.DisposeAsync();
        }

        public async Task<ICollection<Portfolio>> GetAllPortfolio()
        {
            return await _portfolioRepository.GetQuery()
                .Include(s => s.Category)
                .Where(s => !s.IsDelete)
                .ToListAsync();
        }

        public async Task<ICollection<Portfolio>> GetAllDeletedPortfolio()
        {
            return await _portfolioRepository.GetQuery()
                .Include(s => s.Category)
                .Where(s => s.IsDelete)
                .ToListAsync();
        }

        public async Task<ICollection<Portfolio>> GetAllPortfolioDes()
        {
            return await _portfolioRepository.GetQuery()
                .Include(s => s.Category)
                .OrderByDescending(s=>s.LastUpdateDate)
                .Where(s => !s.IsDelete)
                .ToListAsync();
        }
    }
}