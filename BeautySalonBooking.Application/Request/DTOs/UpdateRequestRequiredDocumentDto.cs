using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{
    public class UpdateRequestRequiredDocumentDto
    {
        [Required] public int Id { get; set; }
        [Required] public bool IsRequired { get; set; }
        [Required] public int DisplayOrder { get; set; }
    }

}
