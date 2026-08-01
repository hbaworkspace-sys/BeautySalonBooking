namespace BeautySalonBooking.WebApp.Settings
{
    public class EndpointSettings
    {
        public AuthenticationEndpoints Authentication { get; set; } = new();
        public UserEndpoints User { get; set; } = new();
        public RolesEndpoints Roles { get; set; } = new(); // اضافه شده

    }
}
