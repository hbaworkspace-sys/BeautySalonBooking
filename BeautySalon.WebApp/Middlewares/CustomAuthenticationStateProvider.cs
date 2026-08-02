using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace BeautySalonBooking.WebApp.Authentication
{
    public class CustomAuthenticationStateProvider
        : AuthenticationStateProvider
    {

        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;


        public CustomAuthenticationStateProvider(
            IJSRuntime jsRuntime,
            ILogger<CustomAuthenticationStateProvider> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }



        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                var token =
                    await _jsRuntime.InvokeAsync<string>(
                        "localStorage.getItem",
                        "accessToken");
                if (string.IsNullOrWhiteSpace(token))
                {
                    return Anonymous();
                }

                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(token))
                {
                    return Anonymous();
                }

                var jwt = handler.ReadJwtToken(token);

                if (jwt.ValidTo <= DateTime.UtcNow)
                {
                    return Anonymous();
                }

                return new AuthenticationState(CreateClaimsPrincipal(token));

                //if (string.IsNullOrWhiteSpace(token))
                //{
                //    return Anonymous();
                //}



                //var user =
                //    CreateClaimsPrincipal(token);



                //return new AuthenticationState(user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error loading authentication state");


                return Anonymous();
            }
        }




        private ClaimsPrincipal CreateClaimsPrincipal(string token)
        {

            var claims =
                GetClaimsFromToken(token);



            var identity =
                new ClaimsIdentity(
                    claims,
                    "jwt");


            return new ClaimsPrincipal(identity);
        }





        private List<Claim> GetClaimsFromToken(string token)
        {

            var claims = new List<Claim>();


            try
            {

                var handler =
                    new JwtSecurityTokenHandler();



                var jwt =
                    handler.ReadJwtToken(token);



                foreach (var claim in jwt.Claims)
                {

                    /*
                     * Claim های اصلی JWT
                     */
                    claims.Add(claim);

                }





                /*
                 * ===============================
                 * Roles
                 * ===============================
                 */


                var roles =
                    jwt.Claims
                    .Where(x =>
                        x.Type.Equals(
                            "role",
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        x.Type.Equals(
                            "roles",
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        x.Type == ClaimTypes.Role)
                    .Select(x => x.Value);



                foreach (var role in roles)
                {

                    if (!claims.Any(x =>
                        x.Type == ClaimTypes.Role &&
                        x.Value == role))
                    {

                        claims.Add(
                            new Claim(
                                ClaimTypes.Role,
                                role));

                    }

                }





                /*
                 * ===============================
                 * Permissions
                 * ===============================
                 */


                var permissions =
                    jwt.Claims
                    .Where(x =>
                        x.Type.Equals(
                            "permission",
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        x.Type.Equals(
                            "permissions",
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();




                foreach (var permission in permissions)
                {

                    /*
                     * اگر permission آرایه JSON باشد
                     *
                     * [
                     * "Users.Create",
                     * "Users.Delete"
                     * ]
                     *
                     */


                    if (permission.Value.StartsWith("["))
                    {

                        try
                        {

                            var items =
                                JsonSerializer
                                .Deserialize<List<string>>(
                                    permission.Value);



                            if (items != null)
                            {

                                foreach (var item in items)
                                {

                                    claims.Add(
                                        new Claim(
                                            "permission",
                                            item));

                                }

                            }

                        }
                        catch
                        {

                        }

                    }
                    else
                    {

                        if (!claims.Any(x =>
       x.Type == "permission" &&
       x.Value == permission.Value))
                        {
                            claims.Add(
                                new Claim(
                                    "permission",
                                    permission.Value));
                        }

                    }


                }





                return claims;

            }
            catch (Exception ex)
            {

                _logger.LogError(
                    ex,
                    "JWT parsing error");


                return claims;

            }

        }






        public void NotifyUserAuthentication(
            string token)
        {

            var user =
                CreateClaimsPrincipal(token);



            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(user)));

        }






        public void NotifyUserLogout()
        {

            var anonymous = new ClaimsPrincipal(
       new ClaimsIdentity());

            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(anonymous)
                ));

        }





        public void NotifyUserChanged()
        {

            NotifyAuthenticationStateChanged(
                GetAuthenticationStateAsync());

        }






        private AuthenticationState Anonymous()
        {

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity()));

        }

    }
}