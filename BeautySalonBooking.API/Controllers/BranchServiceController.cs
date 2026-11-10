using BeautySalonBooking.Application.Branch.BranchService.Interfaces;
using BeautySalonBooking.Contracts.Branch.BranchService.Dtos;
using BeautySalonBooking.Contracts.Branch.BranchService.Requests;
using BeautySalonBooking.Domain.ServiceAggregate.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

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

    [HttpGet]
    public async Task<IActionResult> GetFiltered([FromQuery] GetBranchServicesRequest request, CancellationToken ct)
    {
        var result = await _branchServiceService.GetFilteredAsync(request, ct);
        return Ok(result);
    }

    // 👇 درج
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchServiceRequest request, CancellationToken ct)
    {
        var result = await _branchServiceService.CreateAsync(request, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // 👇 ویرایش
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBranchServiceRequest request, CancellationToken ct)
    {
        var result = await _branchServiceService.UpdateAsync(request, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // 👇 حذف
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        var result = await _branchServiceService.DeleteAsync(id, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}