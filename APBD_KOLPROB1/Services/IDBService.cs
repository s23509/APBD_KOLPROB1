using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace APBD_KOLPROB1.Services
{
    public interface IDBService
    {

        Task<IList<Prescription>> GetPrescriptionListAsync();
        Task<IList<Prescription>> GetPrescriptionListAsync(string lastName);
        Task<int> AddMedicament(int pId, IEnumerable<Prescription_Medicament> pre_med_list);
    }
}
