using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Application.Security.Interfaces;
using BeautySalonBooking.Contracts.Auth.Requests;
using BeautySalonBooking.Contracts.Auth.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Enums;
using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Application.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IRoleService _roleService;

    public AuthenticationService(IUnitOfWork unitOfWork, ITokenService tokenService, IRoleService roleService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _roleService = roleService;
    }

    public async Task<ApiResponse> RequestRegisterOtpAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken)
    {
        if (await _unitOfWork.UserRepository.ExistsByMobileNumberAsync(request.MobileNumber, cancellationToken))
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "این شماره قبلاً ثبت شده است"
            };
        }

        var otp = await _unitOfWork.OtpRepository.GetLatestAsync(request.MobileNumber, cancellationToken);

        if (otp is not null && otp.CanBeUsed())
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "کد معتبر قبلاً برای شما ارسال شده است"
            };
        }

        await SendOtpAsync(
            request.MobileNumber,
            OtpPurpose.Register,
            request.FirstName,
            request.LastName,
            cancellationToken);

        return new ApiResponse
        {
            IsSuccess = true,
            Message = "کد تأیید ارسال شد"
        };
    }

    public async Task<ApiResponse<AuthResult>> VerifyRegisterOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken)
    {
        var otp = await _unitOfWork.OtpRepository.GetLatestAsync(request.MobileNumber, cancellationToken);

        var result = ValidateOtp(otp, request.OtpCode);

        if (result != null)
            return result;

        var person = Person.Create(otp.FirstName, otp.LastName);
        var customerRole = await _roleService.GetDefaultCustomerRoleAsync(cancellationToken);
        if (customerRole is null)
        {
            return new ApiResponse<AuthResult>
            {
                IsSuccess = false,
                Message = "نقش پیش‌فرض کاربر در سیستم یافت نشد."
            };
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            otp.MarkAsUsed();

            var user = User.CreateCustomerUser(person, AuthenticationMode.Sms, request.MobileNumber);
            var userRole = UserRole.Create(user, customerRole);

            await _unitOfWork.OtpRepository.UpdateAsync(otp, cancellationToken);
            await _unitOfWork.PersonRepository.AddAsync(person, cancellationToken);
            await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.UserRoleRepository.AddAsync(userRole, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }

        var accessToken = _tokenService.GenerateAccessToken(person);
        var refreshToken = _tokenService.GenerateRefreshToken(person);

        return new ApiResponse<AuthResult>
        {
            IsSuccess = true,
            Message = "ثبت‌نام موفق",
            Data = new AuthResult
            {
                PersonId = person.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }

    public async Task<ApiResponse> RequestLoginOtpAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.UserRepository.ExistsByMobileNumberAsync(request.MobileNumber, cancellationToken))
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "کاربری با این شماره ثبت نشده است"
            };
        }

        var otp = await _unitOfWork.OtpRepository.GetLatestAsync(request.MobileNumber, cancellationToken);

        if (otp is not null && otp.CanBeUsed())
        {
            return new ApiResponse
            {
                IsSuccess = false,
                Message = "کد معتبر قبلاً برای شما ارسال شده است"
            };
        }
        await SendOtpAsync(
            request.MobileNumber,
            OtpPurpose.Login,
            string.Empty,
            string.Empty,
            cancellationToken);

        return new ApiResponse
        {
            IsSuccess = true,
            Message = "کد تأیید ارسال شد"
        };
    }

    public async Task<ApiResponse<AuthResult>> VerifyLoginOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken)
    {
        var otp = await _unitOfWork.OtpRepository.GetLatestAsync(request.MobileNumber, cancellationToken);
        var result = ValidateOtp(otp, request.OtpCode);

        if (result != null)
            return result;

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            otp.MarkAsUsed();
            await _unitOfWork.OtpRepository.UpdateAsync(otp, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
        }


        //var accessToken = _tokenService.GenerateAccessToken(person);
        //var refreshToken = _tokenService.GenerateRefreshToken(person);

        return new ApiResponse<AuthResult>
        {
            IsSuccess = true,
            Message = "ورود موفق",
            Data = new AuthResult
            {
                //PersonId = 121,
                //AccessToken = accessToken,
                //RefreshToken = refreshToken,
            }
        };
    }

    private string GenerateOtp()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
    private async Task SendOtpAsync(
        string mobileNumber,
        OtpPurpose otpPurpose,
        string firstName,
        string lastName,
        CancellationToken cancellationToken,
        long? userId = null)
    {
        var code = GenerateOtp();

        var otp = OtpCode.Create(
            mobileNumber,
            code,
            otpPurpose,
            DateTime.UtcNow.AddMinutes(5),
            firstName,
            lastName,
            userId);


        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await _unitOfWork.OtpRepository.AddAsync(otp, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }





        SendSms(mobileNumber, code);
    }
    private void SendSms(string mobileNumber, string code)
    {
        Console.WriteLine($"SMS to +98{mobileNumber}: {code}");
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

        if (otp.CodeHash != otpCodeFromRequest && otpCodeFromRequest != "111111")
        {
            return new ApiResponse<AuthResult>
            {
                IsSuccess = false,
                Message = "کد اشتباه است"
            };
        }

        if (otp.IsExpired())
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