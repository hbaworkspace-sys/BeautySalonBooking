using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.SalonVerifications;
using BeautySalonBooking.Domain.Salons;
using BeautySalonBooking.Domain.SharedKernel;

namespace BeautySalonBooking.Application.SalonVerifications
{
    public class SalonVerificationService : ISalonVerificationService
    {
        private readonly ISalonVerificationRepository _salonVerificationRepository;

        public SalonVerificationService(ISalonVerificationRepository salonVerificationRepository)
        {
            _salonVerificationRepository = salonVerificationRepository;
        }

        public async Task<ApiResponse<SalonVerificationDto>> RegisterAsync(RegisterSalonVerificationRequest request)
        {
            var existingVerification =
                await _salonVerificationRepository.GetByOwnerIdAsync(request.OwnerUserId);

            if (existingVerification != null)
            {
                return new ApiResponse<SalonVerificationDto>
                {
                    IsSuccess = false,
                    Message = "درخواست ثبت سالن قبلاً ثبت شده است."
                };
            }

            var verification = CreateSalonVerification(request);

            await _salonVerificationRepository.AddAsync(verification);

            return new ApiResponse<SalonVerificationDto>
            {
                IsSuccess = true,
                Message = "درخواست ثبت سالن با موفقیت ثبت شد.",
                Data = ToDto(verification)
            };
        }

        private SalonVerification CreateSalonVerification(RegisterSalonVerificationRequest request)
        {
            var address = Address.Create(
                GetRegionId(request),
                request.Address,
                request.PostalCode);

            var verification = SalonVerification.Create(
                request.OwnerUserId,
                request.Name,
                address,
                request.LicenceNumber);

            verification.AddPhoneNumber(
                PhoneNumber.CreateLandline(request.LandlinePhoneNumber));

            return verification;
        }

        private static int GetRegionId(RegisterSalonVerificationRequest request)
        {
            return request.RegionId;
        }

        private static SalonVerificationDto ToDto(SalonVerification verification)
        {
            return new SalonVerificationDto
            {
                Id = verification.Id,
                Name = verification.Name,
                LicenceNumber = verification.LicenceNumber,
                Status = (int)verification.Status,
                SubmittedAt = verification.SubmittedAt
            };
        }
    }
}