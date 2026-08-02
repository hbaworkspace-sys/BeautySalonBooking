using BeautySalonBooking.Application.Authentication.Helpers;
using BeautySalonBooking.Application.Authentication.Interfaces;

using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;

using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BeautySalonBooking.Application.Authentication.Services;


public class RefreshTokenService : IRefreshTokenService
{

    private readonly IConfiguration _configuration;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IUserPermissionService _userPermissionService;



    public RefreshTokenService(
        IConfiguration configuration,
        IUnitOfWork unitOfWork,
        IUserPermissionService userPermissionService)
    {

        _configuration = configuration;

        _unitOfWork = unitOfWork;

        _userPermissionService = userPermissionService;

    }





    #region Create



    public async Task<TokenResponse>
        CreateAsync(
        UserDto user,
        CancellationToken cancellationToken)
    {

        var accessToken =
            GenerateAccessToken(user);



        var refreshToken =
            GenerateRefreshToken();




        var refreshHash =
            HashHelper.ComputeSha256(
                refreshToken);




        var entity =
            RefreshToken.Create(
                tokenHash: refreshHash,
                userId: user.Id,
                expiresAt:
                    DateTime.UtcNow.AddDays(7),
                deviceId:
                    Guid.NewGuid(),
                deviceName:
                    "Unknown Device",
                ipAddress:
                    "Unknown");





        await _unitOfWork
            .BeginTransactionAsync(
                cancellationToken);




        try
        {

            await _unitOfWork
                .RefreshTokenRepository
                .AddAsync(
                    entity,
                    cancellationToken);



            await _unitOfWork
                .CommitAsync(
                    cancellationToken);




            return new TokenResponse
            {

                AccessToken =
                    accessToken,


                RefreshToken =
                    refreshToken,


                TokenType =
                    "Bearer",


                ExpiresAt =
                    DateTime.UtcNow
                    .AddMinutes(30)

            };


        }
        catch
        {

            await _unitOfWork
                .RollbackAsync(
                    cancellationToken);


            throw;

        }

    }





    #endregion




    #region JWT



    private string GenerateAccessToken(
        UserDto user)
    {

        var key =
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Secret"]!);




        var claims =
            new List<Claim>
            {

                new(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),


                new(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),


                new(
                    ClaimTypes.Name,
                    user.FullName ?? ""),


                new(
                    ClaimTypes.Email,
                    user.Email ?? ""),


                new(
                    "UserName",
                    user.UserName ?? ""),


                new(
                    "AuthenticationType",
                    user.AuthenticationType.ToString()),


                new(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())

            };



        foreach (var role in user.Roles.Distinct())
        {

            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    role));

        }



        foreach (var permission in user.Permissions)
        {

            claims.Add(
                new Claim(
                    "Permission",
                    permission.Code));

        }



        foreach (var menu in user.Menus)
        {

            claims.Add(
                new Claim(
                    "Menu",
                    menu.Code));

        }



        var token =
            new JwtSecurityToken
            (
                issuer:
                    _configuration["Jwt:Issuer"],


                audience:
                    _configuration["Jwt:Audience"],


                claims:
                    claims,


                expires:
                    DateTime.UtcNow.AddMinutes(30),


                signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256)
            );




        return new JwtSecurityTokenHandler()
            .WriteToken(token);

    }



    #endregion

    #region Refresh



    public async Task<TokenResponse>
        RefreshAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new SecurityTokenException(
                "Refresh token is missing.");
        }





        var refreshHash =
            HashHelper.ComputeSha256(
                refreshToken);




        var storedToken =
            await _unitOfWork
                .RefreshTokenRepository
                .GetByHashAsync(
                    refreshHash);





        if (storedToken == null)
        {
            throw new SecurityTokenException(
                "Invalid refresh token.");
        }





        if (storedToken.IsRevoked)
        {
            throw new SecurityTokenException(
                "Refresh token revoked.");
        }





        if (storedToken.ExpiresAt <= DateTime.UtcNow)
        {
            throw new SecurityTokenException(
                "Refresh token expired.");
        }






        var user =
            await _unitOfWork
                .UserRepository
                .GetUserWithFullDetailsAsync(
                    storedToken.UserId,
                    cancellationToken);


        var user2 = await _unitOfWork.UserRepository.GetByIdAsync(
     storedToken.UserId,
    cancellationToken,
    u => u.OrganizationOwners,
    u => u.Person,
    u => u.UserRole
);


        if (user == null)
        {
            throw new SecurityTokenException(
                "User not found.");
        }





        /*
            Refresh Token Rotation
        */


        storedToken.Revoke();



        _unitOfWork
            .RefreshTokenRepository
            .Update(
                storedToken);




        await _unitOfWork
            .CommitAsync(
                cancellationToken);






        var dto =
            await BuildUserDtoAsync(
                user,
                cancellationToken);





        return await CreateAsync(
            dto,
            cancellationToken);

    }







    private async Task<UserDto>
        BuildUserDtoAsync(
        Domain.Identity.UserAggregate.Entities.User user,
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


            Id =
                user.Id,



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
                ??
                string.Empty,



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






    #region Revoke



    public async Task<bool>
        RevokeAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(refreshToken))
            return false;




        var hash =
            HashHelper.ComputeSha256(
                refreshToken);




        var stored =
            await _unitOfWork
                .RefreshTokenRepository
                .GetByHashAsync(
                    hash);





        if (stored == null)
            return false;




        stored.Revoke();




        _unitOfWork
            .RefreshTokenRepository
            .Update(
                stored);




        await _unitOfWork
            .CommitAsync(
                cancellationToken);



        return true;

    }




    #endregion

    #region Revoke All



    public async Task<bool>
        RevokeAllAsync(
        long userId,
        CancellationToken cancellationToken)
    {

        var tokens =
            await _unitOfWork
                .RefreshTokenRepository
                .GetAllByUserIdAsync(
                    userId);




        var changed = false;




        foreach (var token in tokens)
        {

            if (token.IsRevoked)
                continue;



            token.Revoke();



            _unitOfWork
                .RefreshTokenRepository
                .Update(
                    token);



            changed = true;

        }




        if (changed)
        {

            await _unitOfWork
                .CommitAsync(
                    cancellationToken);

        }



        return changed;

    }





    #endregion







    #region Validate Access Token



    public ClaimsPrincipal?
        ValidateAccessToken(
        string accessToken)
    {

        var handler =
            new JwtSecurityTokenHandler();



        var key =
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Secret"]!);




        try
        {

            return handler.ValidateToken(
                accessToken,

                new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,


                    IssuerSigningKey =
                        new SymmetricSecurityKey(key),


                    ValidateIssuer = true,


                    ValidIssuer =
                        _configuration["Jwt:Issuer"],


                    ValidateAudience = true,


                    ValidAudience =
                        _configuration["Jwt:Audience"],


                    ValidateLifetime = true,


                    ClockSkew =
                        TimeSpan.Zero

                },

                out _);

        }
        catch
        {

            return null;

        }

    }




    #endregion







    #region Expired Token Principal



    public ClaimsPrincipal?
        GetPrincipalFromExpiredToken(
        string accessToken)
    {

        var handler =
            new JwtSecurityTokenHandler();



        var key =
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Secret"]!);



        var parameters =
            new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,


                IssuerSigningKey =
                    new SymmetricSecurityKey(key),



                ValidateIssuer = true,


                ValidIssuer =
                    _configuration["Jwt:Issuer"],



                ValidateAudience = true,


                ValidAudience =
                    _configuration["Jwt:Audience"],



                ValidateLifetime = false,


                ClockSkew =
                    TimeSpan.Zero

            };




        try
        {

            var principal =
                handler.ValidateToken(
                    accessToken,
                    parameters,
                    out SecurityToken token);




            if (token is not JwtSecurityToken jwt)
                return null;




            if (!jwt.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }




            return principal;

        }
        catch
        {

            return null;

        }

    }





    #endregion







    #region Token Expiration



    public bool IsTokenExpired(
        string accessToken)
    {

        try
        {

            var handler =
                new JwtSecurityTokenHandler();



            var token =
                handler.ReadJwtToken(
                    accessToken);



            return token.ValidTo <= DateTime.UtcNow;

        }
        catch
        {

            return true;

        }

    }





    #endregion







    #region Helpers



    private static string GenerateRefreshToken()
    {

        var bytes =
            RandomNumberGenerator
                .GetBytes(64);



        return Convert
            .ToBase64String(bytes);

    }





    #endregion



}