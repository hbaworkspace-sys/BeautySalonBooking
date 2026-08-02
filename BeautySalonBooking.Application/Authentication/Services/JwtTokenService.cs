using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeautySalonBooking.Application.Authentication.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(UserDto user)
    {
        var secret =
            _configuration["Jwt:Secret"]
            ?? throw new InvalidOperationException("Jwt Secret not configured.");

        var issuer =
            _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt Issuer not configured.");

        var audience =
            _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt Audience not configured.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret));

        var credentials =
            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),

            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            new(JwtRegisteredClaimNames.Email,user.Email),

            new(JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),

            //new(ClaimTypes.Name,user.FullName),

            new(ClaimTypes.NameIdentifier,user.Id.ToString()),

            new("AuthenticationType",
                user.AuthenticationType.ToString())
        };

        var expires = DateTime.UtcNow.AddMinutes(30);

        var token = new JwtSecurityToken(

            issuer,

            audience,

            claims,

            notBefore: DateTime.UtcNow,

            expires: expires,

            signingCredentials: credentials

        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}