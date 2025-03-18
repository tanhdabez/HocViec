using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        //private readonly DbContext _context = context;
        //private IDbContextTransaction? _transaction;
        //private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        //public async Task<TEntity?> GetByIdAsync(object id)
        //{

        //    return await _dbSet.FindAsync(id);
        //}

        //public async Task<IEnumerable<TEntity>> GetAllIncludeAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    return await query.OrderByDescending(x => x.UpdatedDate).ToListAsync();
        //}

        //public async Task<TEntity?> GetSingleIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = _dbSet.AsQueryable();

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    return await query.FirstOrDefaultAsync(predicate);
        //}

        //public async Task<IEnumerable<TEntity>> GetIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    var query = _dbSet.AsQueryable();

        //    foreach (var includeProperty in includeProperties)
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    return await query.Where(predicate).OrderByDescending(x => x.UpdatedDate).ToListAsync();
        //}

        //public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await _dbSet.Where(predicate).OrderByDescending(x => x.UpdatedDate).ToListAsync();
        //}

        //public async Task<IEnumerable<TEntity>> GetAllAsync()
        //{
        //    return await _dbSet.OrderByDescending(x => x.UpdatedDate).ToListAsync();
        //}

        //public async Task<TEntity> AddAsync(TEntity entity)
        //{
        //    entity.CreatedDate = DateTime.Now;
        //    entity.UpdatedDate = entity.CreatedDate;
        //    _dbSet.Add(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        //{
        //    entities.ToList().ForEach(entity =>
        //    {
        //        entity.CreatedDate = DateTime.Now;
        //        entity.UpdatedDate = entity.CreatedDate;
        //    });
        //    _dbSet.AddRange(entities);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        //{

        //    _context.AttachRange(entities);

        //    // Update properties for each entity in the collection
        //    foreach (var entity in entities)
        //    {
        //        // Modify properties as needed...
        //        entity.UpdatedDate = DateTime.Now;
        //    }

        //    // Set the state of all entities to Modified
        //    _context.UpdateRange(entities);

        //    // Save changes
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<TEntity> UpdateAsync(TEntity entity)
        //{
        //    _context.Entry(entity).State = EntityState.Modified;
        //    entity.UpdatedDate = DateTime.Now;
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task<bool> DeleteAsync(TEntity entity)
        //{
        //    _dbSet.Remove(entity);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        ////public async Task InsertHistoryLog(Guid actionBy, string action)
        ////{
        ////    var historyLog = new HistoryLog
        ////    {
        ////        ActionBy = actionBy,
        ////        Action = action,
        ////        ActionDate = DateTime.Now
        ////    };
        ////    historyLog.CreatedDate = historyLog.UpdatedDate = DateTime.Now;

        ////    await _context.Set<HistoryLog>().AddAsync(historyLog);
        ////    await _context.SaveChangesAsync();
        ////}

        //public async Task SaveAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}

        //public async Task BeginTransactionAsync()
        //{
        //    _transaction = await _context.Database.BeginTransactionAsync();
        //}

        //public async Task CommitAsync()
        //{
        //    await _transaction.CommitAsync();
        //}

        //public async Task RollbackAsync()
        //{
        //    await _transaction.RollbackAsync();
        //}

        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.OrderBy(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllWithIncludesAndStatusAsync(params string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.OrderBy(x => x.CreatedDate).Where(x=>x.TrangThai == true).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id) ?? throw new Exception("Không tồn tại");
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;
            //_dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.TrangThai = !entity.TrangThai;
                entity.UpdatedDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                return true;
            }    
            return false;
        }

        public string GenerateMa(string prefix)
        {
            string id = Guid.NewGuid().ToString("N");
            return prefix + "-" + id.Substring(0, 8).ToUpper();
        }

        public string GenerateInvoiceCode(string prefix)
        {
            string invoiceId = Guid.NewGuid().ToString("N");
            // Sử dụng format "N" để loại bỏ dấu gạch ngang, sau đó lấy 8 ký tự đầu
            return prefix + "-" + invoiceId.Substring(0, 8).ToUpper();
        }
    }
}
