using BeautySalonBooking.Application.Category.Interfaces;
using BeautySalonBooking.Contracts.Category.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("children")]
    public async Task<IActionResult> GetChildren(
        [FromQuery] GetChildCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetChildrenAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}