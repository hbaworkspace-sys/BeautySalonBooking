using BeautySalonBooking.Contracts.Common.Enums;

namespace BeautySalonBooking.Contracts.UserRoles.Responses
{
     public class ActiveRoleResponse
     {
        public UserTypeDto Role { get; init; } = default!;
     }
}