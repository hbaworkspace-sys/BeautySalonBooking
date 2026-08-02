using BeautySalonBooking.WebApp.Authentication;
using BeautySalonBooking.WebApp.Components;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Login;
using BeautySalonBooking.WebApp.Interfaces.Roles;
using BeautySalonBooking.WebApp.Services.Common;
using BeautySalonBooking.WebApp.Services.Login;
using BeautySalonBooking.WebApp.Services.Roles;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;


namespace BeautySalonBooking.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents();


            builder.Services.AddMemoryCache();


            builder.Services.Configure<ApiSettings>(
                builder.Configuration.GetSection("ApiSettings"));



            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            });



            builder.Services.AddMudServices();



            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddScoped<ISessionManager, SessionManager>();

            builder.Services.AddScoped<ILoginService, LoginService>();

            builder.Services.AddScoped<IRoleService, RoleService>();


            // ==========================
            // Server Authentication
            // ==========================

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                });



            builder.Services.AddAuthorization();



            // ==========================
            // Blazor Authentication
            // ==========================

            builder.Services.AddAuthorizationCore();


            builder.Services.AddCascadingAuthenticationState();


            builder.Services.AddScoped<CustomAuthenticationStateProvider>();


            builder.Services.AddScoped<AuthenticationStateProvider>(
                sp =>
                sp.GetRequiredService<CustomAuthenticationStateProvider>());



            var app = builder.Build();



            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();


            app.UseStaticFiles();


            app.UseAuthentication();

            app.UseAuthorization();


            app.UseAntiforgery();



            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();



            app.Run();

        }
    }
}