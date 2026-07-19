using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.SalonVerifications;

namespace BeautySalonBooking.Application.SalonVerifications
{
    public interface ISalonVerificationService
    {
        Task<ApiResponse<SalonVerificationDto>> RegisterAsync(RegisterSalonVerificationRequest request);
    }
}
