using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

using APBD_KOLPROB1.Services;

namespace APBD_KOLPROB1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrescriptionController : Controller
    {

        private readonly IDBService _dbService;
        public PrescriptionController(IDBService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IList<Prescription>> GetPrescriptionList()
        {
            return await _dbService.GetPrescriptionListAsync();
        }

        [HttpGet("{lastName}")]
        public async Task<IList<Prescription>> GetPrescriptionList(string lastName)
        {
            return await _dbService.GetPrescriptionListAsync(lastName);
        }

        [HttpPost]
        public async void AddNewMedicament()
        {
        }


    }
}
