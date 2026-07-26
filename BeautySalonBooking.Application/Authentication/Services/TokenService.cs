using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using BeautySalonBooking.Contracts.Authentication.Responses;
using BeautySalonBooking.Domain.Base.UnitOfWork;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Repositories;
using BeautySalonBooking.Domain.Identity.UserAggregate.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BeautySalonBooking.Application.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;

            _unitOfWork = unitOfWork;
        }

        public async Task<TokenResponse> GenerateTokensAsync(UserDto user, CancellationToken cancellationToken)
        {
            var accessToken = GenerateAccessToken(user, cancellationToken);
            var refreshToken = GenerateRefreshToken(cancellationToken);

            var refreshTokenEntity = RefreshToken.Create(refreshToken, user.Id, DateTime.UtcNow.AddDays(7));
            // ذخیره Refresh Token در دیتابیس
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenEntity);
                await _unitOfWork.CommitAsync(cancellationToken);

                return new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                    TokenType = "Bearer"
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }

        }

        public async Task<TokenResponse> RefreshTokensAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
        {
            var storedToken = await _unitOfWork.RefreshTokenRepository.GetValidTokenAsync(refreshToken);
            if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
                throw new SecurityTokenException("Invalid or expired refresh token");

            var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(storedToken.UserId);

            if (existingUser == null)
                throw new SecurityTokenException("User not found");
            UserDto currentUser = new UserDto();

            storedToken.SetRevokedToActive();
            _unitOfWork.RefreshTokenRepository.Update(storedToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return await GenerateTokensAsync(currentUser, cancellationToken);
        }

        public ClaimsPrincipal? ValidateToken(string token, CancellationToken cancellationToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        public bool IsTokenExpired(string token, CancellationToken cancellationToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var storedToken = await _unitOfWork.RefreshTokenRepository.GetValidTokenAsync(refreshToken);
            if (storedToken == null) return false;

            storedToken.SetRevokedToActive();
            _unitOfWork.RefreshTokenRepository.Update(storedToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return true;
        }

        private string GenerateAccessToken(UserDto user, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
            new Claim("AuthenticationType", user.AuthenticationType.ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken(CancellationToken cancellationToken)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
