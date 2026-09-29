using BeautySalonBooking.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExceptionTestController : ControllerBase
{
    [HttpGet("not-found")]
    public IActionResult TestNotFound()
    {
        throw new NotFoundException("کاربر مورد نظر پیدا نشد.");
    }

    [HttpGet("conflict")]
    public IActionResult TestConflict()
    {
        throw new ConflictException("این شماره موبایل قبلاً ثبت شده است.");
    }

    [HttpGet("internal-error")]
    public IActionResult TestInternalError()
    {
        throw new Exception("Test internal server error.");
    }
}