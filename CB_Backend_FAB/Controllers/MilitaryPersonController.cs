using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CB_Backend_FAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilitaryPersonController : ControllerBase
    {
        private readonly IMilitaryPersonService _militaryPerson;

        public MilitaryPersonController(IMilitaryPersonService militaryPersonService)
        {
            _militaryPerson = militaryPersonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilitaryPerson>>> Get()
        {
            return Ok(await _militaryPerson.GetAllAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MilitaryPerson>> Get(int id)
        {
            return Ok(await _militaryPerson.GetByIdAsync(id));
        }
       

        /// <summary>
        /// Transacción simple
        /// </summary>
        /// <param name="militaryPerson"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] MilitaryPerson militaryPerson)
        {
            try
            {
                await _militaryPerson.CreateAsync(militaryPerson);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error : {ex.Message}");
            }
        }
    }
}
