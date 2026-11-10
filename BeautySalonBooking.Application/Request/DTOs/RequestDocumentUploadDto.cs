using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonBooking.Application.Request.DTOs
{
    public class RequestDocumentUploadDto
    {
        [Required] public int DocumentTypeId { get; set; }
        [Required] public string FileName { get; set; } = null!;
        [Required] public string ContentType { get; set; } = null!;
        [Required] public string Base64Content { get; set; } = null!;
    }
}
