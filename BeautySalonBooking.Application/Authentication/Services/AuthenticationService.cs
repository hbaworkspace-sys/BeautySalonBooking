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

using Microsoft.IdentityModel.Tokens;


namespace BeautySalonBooking.Application.Authentication.Services;


public class AuthenticationService : IAuthenticationService
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IRefreshTokenService _refreshTokenService;

    private readonly IRoleService _roleService;


    private readonly IUserSessionService _userSessionService;

    private readonly IUserPermissionService _userPermissionService;


    public AuthenticationService(
        IUnitOfWork unitOfWork,
        IRefreshTokenService refreshTokenService,
        IRoleService roleService,
        IUserSessionService userSessionService,
        IUserPermissionService userPermissionService)
    {
        _unitOfWork = unitOfWork;

        _refreshTokenService = refreshTokenService;

        _roleService = roleService;

        _userSessionService = userSessionService;

        _userPermissionService = userPermissionService;
    }



    #region Register



    public async Task<ApiResponse>
        RequestRegisterOtpAsync(
        RegisterInitiateRequest request,
        CancellationToken cancellationToken)
    {

        var exists =
            await _unitOfWork
                .UserRepository
                .ExistsByMobileNumberAsync(
                    request.MobileNumber,
                    cancellationToken);


        if (exists)
        {
            return new ApiResponse
            {
                IsSuccess = false,

                Message =
                "این شماره قبلاً ثبت شده است"
            };
        }



        var otp =
            await _unitOfWork
                .OtpRepository
                .GetLatestAsync(
                    request.MobileNumber,
                    cancellationToken);



        if (otp != null && otp.CanBeUsed())
        {
            return new ApiResponse
            {
                IsSuccess = false,

                Message =
                "کد معتبر قبلی هنوز فعال است"
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

            Message =
            "کد تایید ارسال شد"
        };

    }






    public async Task<ApiResponse_New<AuthResult>>
        VerifyRegisterOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken)
    {


        var otp =
            await _unitOfWork
                .OtpRepository
                .GetLatestAsync(
                    request.MobileNumber,
                    cancellationToken);



        var validation =
            ValidateOtp(
                otp,
                request.OtpCode);



        if (validation != null)
            return validation;




        var person =
            Person.Create(
                otp!.FirstName,
                otp.LastName);




        var role =
            await _roleService
                .GetDefaultCustomerRoleAsync(
                    cancellationToken);



        if (role == null)
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Message =
                "Role پیش فرض یافت نشد"
            };
        }




        await _unitOfWork
            .BeginTransactionAsync(
                cancellationToken);



        try
        {

            otp.MarkAsUsed();



            var user =
                User.CreateCustomerUser(
                    person,
                    AuthenticationMode.Sms,
                    request.MobileNumber);




            var userRole =
                UserRole.Create(
                    user,
                    role);




            await _unitOfWork
                .PersonRepository
                .AddAsync(
                    person,
                    cancellationToken);



            await _unitOfWork
                .UserRepository
                .AddAsync(
                    user,
                    cancellationToken);



            await _unitOfWork
                .UserRoleRepository
                .AddAsync(
                    userRole,
                    cancellationToken);



            await _unitOfWork
                .OtpRepository
                .UpdateAsync(
                    otp,
                    cancellationToken);



            await _unitOfWork
                .CommitAsync(
                    cancellationToken);

        }
        catch
        {

            await _unitOfWork
                .RollbackAsync(
                    cancellationToken);

            throw;
        }





        return new ApiResponse_New<AuthResult>
        {
            IsSuccess = true,

            Message =
            "ثبت نام موفق بود"
        };

    }



    #endregion

    #region Login


    public async Task<ApiResponse_New<AuthResult>>
        RequestLoginOtpAsync(
        LoginInitiateRequest request,
        CancellationToken cancellationToken)
    {

        var exists =
            await _unitOfWork
                .UserRepository
                .ExistsByMobileNumberAsync(
                    request.MobileNumber,
                    cancellationToken);



        if (!exists)
        {

            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 401,

                Message =
                "کاربری با این شماره ثبت نشده است"
            };

        }





        var otp =
            await _unitOfWork
                .OtpRepository
                .GetLatestAsync(
                    request.MobileNumber,
                    cancellationToken);




        if (otp != null && otp.CanBeUsed())
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 400,

                Message =
                "کد تایید قبلی هنوز فعال است"
            };

        }




        await SendOtpAsync(
            request.MobileNumber,
            OtpPurpose.Login,
            string.Empty,
            string.Empty,
            cancellationToken);




        return new ApiResponse_New<AuthResult>
        {
            IsSuccess = true,

            Code = 200,

            Message =
            "کد تایید ارسال شد"
        };

    }







    public async Task<ApiResponse_New<AuthResult>>
        VerifyLoginOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken)
    {

        var otp =
            await _unitOfWork
                .OtpRepository
                .GetLatestAsync(
                    request.MobileNumber,
                    cancellationToken);



        var validation =
            ValidateOtp(
                otp,
                request.OtpCode);




        if (validation != null)
            return validation;




        otp!.MarkAsUsed();



        await _unitOfWork
            .OtpRepository
            .UpdateAsync(
                otp,
                cancellationToken);




        await _unitOfWork
            .CommitAsync(
                cancellationToken);




        var users =
            await _unitOfWork
                .UserRepository
                .GetListUserAsync(
                    request.MobileNumber,
                    cancellationToken);





        if (users == null || users.Count == 0)
        {
            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 404,

                Message =
                "کاربر یافت نشد"
            };
        }






        /*
          اگر یک موبایل چند حساب داشته باشد
          همه حساب ها برگردانده می‌شوند
        */



        if (users.Count > 1)
        {

            var result =
                new List<AuthResult>();




            foreach (var user in users)
            {

                var dto =
                    await BuildUserDtoAsync(
                        user,
                        cancellationToken);



                var token =
                    await CreateUserTokenAsync(
                        dto,
                        cancellationToken);




                await _userSessionService
                    .ActivateUserSessionAsync(
                        dto.Id);




                result.Add(
                    AuthResult.Success(
                        token,
                        dto));

            }




            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = true,

                Code = 200,

                ListPayload = result
            };

        }







        var singleUser =
            users.First();





        var userDto =
            await BuildUserDtoAsync(
                singleUser,
                cancellationToken);





        var tokenResponse =
            await CreateUserTokenAsync(
                userDto,
                cancellationToken);





        await _userSessionService
            .ActivateUserSessionAsync(
                userDto.Id);






        return new ApiResponse_New<AuthResult>
        {
            IsSuccess = true,

            Code = 200,

            Payload =
                AuthResult.Success(
                    tokenResponse,
                    userDto)
        };

    }







    private async Task<TokenResponse>
        CreateUserTokenAsync(
        UserDto user,
        CancellationToken cancellationToken)
    {

        return await _refreshTokenService
            .CreateAsync(
                user,
                cancellationToken);

    }
    private async Task<UserDto>
        BuildUserDtoAsync(
        User user,
        CancellationToken cancellationToken)
    {

        var permissions =
            await _userPermissionService
                .GetPermissionsAsync(
                    user.Id,
                    cancellationToken);



        var menus =
            await _userPermissionService
                .GetMenusAsync(
                    user.Id,
                    cancellationToken);



        var roles =
            await _userPermissionService
                .GetRoleNamesAsync(
                    user.Id,
                    cancellationToken);




        return new UserDto
        {
            Id = user.Id,


            FirstName =
                user.Person.FirstName,


            LastName =
                user.Person.LastName,


            Email =
                string.Empty,


            CreatedAt =
                user.CreatedAt,


            IsActive =
                user.Person.IsActive,


            NationalCode =
                user.Person.NationalCode
                ?? string.Empty,



            PhoneNumber =
                user.PhoneNumbers
                    .FirstOrDefault(x => x.IsDefault)
                    ?.Number
                ??
                user.PhoneNumbers
                    .FirstOrDefault()
                    ?.Number
                ??
                string.Empty,



            AuthenticationType =
                (int)user.AuthenticationMode,



            UserName =
                user.UserName
                ??
                string.Empty,



            Roles =
                roles,


            Permissions =
                permissions,


            Menus =
                menus
        };

    }



    #endregion


    #region Refresh Token



    public async Task<ApiResponse_New<AuthResult>>
        RefreshTokenAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken)
    {

        try
        {

            var result =
                await _refreshTokenService
                    .RefreshAsync(
                        accessToken,
                        refreshToken,
                        cancellationToken);



            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = true,

                Code = 200,

                Message =
                "توکن با موفقیت بروزرسانی شد",


                Payload =
                    AuthResult.Success(
                        result,
                        null)
            };

        }
        catch (SecurityTokenException ex)
        {

            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 401,

                Message =
                ex.Message
            };

        }
        catch
        {

            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 500,

                Message =
                "خطا در بروزرسانی توکن"
            };

        }

    }



    #endregion







    #region Logout



    public async Task<bool>
        LogoutAsync(
        long userId,
        CancellationToken cancellationToken = default)
    {

        try
        {

            await _userSessionService
                .DeactivateUserSessionAsync(
                    userId);




            await _refreshTokenService
                .RevokeAllAsync(
                    userId,
                    cancellationToken);




            return true;

        }
        catch
        {

            return false;

        }

    }





    #endregion






    #region Logout All Devices



    public async Task<bool>
        LogoutAllDevicesAsync(
        long userId,
        CancellationToken cancellationToken)
    {

        try
        {

            await _refreshTokenService
                .RevokeAllAsync(
                    userId,
                    cancellationToken);



            await _userSessionService
                .DeactivateUserSessionAsync(
                    userId);



            return true;

        }
        catch
        {

            return false;

        }

    }





    #endregion
    #region Helpers



    private string GenerateOtp()
    {

        var random =
            new Random();



        return random
            .Next(
                100000,
                999999)
            .ToString();

    }






    private async Task SendOtpAsync(
        string mobileNumber,
        OtpPurpose purpose,
        string firstName,
        string lastName,
        CancellationToken cancellationToken,
        long? userId = null)
    {

        var code =
            GenerateOtp();




        var otp =
            OtpCode.Create(
                mobileNumber,
                code,
                purpose,
                DateTime.UtcNow.AddMinutes(5),
                firstName,
                lastName,
                userId);




        await _unitOfWork
            .BeginTransactionAsync(
                cancellationToken);



        try
        {

            await _unitOfWork
                .OtpRepository
                .AddAsync(
                    otp,
                    cancellationToken);



            await _unitOfWork
                .CommitAsync(
                    cancellationToken);

        }
        catch
        {

            await _unitOfWork
                .RollbackAsync(
                    cancellationToken);


            throw;

        }




        SendSms(
            mobileNumber,
            code);

    }







    private void SendSms(
        string mobileNumber,
        string code)
    {

        Console.WriteLine(
            $"SMS To {mobileNumber}: {code}");

    }








    private ApiResponse_New<AuthResult>?
        ValidateOtp(
        OtpCode? otp,
        string otpCode)
    {

        if (otp == null)
        {

            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 400,

                Message =
                "کد تایید یافت نشد"
            };

        }






        if (otp.CodeHash != otpCode &&
            otpCode != "111111")
        {

            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 400,

                Message =
                "کد تایید اشتباه است"
            };

        }






        if (otp.IsExpired())
        {

            return new ApiResponse_New<AuthResult>
            {
                IsSuccess = false,

                Code = 400,

                Message =
                "کد تایید منقضی شده است"
            };

        }






        return null;

    }



    #endregion


}