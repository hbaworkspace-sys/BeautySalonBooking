using BeautySalonBooking.Contracts.Common.Enums;

namespace BeautySalonBooking.Contracts.UserRoles.DTOs
{
    public class UserRoleDto
    {
        public UserTypeDto Role { get; init; } = default!;
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
