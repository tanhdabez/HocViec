using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        //Task<TEntity?> GetByIdAsync(object id);

        //Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        //Task<IEnumerable<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties);

        //Task<TEntity?> GetSingleIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        //Task<IEnumerable<TEntity>> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        //Task<IEnumerable<TEntity>> GetAllAsync();

        //Task<TEntity> AddAsync(TEntity entity);

        //Task AddRangeAsync(IEnumerable<TEntity> entities);

        //Task<TEntity> UpdateAsync(TEntity entity);

        //Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        //Task<bool> DeleteAsync(TEntity entity);

        ////Task InsertHistoryLog(Guid actionBy, string action);
        //Task SaveAsync();
        //Task BeginTransactionAsync();
        //Task CommitAsync();
        //Task RollbackAsync();
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllWithIncludesAsync(params string[] includes);
        Task<List<TEntity>> GetAllWithIncludesAndStatusAsync(params string[] includes);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateStatusAsync(Guid id);
        string GenerateMa(string prefix);
        string GenerateInvoiceCode(string prefix);
    }
}
