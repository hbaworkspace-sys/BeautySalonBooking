//using BeautySalonBooking.Maui.Features.Auth;
//using BeautySalonBooking.Maui.Features.Auth.Services;

//namespace BeautySalonBooking.Maui.Common.DependencyInjection;

//public static class ApiDependencyInjection
//{
//    public static IServiceCollection AddApiServices(this IServiceCollection services)
//    {
//        //static void Configure(HttpClient client)
//        //{
//        //    //client.BaseAddress = new Uri("https://localhost:7036/");
//        //    client.BaseAddress = new Uri("http://192.168.1.100:7036/");
//        //    client.Timeout = TimeSpan.FromSeconds(30);
//        //}
//        //services.AddHttpClient<IAuthApiService, AuthApiService>(Configure);

//        services.AddHttpClient<IAuthApiService, AuthApiService>(client =>
//        {
//            client.BaseAddress = new Uri("https://192.168.1.100:7036/");
//        })
//            .ConfigurePrimaryHttpMessageHandler(() =>
//            {
//                return new HttpClientHandler
//                {
//                    ServerCertificateCustomValidationCallback =
//                        (message, cert, chain, errors) => true
//                };
//            });
//        return services;
//    }
//}

using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Auth.Services;

namespace BeautySalonBooking.Maui.Common.DependencyInjection;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthApiService, AuthApiService>(Configure)
                .ConfigurePrimaryHttpMessageHandler(CreateHttpClientHandler);

        return services;
    }

    private static void Configure(HttpClient client)
    {
        client.BaseAddress = new Uri("https://192.168.1.103:7036/");
        client.Timeout = TimeSpan.FromSeconds(30);
    }

    private static HttpMessageHandler CreateHttpClientHandler()
    {

        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        return new HttpClientHandler();

    }
}
