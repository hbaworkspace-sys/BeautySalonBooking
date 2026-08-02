using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Base.Models;
using System.Linq.Expressions;

namespace BeautySalonBooking.Domain.Base.Repositories;

public interface IRepository<TEntity, TKey>
where TEntity : BaseEntity<TKey>
where TKey : struct
{
    // ========== عملیات پایه ==========
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    // ========== عملیات با Include ==========
    Task<TEntity?> GetByIdWithIncludesAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> GetAllWithIncludesAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> FindWithIncludesAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    // ========== عملیات CRUD ==========
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task DeleteRangeByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);

    // ========== عملیات استعلام ==========
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    Task<TResult?> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);
    Task<TResult?> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);
    Task<decimal> SumAsync(Expression<Func<TEntity, decimal>> selector, CancellationToken cancellationToken = default);
    Task<double> AverageAsync(Expression<Func<TEntity, double>> selector, CancellationToken cancellationToken = default);

    // ========== پیج‌بندی و مرتب‌سازی ==========
    Task<PagedResult<TEntity>> GetPagedAsync(
        int pageNumber = 1,
        int pageSize = 10,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default);

    Task<PagedResult<TEntity>> GetPagedWithIncludesAsync(
        int pageNumber = 1,
        int pageSize = 10,
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);

    // ========== عملیات پیشرفته ==========
    IQueryable<TEntity> Query();
    Task<IEnumerable<TEntity>> FromSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
    Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
}