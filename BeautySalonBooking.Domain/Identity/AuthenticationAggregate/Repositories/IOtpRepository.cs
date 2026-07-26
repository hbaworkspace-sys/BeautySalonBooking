using BeautySalonBooking.Domain.Base.Repositories;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

namespace BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;

public interface IOtpRepository : IRepository<OtpCode, long>
{
    Task<OtpCode?> GetLatestAsync(string mobileNumber, CancellationToken cancellationToken);
    //Task AddAsync(OtpCode otp, CancellationToken cancellationToken);
    Task UpdateAsync(OtpCode otp, CancellationToken cancellationToken);
}