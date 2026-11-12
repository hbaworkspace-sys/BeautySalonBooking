using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.OrganizationAggregate.Entities;

namespace BeautySalonBooking.Domain.OrganizationAggregate.Repositories;

public interface IOrganizationRepository : IRepository<Organization, long>
{
    // متد اختصاصی شما برای دریافت همه سازمان‌ها با Media
    // این متد در کلاس پایه هم وجود دارد ولی شما نسخه اختصاصی خودتان را می‌خواهید
    Task<IReadOnlyList<Organization>> GetAllWithMediaAsync(CancellationToken cancellationToken = default);

    Task<Organization?> GetFullDetailAsync(long id, CancellationToken ct = default);

    // اگر متدهای اختصاصی دیگری هم دارید (مثلاً GetByLicenseNumberAsync) اینجا اضافه کنید
    // Task<Organization?> GetByLicenseNumberAsync(string licenseNumber, CancellationToken ct = default);
}