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
public class RequestsController : ControllerBase
{
    private readonly IRequestService _service;
    public RequestsController(IRequestService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var data = await _service.GetAllAsync(ct);
        return Ok(ApiResponse_New<List<RequestListDto>>.SuccessResponse(data, "لیست درخواست‌ها دریافت شد."));
    }

    [HttpPost("full")]
    public async Task<IActionResult> CreateFullRequest([FromBody] CreateFullRequestDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _service.CreateFullRequestAsync(dto, ct);
            return Ok(ApiResponse_New<long>.CreatedResponse(id, "درخواست با موفقیت ثبت شد."));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse_New<long>.FailureResponse(ex.Message, 401));
        }
        catch (Exception ex)
        {
            var deepestError = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
            return BadRequest(ApiResponse_New<long>.FailureResponse(deepestError));
        }
    }

    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeRequestStatusDto dto, CancellationToken ct)
    {
        try
        {
            await _service.ChangeStatusAsync(dto, ct);
            return Ok(ApiResponse.SuccessResponse("وضعیت درخواست با موفقیت تغییر کرد."));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse.FailureResponse(ex.Message, 401));
        }
        catch (Exception ex)
        {
            var deepestError = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
            return BadRequest(ApiResponse.FailureResponse(deepestError));
        }
    }

    [HttpPost("organization-upgrade")]
    public async Task<IActionResult> CreateOrganizationUpgrade(
    [FromBody] CreateOrganizationUpgradeRequestDto dto, CancellationToken ct)
    {
        try
        {
            var id = await _service.CreateOrganizationUpgradeRequestAsync(dto, ct);
            return Ok(ApiResponse_New<long>.CreatedResponse(id, "درخواست ارتقا به سالن ثبت شد."));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse_New<long>.FailureResponse(ex.Message, 401));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse_New<long>.FailureResponse(ex.Message));
        }
    }

    [HttpPost("organization-upgrade/approve")]
    public async Task<IActionResult> ApproveOrganizationUpgrade(
        [FromBody] ApproveOrganizationUpgradeRequestDto dto, CancellationToken ct)
    {
        try
        {
            await _service.ApproveOrganizationUpgradeRequestAsync(dto, ct);
            return Ok(ApiResponse.SuccessResponse("درخواست تایید و سازمان و شعبه ایجاد شد."));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ApiResponse.FailureResponse(ex.Message, 401));
        }
        catch (Exception ex)
        {
            var deepest = ex.InnerException?.InnerException?.Message
                          ?? ex.InnerException?.Message
                          ?? ex.Message;
            return BadRequest(ApiResponse.FailureResponse(deepest));
        }
    }
}