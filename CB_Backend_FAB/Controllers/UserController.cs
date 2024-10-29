using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CB_Backend_FAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usuarioService;

        public UserController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsuarios()
        {
            return Ok(await _usuarioService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsuario(int id)
        {
            var user = await _usuarioService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUsuario([FromBody] User user)
        {
            await _usuarioService.CreateAsync(user);
            return CreatedAtAction(nameof(GetUsuario), new { id = user.UserID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] User user)
        {
            User usuario = new User(id, user.Email, user.Password, user.Role, user.Group);
            if (id != usuario.UserID)
            {
                return BadRequest();
            }

            await _usuarioService.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
    }

}
