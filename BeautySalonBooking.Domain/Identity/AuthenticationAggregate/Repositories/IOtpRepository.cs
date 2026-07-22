using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

namespace BeautySalonBooking.Domain.OTP
{
    public interface IOtpRepository
    {
        Task<OtpCode?> GetLatestAsync(string mobileNumber, CancellationToken cancellationToken);
        Task AddAsync(OtpCode otp, CancellationToken cancellationToken);
        Task UpdateAsync(OtpCode otp, CancellationToken cancellationToken);
    }
}