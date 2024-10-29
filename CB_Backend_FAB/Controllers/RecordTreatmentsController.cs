using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CB_Backend_FAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordTreatmentsController : ControllerBase
    {
        private readonly IRecordTreatmentsService _recordTreatmentsService;

        public RecordTreatmentsController(IRecordTreatmentsService recordTreatmentsService)
        {
            _recordTreatmentsService = recordTreatmentsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordTreatments>>> Get()
        {
            return Ok(await _recordTreatmentsService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecordTreatments>> Get(int id)
        {
            var recordTreatments = await _recordTreatmentsService.GetByIdAsync(id);
            if (recordTreatments == null)
            {
                return NotFound();
            }
            return Ok(recordTreatments);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RecordTreatments recordTreatments)
        {
            try
            {
                await _recordTreatmentsService.CreateAsync(recordTreatments);
                return Ok(new { message = "OK" }); // Devuelve un objeto JSON en lugar de texto plano
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


    }
}
