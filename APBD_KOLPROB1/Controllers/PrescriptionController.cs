using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using APBD_KOLPROB1.Services;
using System.Linq;

namespace APBD_KOLPROB1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {

        private readonly IDBService _dbService;
        public PrescriptionController(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescriptionList()
        {
            return Ok(await _dbService.GetPrescriptionListAsync());
        }

        [HttpGet("{lastName}")]
        public async Task<IActionResult> GetPrescriptionList(string lastName)
        {
            var result = await _dbService.GetPrescriptionListAsync(lastName);
            if(!result.Any())
                return NotFound("No prescription for patient with that last name.");
            return Ok(result);

        }

    }
}
