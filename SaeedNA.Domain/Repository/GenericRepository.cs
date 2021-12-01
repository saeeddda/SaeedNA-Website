using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.Common;

namespace SaeedNA.Domain.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where  TEntity :BaseEntity
    {
        #region constructor

        private readonly SaeedNAContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SaeedNAContext context)
        {
            _context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        #endregion

        #region methods

        public async Task<bool> AddEntity(TEntity entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = entity.CreateDate;

                await _dbSet.AddAsync(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditEntity(TEntity entity)
        {
            try
            {
                entity.LastUpdateDate = DateTime.Now;
                _dbSet.Update(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(s => s.Id == entityId);
        }

        public IQueryable<TEntity> GetQuery(TEntity entity)
        {
            return _dbSet.AsQueryable();
        }

        public async Task<bool> DeleteEntity(long entityId)
        {
            try
            {
                var entity = await GetEntityById(entityId);
                if (entity == null) return false;
                
                entity.IsDelete = true;
                DeleteEntity(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEntity(TEntity entity)
        {
            try
            {
                entity.IsDelete = true;
                entity.LastUpdateDate = DateTime.Now;

                EditEntity(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePermanentEntity(long entityId)
        {
            try
            {
                var entity = await GetEntityById(entityId);
                if (entity != null) DeletePermanentEntity(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePermanentEntity(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
        }

        #endregion
    }
}