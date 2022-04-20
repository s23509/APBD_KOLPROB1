using Microsoft.AspNetCore.Mvc;
using APBD_KOLPROB1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace APBD_KOLPROB1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionMedicamentController : ControllerBase
    {

        private readonly IDBService _dbService;
        public PrescriptionMedicamentController(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("{prescriptionId:int}")]
        public async Task<IActionResult> AddNewMedicamentToPrescription(int prescriptionId, IEnumerable<PrescriptionMedicament> prescriptionMedicamentList)
        {
            int affected = await _dbService.AddNewMedicamentToPrescription(prescriptionId, prescriptionMedicamentList);
            if (affected == 0)
                return BadRequest();
            if(affected == prescriptionMedicamentList.Count())
            {
                foreach (PrescriptionMedicament prescriptionMedicament in prescriptionMedicamentList)
                    prescriptionMedicament.IdPrescription = prescriptionId;
                return Created("", prescriptionMedicamentList);

            }

            return Problem();

        }

    }
}
