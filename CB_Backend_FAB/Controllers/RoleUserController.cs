using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB_Backend_FAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly IRoleUserService _roleUserService;

        public RoleUserController(IRoleUserService roleUserService)
        {
            _roleUserService = roleUserService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleUser>>> GetRoleUsers()
        {
            return Ok(await _roleUserService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleUser>> GetRoleUser(int id)
        {
            var roleUser = await _roleUserService.GetByIdAsync(id);
            if (roleUser == null)
            {
                return NotFound();
            }
            return Ok(roleUser);
        }

        [HttpPost]
        public async Task<ActionResult<RoleUser>> PostRoleUser([FromBody] RoleUser roleUser)
        {
            await _roleUserService.CreateAsync(roleUser);
            return CreatedAtAction(nameof(GetRoleUser), new { id = roleUser.RoleID }, roleUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleUser(int id, [FromBody] RoleUser roleUser)
        {
            RoleUser newRoleUser = new RoleUser(id , roleUser.Name, roleUser.Description);
            if (id != roleUser.RoleID)
            {
                return BadRequest();
            }

            await _roleUserService.UpdateAsync(roleUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleUser(int id)
        {
            await _roleUserService.DeleteAsync(id);
            return NoContent();
        }
    }
}
