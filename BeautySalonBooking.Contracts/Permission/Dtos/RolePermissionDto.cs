using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Permission.Dtos;

public class RolePermissionDto
{

    public int Id { get; set; }


    public string Title { get; set; } = null!;


    public string Code { get; set; } = null!;


    public int Type { get; set; }


    public bool IsSelected { get; set; }

}
