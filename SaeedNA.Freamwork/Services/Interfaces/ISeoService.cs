using System;
using System.Threading.Tasks;
using SaeedNA.Data.Models.Settings;

namespace SaeedNA.Service.Interfaces
{
    public interface ISeoService:IAsyncDisposable
    {
        Task<Seo> GetDefaultSeo();
    }
}