using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Auth.Services;
using BeautySalonBooking.Maui.Features.Booking.Services;
using BeautySalonBooking.Maui.Features.Branch.BranchMemberService.Services;
using BeautySalonBooking.Maui.Features.Branch.BranchService.Services;
using BeautySalonBooking.Maui.Features.Category.Services;
using BeautySalonBooking.Maui.Features.Service.Services;
using BeautySalonBooking.Maui.Features.Appointment.Services;
using BeautySalonBooking.Maui.Common.Handlers;

namespace BeautySalonBooking.Maui.Common.DependencyInjection;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services)
    {
        services.AddTransient<AuthTokenHandler>();

        // Refresh Token Client
        services.AddHttpClient("RefreshTokenClient", Configure)
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        // Normal API Clients
        services.AddHttpClient<IAuthApiService, AuthApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        services.AddHttpClient<ICategoryApiService, CategoryApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        services.AddHttpClient<IServiceApiService, ServiceApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        services.AddHttpClient<IBranchServiceApiService, BranchServiceApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        services.AddHttpClient<IBranchMemberServiceApiService, BranchMemberServiceApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        services.AddHttpClient<IBookingApiService, BookingApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        services.AddHttpClient<IAppointmentApiService, AppointmentApiService>(Configure)
            .AddHttpMessageHandler<AuthTokenHandler>()
            .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        return services;
    }
    private async static void Configure(HttpClient client)
    {
        //var response = await client.GetAsync("http://94.183.31.34:4545/swagger/index.html");
        //await Shell.Current.DisplayAlert("نتیجه تست", $"HTTP Status: {(int)response.StatusCode}", "OK");

        client.BaseAddress = new Uri("https://192.168.1.109:7036/");
        //client.Timeout = TimeSpan.FromSeconds(30);

        //client.BaseAddress = new Uri(
        //"http://192.168.1.117:4545/");

        ////client.BaseAddress = new Uri(
        ////"http://94.183.31.34:4545/");
    }

    private static HttpMessageHandler CreateHttpClientHandler()
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    }
}