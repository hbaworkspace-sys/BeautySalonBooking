namespace BeautySalonBooking.WebApp.Settings
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = string.Empty;

        public EndpointSettings Endpoints { get; set; } = new();
    }

}
