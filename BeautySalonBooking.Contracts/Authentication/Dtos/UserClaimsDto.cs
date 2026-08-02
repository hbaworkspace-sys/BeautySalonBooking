using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Authentication.Dtos
{
    public sealed class UserClaimsDto
    {
        public long UserId { get; set; }

        public long PersonId { get; set; }

        public string UserName { get; set; } = "";

        public string FullName { get; set; } = "";

        public string Email { get; set; } = "";

        public int AuthenticationType { get; set; }

        public int RoleId { get; set; }

        public string RoleTitle { get; set; } = "";

        public string RoleCode { get; set; } = "";

        public List<PermissionClaimDto> Permissions { get; set; } = new();
    }

    public sealed class PermissionClaimDto
    {
        public string Code { get; set; } = "";

        public string Title { get; set; } = "";

        public int Type { get; set; }
    }
}
