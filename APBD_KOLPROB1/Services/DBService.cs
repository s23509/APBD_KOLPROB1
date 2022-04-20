using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;
using APBD_KOLPROB1;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<int> AddMedicament(int pId, IEnumerable<Prescription_Medicament> pre_med_list) 
        {
            string sql = "INSERT INTO [Prescription_Medicament] VALUES (@IdMedicament, @IdPrescription, @Dose, @Details)";
            await using SqlConnection readConn = new(conString);
            await readConn.OpenAsync();
            await using SqlCommand cmd = new(sql,readConn);
            cmd.Transaction = (SqlTransaction)await readConn.BeginTransactionAsync();
            int affected = 0;

            try
            {
                foreach (var Prescription_Medicament in pre_med_list)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdMedicament", Prescription_Medicament.IdMedicament);
                    cmd.Parameters.AddWithValue("IdPrescription", Prescription_Medicament.IdPrescription);
                    cmd.Parameters.AddWithValue("Dose", Prescription_Medicament.Dose);
                    cmd.Parameters.AddWithValue("Details", Prescription_Medicament.Details);
                    affected += await cmd.ExecuteNonQueryAsync();
                }
                await cmd.Transaction.CommitAsync();
            }
            catch (SqlException e)
            {
                await cmd.Transaction.RollbackAsync();
                Console.WriteLine(e);
            }
            catch (Exception e) {
                await cmd.Transaction.RollbackAsync();
                Console.WriteLine(e);
            }
            return affected;
        }
    }
}