using BeautySalonBooking.Contracts.Common.Enums;

namespace BeautySalonBooking.Contracts.UserRoles.Requests
{
     public class AssignRoleRequest
     {
        public UserTypeDto Role { get; init; } = default!;
    }
}