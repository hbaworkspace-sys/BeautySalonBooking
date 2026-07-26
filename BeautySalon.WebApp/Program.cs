using BeautySalonBooking.WebApp.Components;
using BeautySalonBooking.WebApp.Interfaces.Common;
using BeautySalonBooking.WebApp.Interfaces.Login;
using BeautySalonBooking.WebApp.Services.Common;
using BeautySalonBooking.WebApp.Services.Login;
using BeautySalonBooking.WebApp.Settings;
using Microsoft.AspNetCore.Components.Authorization;

namespace BeautySalonBooking.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMemoryCache();
            // 1️⃣ ثبت تنظیمات
            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

            // 2️⃣ ثبت HttpClient
            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(30);
            });
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            // تنظیم HttpClient Factory
            builder.Services.AddHttpClient("BeautySalonAPI", client =>
            {
                var apiUrl = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7036/";
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "BeautySalonBooking");
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
