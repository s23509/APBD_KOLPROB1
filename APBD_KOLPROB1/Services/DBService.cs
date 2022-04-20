using System.Collections.Generic;
using System.Threading.Tasks;

namespace APBD_KOLPROB1.Services
{
    public class DBService : IDBService
    {

        public Task<IList<Prescription>> GetPrescriptionListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Prescription>> GetPrescriptionListAsync(string lastName)
        {
            throw new System.NotImplementedException();
        }
    }
}