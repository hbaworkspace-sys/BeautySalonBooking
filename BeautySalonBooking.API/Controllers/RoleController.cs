using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// دریافت لیست نقش‌ها با قابلیت جستجو و پیج‌بندی
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<RoleListResponse>> GetRoles(
            [FromQuery] string? searchTerm = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var request = new RoleSearchRequest
            {
                SearchTerm = searchTerm,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _roleService.GetRolesAsync(request, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// دریافت نقش با شناسه
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);
                return Ok(role);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// ایجاد نقش جدید
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<RoleDto>> CreateRole(
            [FromBody] CreateRoleRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var createdRole = await _roleService.CreateRoleAsync(request, cancellationToken);
                return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// ویرایش نقش
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> UpdateRole(
            int id,
            [FromBody] UpdateRoleRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                // اطمینان از تطابق شناسه
                if (id != request.Id)
                    return BadRequest(new { message = "شناسه در مسیر با شناسه در بدنه درخواست مطابقت ندارد." });

                var updatedRole = await _roleService.UpdateRoleAsync(request, cancellationToken);
                return Ok(updatedRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// حذف نقش
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id, cancellationToken);

                return Ok(true);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

