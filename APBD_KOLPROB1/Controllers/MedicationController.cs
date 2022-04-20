using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APBD_KOLPROB1.Services;
using System.Linq;

namespace APBD_KOLPROB1.Controllers {
    
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController : Controller
    {
        private readonly IDBService _dBService;
        public MedicationController(IDBService dbService)
        {
            _dBService = dbService;
        }

        [HttpPost("{prescription:int}")]
        public async Task<IActionResult> AddMedicament(int prescription, IEnumerable<Prescription_Medicament> Pred_Med_List) 
        {
            int toEdit = await _dBService.AddMedicament(prescription,Pred_Med_List);
            if (toEdit == 0) {
                return BadRequest();
            }
            if (toEdit == Pred_Med_List.Count()) 
            {
                foreach (Prescription_Medicament preMed in Pred_Med_List) 
                {
                    preMed.IdPrescription = prescription;
                    return Created("",Pred_Med_List);
                }
            }

            return Problem();
        }
    }
}