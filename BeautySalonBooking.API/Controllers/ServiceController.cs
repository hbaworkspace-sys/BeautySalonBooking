using BeautySalonBooking.Application.Service.Interfaces;
using BeautySalonBooking.Contracts.Service.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet("by-category")]
    public async Task<IActionResult> GetByCategory(
        [FromQuery] GetServicesByCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _serviceService.GetByCategoryAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}