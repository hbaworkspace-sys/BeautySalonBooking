using BeautySalonBooking.Application.Security.Interfaces;
using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Contracts.Auth.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.OTP;
using BeautySalonBooking.Domain.SharedKernel;
using BeautySalonBooking.Domain.UserAggregate.Entities;
using BeautySalonBooking.Domain.UserAggregate.Repositories;

namespace BeautySalonBooking.Application.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly ITokenService _tokenService;
    public AuthenticationService(ITokenService tokenService, IUserRepository userRepository, IOtpRepository otpRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _otpRepository = otpRepository;
    }

    public async Task<ApiResponse> RequestOtpForRegisterAsync(RegisterInitiateRequest request)
    {
        var phoneNumber = PhoneNumber.CreateMobile(request.CountryCode, request.PhoneNumber);

        var existingUser = await _userRepository.GetByPhoneNumberAsync(phoneNumber);

        if (existingUser != null)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "این شماره قبلاً ثبت شده است"
            };
        }
        await _otpRepository.DeleteByPhoneNumberAsync(phoneNumber);

        await SendOtpAsync(request.FirstName, request.LastName, request.CountryCode, request.PhoneNumber);

        return new ApiResponse
        {
            IsSuccess = true,
            Message = "کد تأیید ارسال شد"
        };
    }
    public async Task<ApiResponse<AuthResult>> ConfirmRegistrationAsync(VerifyOtpRequest request)
    {
        var phoneNumber = PhoneNumber.CreateMobile(request.CountryCode, request.PhoneNumber);

        var otp = await _otpRepository.GetByPhoneNumberAsync(phoneNumber);

        var result = ValidateOtp(otp, request.OtpCode);
        if (result != null)
            return result;

        var user = User.Create(otp.FirstName, otp.LastName, phoneNumber, otp.Code);

        await _userRepository.AddAsync(user);
        await _otpRepository.DeleteAsync(otp);

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        return new ApiResponse<AuthResult>
        {
            IsSuccess = true,
            Message = "ثبت‌نام موفق",
            Data = new AuthResult
            {
                UserId = user.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }
    public async Task<ApiResponse> RequestOtpForLoginAsync(LoginInitiateRequest request)
    {
        var phoneNumber = PhoneNumber.CreateMobile(request.CountryCode, request.PhoneNumber);

        var existingUser = await _userRepository.GetByPhoneNumberAsync(phoneNumber);

        if (existingUser == null)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "کاربری با این شماره ثبت نشده است"
            };
        }

        await _otpRepository.DeleteByPhoneNumberAsync(phoneNumber);
        await SendOtpAsync(existingUser.FirstName, existingUser.LastName, request.CountryCode, request.PhoneNumber);

        return new ApiResponse
        {
            IsSuccess = true,
            Message = "کد تأیید ارسال شد"
        };
    }
    public async Task<ApiResponse<AuthResult>> ConfirmLoginAsync(VerifyOtpRequest request)
    {
        var phoneNumber = PhoneNumber.CreateMobile(request.CountryCode, request.PhoneNumber);

        var otp = await _otpRepository.GetByPhoneNumberAsync(phoneNumber);

        var result = ValidateOtp(otp, request.OtpCode);
        if (result != null)
            return result;

        var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);

        if (user is null)
        {
            return new ApiResponse<AuthResult>
            {
                IsSuccess = false,
                Message = "کاربر یافت نشد"
            };
        }
        await _otpRepository.DeleteAsync(otp);

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        return new ApiResponse<AuthResult>
        {
            IsSuccess = true,
            Message = "ورود موفق",
            Data = new AuthResult
            {
                UserId = user.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,

            }
        };
    }

    private string GenerateOtp()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
    private void SendSms(string countryCode, string phoneNumber, string code)
    {
        Console.WriteLine($"SMS to{countryCode}{phoneNumber}: {code}");
    }
    private async Task SendOtpAsync(string firstName, string lastName, string countryCode, string phoneNumber)
    {
        var otpCode = GenerateOtp();
        var otp = OtpCode.Create(firstName, lastName, countryCode, phoneNumber, otpCode, DateTime.UtcNow.AddMinutes(2));

        await _otpRepository.AddAsync(otp);

        SendSms(countryCode, phoneNumber, otpCode);
    }
    private ApiResponse<AuthResult>? ValidateOtp(OtpCode otp, string otpCodeFromRequest)
    {
        if (otp == null)
        {
            return new ApiResponse<AuthResult>
            {
                IsSuccess = false,
                Message = "کد یافت نشد"
            };
        }

        if (otp.Code != otpCodeFromRequest)
        {
            return new ApiResponse<AuthResult>
            {
                IsSuccess = false,
                Message = "کد اشتباه است"
            };
        }

        if (otp.ExpireAt < DateTime.UtcNow)
        {
            return new ApiResponse<AuthResult>
            {
                IsSuccess = false,
                Message = "کد منقضی شده است"
            };
        }
        return null; // کد معتبر است
    }
}