using BeautySalonBooking.Application.Organization.Interfaces;
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
}