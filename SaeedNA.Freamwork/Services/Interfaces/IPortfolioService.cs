using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaeedNA.Data.Entities.Pages;

namespace SaeedNA.Service.Interfaces
{
    public interface IPortfolioService:IAsyncDisposable 
    {
        Task<ICollection<Portfolio>> GetAllPortfolio();
        Task<ICollection<Portfolio>> GetAllDeletedPortfolio();
        Task<ICollection<Portfolio>> GetAllPortfolioDes();
    }
}