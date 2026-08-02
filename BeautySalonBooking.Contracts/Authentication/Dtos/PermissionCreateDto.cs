using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Authentication.Dtos
{
    public class PermissionCreateDto
    {
        public string Title { get; set; } = null!;

        public string Code { get; set; } = null!;

        public byte Type { get; set; }

        public string? Description { get; set; }

        public int? ParentId { get; set; }
    }
}
