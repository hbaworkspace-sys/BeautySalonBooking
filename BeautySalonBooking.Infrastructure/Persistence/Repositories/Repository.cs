using BeautySalonBooking.Domain.Base;
using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using BeautySalonBooking.Domain.Base.Repositories;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : struct
{
    protected readonly BeautyDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(BeautyDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    // ========== عملیات پایه ==========

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    }

    // ========== عملیات با Include ==========

    public virtual async Task<TEntity?> GetByIdWithIncludesAsync(
        TKey id,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();

        if (includes?.Any() == true)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();

        if (includes?.Any() == true)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> FindWithIncludesAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = _dbSet.Where(predicate);

        if (includes?.Any() == true)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync(cancellationToken);
    }

    // ========== عملیات CRUD ==========

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await _dbSet.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            Delete(entity);
        }
    }

    public virtual async Task DeleteRangeByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
        DeleteRange(entities);
    }

    // ========== عملیات استعلام ==========

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate == null
            ? await _dbSet.CountAsync(cancellationToken)
            : await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public virtual async Task<TResult?> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return await _dbSet.MaxAsync(selector, cancellationToken);
    }

    public virtual async Task<TResult?> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
    {
        return await _dbSet.MinAsync(selector, cancellationToken);
    }

    public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default)
    {
        return await _dbSet.SumAsync(selector, cancellationToken);
    }

    public virtual async Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AverageAsync(selector, cancellationToken);
    }

    // ========== پیج‌بندی و مرتب‌سازی ==========

    public virtual async Task<PagedResult<TEntity>> GetPagedAsync(
        int pageNumber = 1,
        int pageSize = 10,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default)
    {
        return await GetPagedWithIncludesAsync(pageNumber, pageSize, predicate, orderBy, cancellationToken);
    }

    public virtual async Task<PagedResult<TEntity>> GetPagedWithIncludesAsync(
        int pageNumber = 1,
        int pageSize = 10,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        // اعمال فیلتر
        var query = predicate != null ? _dbSet.Where(predicate) : _dbSet;

        // اعمال Include
        if (includes?.Any() == true)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        // دریافت تعداد کل
        var totalCount = await query.CountAsync(cancellationToken);

        // اعمال مرتب‌سازی
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        else
        {
            // مرتب‌سازی پیش‌فرض بر اساس Id
            query = query.OrderBy(e => e.Id);
        }

        // اعمال پیج‌بندی
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>(items, totalCount, pageNumber, pageSize);
    }

    // ========== عملیات پیشرفته ==========

    public virtual IQueryable<TEntity> Query()
    {
        return _dbSet.AsQueryable();
    }

    public virtual async Task<IEnumerable<TEntity>> FromSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FromSqlInterpolated(sql).ToListAsync(cancellationToken);
    }

    public virtual async Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        return await _context.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
    }
}