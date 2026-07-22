using BeautySalonBooking.Domain.Identity.AuthenticationAggregate.Entities;
using BeautySalonBooking.Domain.OTP;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public class OtpRepository : IOtpRepository
{
    private readonly BeautyDbContext _context;

    public OtpRepository(BeautyDbContext context)
    {
        _context = context;
    }
    public async Task<OtpCode?> GetLatestAsync(string mobileNumber, CancellationToken cancellationToken)
    {
        return await _context.OtpCodes.Where(x =>
        x.MobileNumber == mobileNumber)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task AddAsync(OtpCode otp, CancellationToken cancellationToken)
    {
        await _context.OtpCodes.AddAsync(otp);
    }

    public async Task UpdateAsync(OtpCode otp, CancellationToken cancellationToken)
    {
        _context.OtpCodes.Update(otp);
    }
}