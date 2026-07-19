using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.SalonVerifications;
using BeautySalonBooking.Maui.Common.Services;
using BeautySalonBooking.Maui.Features.Salons.Constants;
namespace BeautySalonBooking.Maui.Features.Salons
{
    public class SalonVerificationApiService : BaseApiService
    {
        public SalonVerificationApiService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public Task<ApiResponse<SalonVerificationDto>> RegisterAsync(
            RegisterSalonVerificationRequest request)
        {
            return SendAsync<RegisterSalonVerificationRequest, SalonVerificationDto>(
                HttpMethod.Post,
                SalonVerificationRoutes.Register(),
                request);
        }
    }
}