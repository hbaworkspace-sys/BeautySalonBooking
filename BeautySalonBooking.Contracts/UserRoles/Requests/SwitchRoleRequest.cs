using BeautySalonBooking.Contracts.Common.Enums;

namespace BeautySalonBooking.Contracts.UserRoles.Requests
{
     public class SwitchRoleRequest
     {
        public UserTypeDto Role { get; init; } = default!;
    }
}