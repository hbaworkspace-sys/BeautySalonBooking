using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{
    public class UpdateDocumentTypeDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Title { get; set; } = null!;
        [Required] public string Code { get; set; } = null!;
        public string? Description { get; set; }
    }
}
