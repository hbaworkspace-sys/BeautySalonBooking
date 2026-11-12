using BeautySalonBooking.Application.Request.DTOs;
using BeautySalonBooking.Application.Request.Interfaces;
using BeautySalonBooking.Application.Requests.Services;
using BeautySalonBooking.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypesController : ControllerBase
{
    private readonly IDocumentTypeService _service;
    public DocumentTypesController(IDocumentTypeService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var data = await _service.GetAllAsync(ct);
        return Ok(ApiResponse_New<List<DocumentTypeResponseDto>>.SuccessResponse(data, "لیست انواع مدرک دریافت شد."));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentTypeDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _service.CreateAsync(dto, ct);
            return Ok(ApiResponse_New<int>.CreatedResponse(id, "نوع مدرک ایجاد شد."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse_New<int>.FailureResponse(ex.Message));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDocumentTypeDto dto, CancellationToken ct)
    {
        try
        {
            await _service.UpdateAsync(dto, ct);
            return Ok(ApiResponse.SuccessResponse("ویرایش انجام شد."));
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
            return Ok(ApiResponse.SuccessResponse("حذف انجام شد."));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse.FailureResponse(ex.Message));
        }
    }
}