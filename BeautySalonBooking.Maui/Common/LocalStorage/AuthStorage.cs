namespace BeautySalonBooking.Maui.Common.LocalStorage
{
    public class AuthStorage
    {
        private const string TokenKey = "access_token";
        //private const string TokenKey = "aaa";

        public static async Task SaveTokenAsync(string token)
        {
            await SecureStorage.SetAsync(TokenKey, token);
        }

        public static async Task<string?> GetTokenAsync()
        {
            return await SecureStorage.GetAsync(TokenKey);
        }

        public static void ClearToken()
        {
            SecureStorage.Remove(TokenKey);
        }

        public static async Task<bool> IsLoggedInAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }
    }
}