using BeautySalonBooking.Domain.CategoryAggregate.Entities;
using BeautySalonBooking.Domain.CategoryAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public sealed class CategoryRepository
    : Repository<Category, int>,
      ICategoryRepository
{
    private readonly BeautyDbContext _context;

    public CategoryRepository(BeautyDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Category?> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x =>
                    x.Code == code &&
                    x.IsActive &&
                    !x.IsDeleted,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Category>> GetChildrenAsync(
        int parentId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(
                x =>
                    x.ParentId == parentId &&
                    x.IsActive &&
                    !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(cancellationToken);
    }
}