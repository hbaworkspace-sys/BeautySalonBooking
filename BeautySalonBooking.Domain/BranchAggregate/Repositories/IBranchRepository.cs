using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.BranchAggregate.Entities;

namespace BeautySalonBooking.Domain.BranchAggregate.Repositories;

public interface IBranchRepository : IRepository<Branch, long>
{
    // متد اختصاصی شما برای دریافت همه شعبه‌ها با Media
    Task<IReadOnlyList<Branch>> GetAllWithMediaAsync(CancellationToken cancellationToken = default);

    // اگر متدهای اختصاصی دیگری دارید (مثلاً GetByOrganizationIdAsync) اینجا اضافه کنید
    // Task<IReadOnlyList<Branch>> GetByOrganizationIdAsync(long organizationId, CancellationToken ct = default);
}