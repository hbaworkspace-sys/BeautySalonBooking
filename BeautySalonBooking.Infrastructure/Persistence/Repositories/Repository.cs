using BeautySalonBooking.Domain.Base;
using BeautySalonBooking.Domain.Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T>
    where T : BaseEntity<int>
{
    protected readonly BeautyDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(BeautyDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> Exists(int id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRange(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public IQueryable<T> Query()
    {
        return _dbSet;
    }
}