using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;

namespace APBD_KOLPROB1.Services
{
    public class DBService : IDBService
    {
        private const string conString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=true";

        public async Task<IList<Prescription>> GetPrescriptionListAsync()
        {
            string sql = "SELECT * FROM Prescription p" +
                        " ORDER BY Date DESC";
            List<Prescription> result = new();
            using var connection = new SqlConnection(conString);
            using var cmd = new SqlCommand(sql,connection);

            await connection.OpenAsync();
            using var dReader = await cmd.ExecuteReaderAsync();

            while (await dReader.ReadAsync()) 
            {
                result.Add(new Prescription
                {
                    IdPrescription = int.Parse(dReader["IdPrescription"].ToString()),
                    Date = DateTime.Parse(dReader["Date"].ToString()),
                    DueDate = DateTime.Parse(dReader["DueDate"].ToString()),
                    IdPatient = int.Parse(dReader["IdPatient"].ToString()),
                    IdDoctor = int.Parse(dReader["IdDoctor"].ToString())
                });
            }
            await connection.CloseAsync();
            return result;
        }

        public async Task<IList<Prescription>> GetPrescriptionListAsync(string lastName)
        {
            string sql = "SELECT * FROM Prescription p " +
                " JOIN Patient pat ON p.IdPatient= pat.IdPatient" +
                $" WHERE pat.LastName = {lastName} " +
                " ORDER BY Date DESC";
            List<Prescription> result = new();
            await using SqlConnection connection = new SqlConnection(conString);
            using var cmd = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            using var dReader = await cmd.ExecuteReaderAsync();

            while (await dReader.ReadAsync())
            {
                result.Add(new Prescription
                {
                    IdPrescription = int.Parse(dReader["IdPrescription"].ToString()),
                    Date = DateTime.Parse(dReader["Date"].ToString()),
                    DueDate = DateTime.Parse(dReader["DueDate"].ToString()),
                    IdPatient = int.Parse(dReader["IdPatient"].ToString()),
                    IdDoctor = int.Parse(dReader["IdDoctor"].ToString())
                });
            }
            await connection.CloseAsync();
            return result;
        }
    }
}