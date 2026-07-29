using BeautySalonBooking.Maui.Features.Auth;
using BeautySalonBooking.Maui.Features.Region;
using BeautySalonBooking.Maui.Features.UserRoles;

namespace BeautySalonBooking.Maui.Common.DependencyInjection
{
    public static class ApiDependencyInjection
    {
        //https://192.168.1.100:7081/
        //https://10.0.2.2:7081/
        //https://localhost:7081/
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //static void Configure(HttpClient client)
            //{
            //    client.BaseAddress = new Uri("https://192.168.100.233:7081/");
            //}
            static void Configure(HttpClient client)
            {
                client.BaseAddress = new Uri("https://localhost:7081/");
            }
            services.AddHttpClient<AuthApiService>(Configure);
            services.AddHttpClient<UserRoleApiService>(Configure);
            services.AddHttpClient<RegionApiService>(Configure);

            return services;
        }
    }
}
