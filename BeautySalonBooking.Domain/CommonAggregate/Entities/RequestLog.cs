using BeautySalonBooking.Domain.Base.Entities;
using BeautySalonBooking.Domain.Identity.UserAggregate.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalonBooking.Domain.CommonAggregate.Entities;

public class RequestLog : AuditableEntity<int>
{
    [Required]
    [MaxLength(10)]
    public string HttpMethod { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? IPAddress { get; set; }

    public int? UserId { get; set; }

    [MaxLength(50)]
    public string? UserName { get; set; }

    public DateTime RequestTime { get; set; }
    public DateTime? ResponseTime { get; set; }

    public int StatusCode { get; set; }

    [MaxLength(10)]
    public string? ResponseStatus { get; set; } // Success, Error, Exception

    public long? ExecutionTime { get; set; } // به میلی‌ثانیه

    [MaxLength(1000)] // کاهش سایز
    public string? RequestHeaders { get; set; }

    [MaxLength(4000)] // کاهش سایز
    public string? RequestBody { get; set; }

    [MaxLength(4000)] // کاهش سایز
    public string? ResponseBody { get; set; }

    [MaxLength(500)]
    public string? ExceptionMessage { get; set; }

    [MaxLength(2000)]
    public string? ExceptionStackTrace { get; set; }

    [MaxLength(100)]
    public string? ExceptionType { get; set; }

    // فیلدهای ControllerName و ActionName حذف شدند

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}

