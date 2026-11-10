using BeautySalonBooking.Application.Branch.Branch.Interfaces;
using BeautySalonBooking.Contracts.Branch.Branch.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchMembersController : ControllerBase
    {
        private readonly IBranchMemberAppService _service;
        public BranchMembersController(IBranchMemberAppService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] GetBranchMembersRequest request, CancellationToken ct)
        {
            var result = await _service.GetFilteredAsync(request, ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken ct)
        {
            var result = await _service.GetByIdAsync(id, ct);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBranchMemberDto dto, CancellationToken ct)
        {
            var result = await _service.CreateAsync(dto, ct);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBranchMemberDto dto, CancellationToken ct)
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
