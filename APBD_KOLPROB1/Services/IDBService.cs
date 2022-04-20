using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD_KOLPROB1.Services
{
    public interface IDBService
    {
        Task<IEnumerable<Prescription>> GetPrescriptionListAsync();
        Task<IEnumerable<Prescription>> GetPrescriptionListAsync(string lastName);
        Task<int> AddNewMedicamentToPrescription(int prescriptionId, IEnumerable<PrescriptionMedicament> prescriptionMedicamentList);
    }
}
