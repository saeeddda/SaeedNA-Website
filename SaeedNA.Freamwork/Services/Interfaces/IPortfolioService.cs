using SaeedNA.Data.DTOs.Common;
using SaeedNA.Data.DTOs.Pages;
using System;
using System.Threading.Tasks;

namespace SaeedNA.Service.Interfaces
{
    public interface IPortfolioService:IAsyncDisposable 
    {
        Task<ServiceResult> AddNewPortfolio(PortfolioCreateDTO portfolio);
        Task<ServiceResult> EditPortfolio(PortfolioEditDTO portfolio);
        Task<ServiceResult> DeletePortfolio(long portfolioId);
        Task<PortfolioFilterDTO> FilterPortfolio(PortfolioFilterDTO filter);
        Task<PortfolioEditDTO> GetPortfolioForEdit(long portfolioId);
    }
}