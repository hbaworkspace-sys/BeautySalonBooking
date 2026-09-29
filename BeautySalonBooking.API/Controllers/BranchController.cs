using BeautySalonBooking.Application.Branch.Branch.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BranchController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(
       IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result =
            await _branchService.GetAllAsync(
                cancellationToken);

        return Ok(result);
    }
}