using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD_KOLPROB1.Services
{
    public interface IDBService
    {

        Task<IList<Prescription>> GetPrescriptionListAsync();
        Task<IList<Prescription>> GetPrescriptionListAsync(string lastName);


    }
}
