using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zoomnews.Database.IRepositories;
using Zoomnews.Database.Models;
using Zoomnews.Database.Models.BaseModels;
using Zoomnews.Common.Extensions;
using ReflectionIT.Mvc.Paging;
using Zoomnews.Database.Data;
using Newtonsoft.Json;

namespace Zoomnews.Database.Repositories
{
    public abstract class GenericRepository<TEntity, DbContextT> : IGenericRepository<TEntity> where TEntity : class, IBaseModel where DbContextT : DbContext
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbSet;
        private bool _saveChangesImmediately;

        internal GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _saveChangesImmediately = true;
        }

        internal GenericRepository(DbContext context, bool saveChangesImmediatly = true)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            _saveChangesImmediately = saveChangesImmediatly;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<TEntity> QueryAll()
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> QueryMany(Expression<Func<TEntity, bool>> where)
        {
            return _context.Set<TEntity>().Where(where);
        }
        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<int> CountTotalRecordsAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<int> CountManyRecordsAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _context.Set<TEntity>().Where(where).CountAsync();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(string sortField, string sortDirection = "asc", int pageIndex = 1, int pageSize = 5)
        {
            var qry = _context.Set<TEntity>();
            var qryOrdered = qry.OrderBy(sortField, sortDirection);
            var results = await PagingList.CreateAsync(qryOrdered, pageSize, pageIndex);
            return results;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, string sortField, string sortDirection = "asc", int pageIndex = 1, int pageSize = 5)
        {
            var qry = _context.Set<TEntity>().Where(where);
            var qryOrdered = qry.OrderBy(sortField, sortDirection);
            var results = await PagingList.CreateAsync(qryOrdered, pageSize, pageIndex);
            return results;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            var qry = _context.Set<TEntity>().Where(where);
            return await qry.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _context.Set<TEntity>().Where(where).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(object id, IEnumerable<Expression<Func<TEntity, object>>> includeExpressions)
        {
            var dbSet = _context.Set<TEntity>().AsQueryable();
            foreach (var includeExpression in includeExpressions)
            {
                dbSet = dbSet.Include(includeExpression);
            }
            var result = await dbSet.FirstOrDefaultAsync(t => t.Id == (Guid)id);
            return result;
        }
         

        public async Task<TEntity> CreateAsync(TEntity o)
        {
            var result = _context.Set<TEntity>().Add(o);
            await AddAuditInfo(result.Entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteAsync(TEntity o)
        {
            var result = _context.Set<TEntity>().Remove(o);
            var returnValue = result.State == EntityState.Deleted;
            await _context.SaveChangesAsync();
            return returnValue;
        }

        public async Task<TEntity> UpdateAsync(TEntity o)
        {
            var result = _context.Set<TEntity>().Update(o);
            await AddAuditInfo(result.Entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        private async Task AddAuditInfo(TEntity entity)
        {
            if (entity is IBaseModel auditable)
            {
                var audit = new Audit
                {
                    Id = Guid.NewGuid(),
                    EntityId = entity.Id,
                    EntityName = auditable.GetType().Name,
                    Changes = JsonConvert.SerializeObject(entity, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }),
                    ChangedAt = DateTime.UtcNow,
                    ChangedByUserId = Guid.Empty,
                    ChangedByUserName = "System audit automaticlly"
                };
                await _context.AddAsync<Audit>(audit);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
