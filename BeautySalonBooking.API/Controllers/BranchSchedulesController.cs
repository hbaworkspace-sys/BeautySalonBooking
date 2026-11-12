using BeautySalonBooking.Application.Scheduling.DTOs;
using BeautySalonBooking.Application.Scheduling.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchSchedulesController : ControllerBase
    {
        private readonly IBranchScheduleService _service;
        public BranchSchedulesController(IBranchScheduleService service) => _service = service;

        [HttpGet("by-branch/{branchId}")]
        public async Task<IActionResult> GetByBranchId(long branchId, CancellationToken ct)
        {
            var result = await _service.GetByBranchIdAsync(branchId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchScheduleDto dto, CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBranchScheduleDto dto, CancellationToken ct)
        {
            var result = await _service.UpdateAsync(dto, ct);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken ct)
        {
            var result = await _service.DeleteAsync(id, ct);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
