using BeautySalonBooking.Application.Branch.BranchService.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchService.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BranchServiceController : ControllerBase
{
    private readonly IBranchServiceService _branchServiceService;

    public BranchServiceController(
        IBranchServiceService branchServiceService)
    {
        _branchServiceService = branchServiceService;
    }

    [HttpGet("by-service")]
    public async Task<IActionResult> GetOrganizationsByService(
        [FromQuery] GetBranchesByServiceRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _branchServiceService.GetBranchesByServiceAsync(
                request,
                cancellationToken);

        return Ok(result);
    }
}