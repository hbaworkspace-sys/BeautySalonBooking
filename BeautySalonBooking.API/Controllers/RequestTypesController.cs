using BeautySalonBooking.Application.Request.DTOs;
using BeautySalonBooking.Application.Request.Interfaces;
using BeautySalonBooking.Application.Requests.Services;
using BeautySalonBooking.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestTypesController : ControllerBase
{
    private readonly IRequestTypeService _service;
    public RequestTypesController(IRequestTypeService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var data = await _service.GetAllAsync(ct);
        return Ok(ApiResponse_New<List<RequestTypeResponseDto>>.SuccessResponse(data, "لیست انواع درخواست دریافت شد."));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestTypeDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _service.CreateAsync(dto, ct);
            return Ok(ApiResponse_New<int>.CreatedResponse(id, "نوع درخواست ایجاد شد."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse_New<int>.FailureResponse(ex.Message));
        }
    }
}