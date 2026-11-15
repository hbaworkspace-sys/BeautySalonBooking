using BeautySalonBooking.Domain.CategoryAggregate.Entities;
using BeautySalonBooking.Domain.CategoryAggregate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;


public sealed class CategoryRepository : Repository<Category, int>, ICategoryRepository
{
    private readonly BeautyDbContext _context;

    public CategoryRepository(BeautyDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Category?> GetWithMediaAsync(int id, CancellationToken ct = default)
    {
        return await _context.Categories
            .Include(x => x.Media)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, ct);
    }
    public async Task<Category?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(x => x.Media)
            .FirstOrDefaultAsync(x => x.Code == code && x.IsActive && !x.IsDeleted, ct);
    }

    public async Task<IReadOnlyList<Category>> GetChildrenAsync(int parentId, CancellationToken ct = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(x => x.Media)
            .Where(x => x.ParentId == parentId && x.IsActive && !x.IsDeleted)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(ct);
    }

    // 👇 متد جدید: لیست ریشه‌ها
    public async Task<IReadOnlyList<Category>> GetRootsAsync(int? type, CancellationToken ct = default)
    {
        var query = _context.Categories
            .AsNoTracking()
            .Include(x => x.Media)
            .Where(x => x.ParentId == null && x.IsActive && !x.IsDeleted);

        if (type.HasValue)
            query = query.Where(x => (int)x.Type == type.Value);

        return await query
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync(ct);
    }

    // 👇 بررسی وجود فرزند
    public async Task<bool> HasChildrenAsync(int categoryId, CancellationToken ct = default)
    {
        return await _context.Categories
            .AnyAsync(x => x.ParentId == categoryId && x.IsActive && !x.IsDeleted, ct);
    }

    // 👇 بررسی وجود سرویس
    public async Task<bool> HasServicesAsync(int categoryId, CancellationToken ct = default)
    {
        return await _context.Services
            .AnyAsync(x => x.CategoryId == categoryId && x.IsActive && !x.IsDeleted, ct);
    }

    // 👇 بررسی تکراری نبودن کد
    public async Task<bool> CodeExistsAsync(string code, int? excludeId = null, CancellationToken ct = default)
    {
        var query = _context.Categories
            .Where(x => x.Code == code && !x.IsDeleted);

        if (excludeId.HasValue)
            query = query.Where(x => x.Id != excludeId.Value);

        return await query.AnyAsync(ct);
    }
}