using BeautySalonBooking.Application.Organization.Interfaces;
using BeautySalonBooking.Contracts.Common;
using BeautySalonBooking.Contracts.Organization.Dtos;
using BeautySalonBooking.Domain.ServiceAggregate.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(
        IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result =
            await _organizationService.GetAlleAsync(
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}/full-detail")]
    public async Task<IActionResult> GetFullDetail(long id, CancellationToken ct)
    {
        try
        {
            var data = await _organizationService.GetFullDetailAsync(id, ct);
            if (data == null)
                return NotFound(ApiResponse_New<OrganizationFullDetailDto>.FailureResponse("سازمان یافت نشد.", 404));

            return Ok(ApiResponse_New<OrganizationFullDetailDto>.SuccessResponse(data, "اطلاعات کامل سازمان دریافت شد."));
        }
        catch (Exception ex)
        {
            var deepest = ex.InnerException?.InnerException?.Message
                          ?? ex.InnerException?.Message
                          ?? ex.Message;
            return BadRequest(ApiResponse_New<OrganizationFullDetailDto>.FailureResponse(deepest));
        }
    }
}