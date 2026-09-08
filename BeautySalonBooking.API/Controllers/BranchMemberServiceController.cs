using BeautySalonBooking.Application.Branch.BranchMemberService.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchMemberService.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BranchMemberServiceController : ControllerBase
{
    private readonly IBranchMemberServiceService _branchMemberService;

    public BranchMemberServiceController(
        IBranchMemberServiceService branchMemberService)
    {
        _branchMemberService = branchMemberService;
    }

    [HttpGet("by-branch-and-service")]
    public async Task<IActionResult> Get(
        [FromQuery] GetMembersByBranchAndServiceRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _branchMemberService.GetMembersByBranchAndServiceAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}