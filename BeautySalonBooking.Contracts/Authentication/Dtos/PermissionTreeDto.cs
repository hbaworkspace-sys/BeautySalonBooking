using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Contracts.Authentication.Dtos
{
    public class PermissionTreeDto
    {

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; } = null!;


        public string Code { get; set; } = null!;


        public int Type { get; set; }


        public bool HasChildren { get; set; }


    }
}
