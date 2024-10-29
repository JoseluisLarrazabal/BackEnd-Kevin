using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CB_Backend_FAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupFABController : ControllerBase
    {
        private readonly IGroupFABService _groupFABService;

        public GroupFABController(IGroupFABService groupFABService)
        {
            _groupFABService = groupFABService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupFAB>>> GetGroupFABs()
        {
            return Ok(await _groupFABService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupFAB>> GetGroupFAB(int id)
        {
            var groupFAB = await _groupFABService.GetByIdAsync(id);
            if (groupFAB == null)
            {
                return NotFound();
            }
            return Ok(groupFAB);
        }


        [HttpPost]
        public async Task<ActionResult<GroupFAB>> PostGroupFAB2([FromBody] GroupFAB groupFAB)
        {
            await _groupFABService.CreateAsync(groupFAB);
            return CreatedAtAction(nameof(GetGroupFAB), new { id = groupFAB.GroupID }, groupFAB);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupFAB(int id, [FromBody] GroupFAB groupFAB)
        {
            GroupFAB newGroupFAB = new GroupFAB(id, groupFAB.Name, groupFAB.Description);
            if (id != groupFAB.GroupID)
            {
                return BadRequest();
            }

            await _groupFABService.UpdateAsync(groupFAB);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupFAB(int id)
        {
            await _groupFABService.DeleteAsync(id);
            return NoContent();
        }
    }

}
