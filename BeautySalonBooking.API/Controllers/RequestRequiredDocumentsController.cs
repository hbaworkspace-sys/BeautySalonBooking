using BeautySalonBooking.Application.Request.DTOs;
using BeautySalonBooking.Application.Request.Interfaces;
using BeautySalonBooking.Application.Requests.Services;
using BeautySalonBooking.Contracts.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RequestRequiredDocumentsController : ControllerBase
{
    private readonly IRequestRequiredDocumentService _service;
    public RequestRequiredDocumentsController(IRequestRequiredDocumentService service) => _service = service;

    [HttpGet("by-request-type/{requestTypeId}")]
    public async Task<IActionResult> GetByRequestType(int requestTypeId, CancellationToken ct)
    {
        var data = await _service.GetByRequestTypeIdAsync(requestTypeId, ct);
        return Ok(ApiResponse_New<List<RequestRequiredDocumentResponseDto>>.SuccessResponse(data, "لیست مدارک مورد نیاز دریافت شد."));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestRequiredDocumentDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _service.CreateAsync(dto, ct);
            return Ok(ApiResponse_New<int>.CreatedResponse(id, "مدرک مورد نیاز با موفقیت ایجاد شد."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse_New<int>.FailureResponse(ex.Message));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRequestRequiredDocumentDto dto, CancellationToken ct)
    {
        try
        {
            await _service.UpdateAsync(dto, ct);
            return Ok(ApiResponse.SuccessResponse("مدرک مورد نیاز ویرایش شد."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.FailureResponse(ex.Message));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        try
        {
            await _service.DeleteAsync(id, ct);
            return Ok(ApiResponse.SuccessResponse("مدرک مورد نیاز حذف شد."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.FailureResponse(ex.Message));
        }
    }
}