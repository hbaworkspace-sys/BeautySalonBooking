using BeautySalonBooking.Contracts.Authentication.Dtos;
using System.Security.Claims;

namespace BeautySalonBooking.Application.Authentication.Interfaces;

public interface ITokenService
{
    /// <summary>
    /// ایجاد Access Token بر اساس اطلاعات کاربر
    /// </summary>
    /// <param name="user">اطلاعات کاربر</param>
    /// <returns>JWT Access Token</returns>
    string GenerateAccessToken(UserDto user);



    /// <summary>
    /// اعتبارسنجی JWT Token
    /// </summary>
    /// <param name="token">Access Token</param>
    /// <returns>
    /// ClaimsPrincipal در صورت معتبر بودن،
    /// در غیر این صورت null
    /// </returns>
    ClaimsPrincipal? ValidateToken(string token);



    /// <summary>
    /// استخراج اطلاعات کاربر از Token منقضی شده
    /// برای فرآیند Refresh Token
    /// </summary>
    /// <param name="token">Access Token منقضی شده</param>
    /// <returns>
    /// ClaimsPrincipal در صورت معتبر بودن ساختار Token
    /// </returns>
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);



    /// <summary>
    /// بررسی منقضی شدن Token
    /// </summary>
    /// <param name="token">JWT Token</param>
    /// <returns>
    /// true اگر Token منقضی شده باشد
    /// </returns>
    bool IsTokenExpired(string token);
}