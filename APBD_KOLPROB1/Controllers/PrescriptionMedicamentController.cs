using Microsoft.AspNetCore.Mvc;
using APBD_KOLPROB1.Services;
using System.Collections.Generic;

namespace APBD_KOLPROB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrescriptionMedicamentController : ControllerBase
    {

        private readonly IDBService _dbService;
        public PrescriptionMedicamentController(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async void AddNewMedicamentToPrescription(List<PrescriptionMedicament> prescriptionMedicamentList)
        {
            await _dbService.AddNewMedicamentToPrescription(prescriptionMedicamentList);
        }




    }
}
