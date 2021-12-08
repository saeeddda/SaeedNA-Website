using System;
using System.Linq;
using System.Threading.Tasks;
using SaeedNA.Data.Entities.Common;

namespace SaeedNA.Domain.Repository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
    {
        Task<bool> AddEntity(TEntity entity);

        bool EditEntity(TEntity entity);
        
        Task<TEntity> GetEntityById(long entityId);

        IQueryable<TEntity> GetQuery();

        bool DeleteEntity(TEntity entity);

        Task<bool> DeleteEntity(long entityId);

        bool DeletePermanentEntity(TEntity entity);

        Task<bool> DeletePermanentEntity(long entityId);

        Task SaveChanges();
    }
}