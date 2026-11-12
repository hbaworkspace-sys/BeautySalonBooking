namespace BeautySalonBooking.Contracts.Branch.BranchService.Dtos;

public class BranchServiceDto
{
    public long Id { get; set; }
    public long BranchId { get; set; }
    public string BranchTitle { get; set; } = null!;
    public long ServiceId { get; set; }
    public string ServiceTitle { get; set; } = null!;
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}