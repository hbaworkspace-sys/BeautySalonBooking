using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Permission.Dtos;

public class AssignRolePermissionsRequest
{

    public int RoleId { get; set; }


    public List<int> PermissionIds { get; set; }
        = new();

}
