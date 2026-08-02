using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BeautySalonBooking.Application.Authentication.Services;


public class TokenService : ITokenService
{

    private readonly IConfiguration _configuration;


    public TokenService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }




    public string GenerateAccessToken(UserDto user)
    {
        var key = Encoding.UTF8.GetBytes(
            _configuration["Jwt:Secret"]!);

        var claims = new List<Claim>
{
    new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),

    new(ClaimTypes.NameIdentifier,user.Id.ToString()),

    new(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),

    new(ClaimTypes.Email,user.Email ?? ""),

    new("UserName",user.UserName ?? ""),

    new("AuthenticationType",user.AuthenticationType.ToString()),

    new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
};

        //-----------------------------------
        // Permissions
        //-----------------------------------

        //foreach (var permission in user.Permissions.Distinct())
        //{
        //    claims.Add(new Claim("Permission", permission));
        //}

        //-----------------------------------
        // Menus
        //-----------------------------------

        //foreach (var menu in user.Menus.Distinct())
        //{
        //    claims.Add(new Claim("Menu", menu));
        //}

        //-----------------------------------
        // Actions
        //-----------------------------------

        //-----------------------------------
        // APIs
        //-----------------------------------



        //-----------------------------------
        // Reports
        //-----------------------------------



        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }




    public ClaimsPrincipal?
        ValidateToken(
            string token)
    {


        var handler =
            new JwtSecurityTokenHandler();



        var key =
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Secret"]!);



        try
        {

            return handler.ValidateToken(
                token,

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


    public ClaimsPrincipal? GetPrincipalFromExpiredToken(
    string token)
    {
        var tokenHandler =
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


                // مهم:
                // چون Token منقضی شده است
                // فقط Lifetime را خاموش می‌کنیم

                ValidateLifetime = false,


                ClockSkew =
                    TimeSpan.Zero
            };


        try
        {
            var principal =
                tokenHandler.ValidateToken(
                    token,
                    parameters,
                    out SecurityToken securityToken);



            if (securityToken is not JwtSecurityToken jwt)
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

    public bool IsTokenExpired(
    string token)
    {
        try
        {
            var handler =
                new JwtSecurityTokenHandler();


            var jwt =
                handler.ReadJwtToken(token);


            return jwt.ValidTo <= DateTime.UtcNow;

        }
        catch
        {
            return true;
        }
    }
}