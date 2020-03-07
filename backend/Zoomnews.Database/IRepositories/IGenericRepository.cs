using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Zoomnews.Database.IRepositories
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> QueryAll();
        IQueryable<TEntity> QueryMany(Expression<Func<TEntity, bool>> where);
        Task<int> CountTotalRecordsAsync();
        Task<int> CountManyRecordsAsync(Expression<Func<TEntity, bool>> where);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(string sortField, string sortDirection = "asc", int pageIndex = 1, int pageSize = 5);
        Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<IReadOnlyCollection<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, string sortField, string sortDirection = "asc", int pageIndex = 1, int pageSize = 5);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetByIdAsync(object id, IEnumerable<Expression<Func<TEntity, object>>> includeExpressions);
        Task<TEntity> CreateAsync(TEntity o);
        Task<bool> DeleteAsync(TEntity o);
        Task<TEntity> UpdateAsync(TEntity o);
        Task SaveChangesAsync();
    }
}
