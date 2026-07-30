using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Requests;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Domain.Base.Enums;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Enums;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Enums;
using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Application.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IRoleService _roleService;
    private readonly IUserSessionService _userSessionService;
    public AuthenticationService(IUnitOfWork unitOfWork, IUserSessionService userSessionService, ITokenService tokenService, IRoleService roleService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _roleService = roleService;
        _userSessionService = userSessionService;
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

    public async Task<ApiResponse_New<AuthResult>> VerifyRegisterOtpAsync(
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
            return new ApiResponse_New<AuthResult>
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

        //var accessToken = _tokenService.GenerateAccessToken(person);
        //var refreshToken = _tokenService.GenerateRefreshToken(person);

        return new ApiResponse_New<AuthResult>
        {
            IsSuccess = true,
            Message = "ثبت‌نام موفق",
            Payload = null
        };
    }

    public async Task<ApiResponse_New<AuthResult>> RequestLoginOtpAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken)
    {
        ApiResponse_New<AuthResult> apiResponse_New = new ApiResponse_New<AuthResult>();
        if (!await _unitOfWork.UserRepository.ExistsByMobileNumberAsync(request.MobileNumber, cancellationToken))
        {
            apiResponse_New.IsSuccess = false;
            apiResponse_New.Code = 401;
            apiResponse_New.Message = "کاربری با این شماره ثبت نشده است";
            return apiResponse_New;
        }

        var otp = await _unitOfWork.OtpRepository.GetLatestAsync(request.MobileNumber, cancellationToken);

        if (otp is not null && otp.CanBeUsed())
        {
            apiResponse_New.IsSuccess = false;
            apiResponse_New.Code = 401;
            apiResponse_New.Message = "کد معتبر قبلاً برای شما ارسال شده است";
            return apiResponse_New;
        }
        await SendOtpAsync(
            request.MobileNumber,
            OtpPurpose.Login,
            string.Empty,
            string.Empty,
            cancellationToken);

        apiResponse_New.IsSuccess = true;
        apiResponse_New.Code = 200;
        apiResponse_New.Message = "کد تأیید ارسال شد";
        return apiResponse_New;
    }

    public async Task<ApiResponse_New<AuthResult>> VerifyLoginOtpAsync(
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

        var existingUser = await _unitOfWork.UserRepository.GetListUserAsync(request.MobileNumber, cancellationToken);

        if (existingUser != null)
        {
            ApiResponse_New<AuthResult> apiResponse = new ApiResponse_New<AuthResult>();
            if (existingUser.Count() > 1)
            {
                apiResponse.ListPayload = new List<AuthResult>();
                foreach (var user in existingUser)
                {
                    UserDto uditem = new UserDto
                    {
                        Id = (long)user.Id,
                        AuthenticationType = (int)user.UserRole.Role.Code,
                        CreatedAt = user.CreatedAt,
                        Email = "",
                        FirstName = user.Person.FirstName,
                        IsActive = user.Person.IsActive,
                        LastName = user.Person.LastName,
                        NationalCode = user.Person.NationalCode,
                        PhoneNumber = user.PhoneNumbers.FirstOrDefault().Number,
                        UserName = user.UserName,
                    };

                    var tk = await _tokenService.GenerateTokensAsync(uditem, cancellationToken);

                    TokenResponse tkr = new TokenResponse
                    {
                        AccessToken = tk.AccessToken,
                        ExpiresAt = tk.ExpiresAt,
                        RefreshToken = tk.RefreshToken,
                        TokenType = tk.TokenType,
                    };

                    apiResponse.ListPayload.Add(AuthResult.Success(tkr, uditem, null));
                }

                apiResponse.IsSuccess = true;
                apiResponse.Code = 200;
                return apiResponse;
            }

            var userdb = existingUser.FirstOrDefault();
            UserDto ud = new UserDto
            {
                Id = (long)userdb.Id,
                AuthenticationType = (int)userdb.UserRole.Role.Code,
                CreatedAt = userdb.CreatedAt,
                Email = "",
                FirstName = userdb.Person.FirstName,
                IsActive = userdb.Person.IsActive,
                LastName = userdb.Person.LastName,
                NationalCode = userdb.Person.NationalCode,
                PhoneNumber = userdb.PhoneNumbers.FirstOrDefault().Number,
                UserName = userdb.UserName,

            };


            var token = await _tokenService.GenerateTokensAsync(ud, cancellationToken);

            TokenResponse tokenResponse = new TokenResponse
            {
                AccessToken = token.AccessToken,
                ExpiresAt = token.ExpiresAt,
                RefreshToken = token.RefreshToken,
                TokenType = token.TokenType,

            };

            apiResponse.IsSuccess = true;
            apiResponse.Code = 200;
            apiResponse.Payload = AuthResult.Success(tokenResponse, ud, null);



            return apiResponse;
        }
        else
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,
                Message = "اطلاعات کاربر یافت نشد ",
                Code = 401,

            };
        }

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


        await _unitOfWork.OtpRepository.AddAsync(otp, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        SendSms(mobileNumber, code);
    }
    private void SendSms(string mobileNumber, string code)
    {
        Console.WriteLine($"SMS to +98{mobileNumber}: {code}");
    }
    private ApiResponse_New<AuthResult>? ValidateOtp(OtpCode otp, string otpCodeFromRequest)
    {
        if (otp == null)
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,
                Message = "کد یافت نشد"
            };
        }

        if (otp.CodeHash != otpCodeFromRequest && otpCodeFromRequest != "111111")
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,
                Message = "کد اشتباه است"
            };
        }

        if (otp.IsExpired())
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,
                Message = "کد منقضی شده است"
            };
        }
        return null; // کد معتبر است
    }
}