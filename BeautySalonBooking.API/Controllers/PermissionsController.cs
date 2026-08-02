using BeautySalonBooking.Application.Authentication.Interfaces;
using BeautySalonBooking.Contracts.Authentication.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalonBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PermissionsController : ControllerBase
    {

        private readonly IPermissionService _service;


        public PermissionsController(
            IPermissionService service)
        {
            _service = service;
        }



        [HttpGet("roots")]
        public async Task<IActionResult> Roots(
            CancellationToken cancellationToken)
        {
            return Ok(
                await _service.GetRootPermissionsAsync(cancellationToken)
            );
        }



        [HttpGet("children/{parentId}")]
        public async Task<IActionResult> Children(
            int parentId,
            CancellationToken cancellationToken)
        {
            return Ok(
                await _service.GetChildrenAsync(
                    parentId,
                    cancellationToken)
            );
        }



        [HttpPost]
        public async Task<IActionResult> Create(
            PermissionCreateDto dto,
            CancellationToken cancellationToken)
        {
            return Ok(await _service.CreateAsync(dto, cancellationToken));
        }



        [HttpPut]
        public async Task<IActionResult> Update(PermissionUpdateDto dto, CancellationToken cancellationToken)
        {
            return Ok(
                await _service.UpdateAsync(
                    dto,
                    cancellationToken)
            );
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            return Ok(
                await _service.DeleteAsync(
                    id,
                    cancellationToken)
            );
        }
    }
}
