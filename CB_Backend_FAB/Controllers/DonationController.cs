using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CB_Backend_FAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {

        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            _donationService = donationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> Get()
        {
            return Ok(await _donationService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MilitaryPerson>> Get(int id)
        {
            return Ok(await _donationService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Donation donation)
        {
            try
            {
                await _donationService.CreateAsync(donation);
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error : {ex.Message}");
            }
        }
    }
}
