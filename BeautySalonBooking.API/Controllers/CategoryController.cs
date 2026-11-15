using BeautySalonBooking.Application.Category.Interfaces;
using BeautySalonBooking.Contracts.Category.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoriesController(ICategoryService service) => _service = service;

    // 👇 لیست ریشه‌ها (برای نمایش درخت)
    [HttpGet("roots")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRoots([FromQuery] GetRootCategoriesRequest request, CancellationToken ct)
    {
        var result = await _service.GetRootsAsync(request, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // 👇 لیست فرزندان یک والد (با کلیک روی نود)
    [HttpGet("children")]
    [AllowAnonymous]
    public async Task<IActionResult> GetChildren([FromQuery] GetChildCategoriesRequest request, CancellationToken ct)
    {
        var result = await _service.GetChildrenAsync(request, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // 👇 درج (ریشه یا فرزند)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // 👇 ویرایش
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest request, CancellationToken ct)
    {
        var result = await _service.UpdateAsync(request, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    // 👇 حذف
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _service.DeleteAsync(id, ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}