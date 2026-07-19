namespace BeautySalonBooking.Maui.Features.AccountSetup.Constants
{
    public static class UserRoleRoutes
    {
        public static string AssignRole(Guid userId)=> $"api/user-roles/{userId}/assign";
        public static string SwitchRole(Guid userId)=> $"api/user-roles/{userId}/switch";
        public static string GetUserRoles(Guid userId)=> $"api/user-roles/{userId}";
        public static string GetActiveRole(Guid userId)=> $"api/user-roles/{userId}/active";
    }
}

