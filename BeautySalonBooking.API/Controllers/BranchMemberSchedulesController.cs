using BeautySalonBooking.Application.Scheduling.DTOs;
using BeautySalonBooking.Application.Scheduling.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchMemberSchedulesController : ControllerBase
    {
        private readonly IBranchMemberScheduleService _service;
        public BranchMemberSchedulesController(IBranchMemberScheduleService service) => _service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-branch-member/{branchMemberId}")]
        public async Task<IActionResult> GetByBranchMemberId(long branchMemberId, CancellationToken ct)
        {
            var result = await _service.GetByBranchMemberIdAsync(branchMemberId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchMemberScheduleDto dto, CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBranchMemberScheduleDto dto, CancellationToken ct)
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
