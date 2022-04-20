using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace APBD_KOLPROB1.Services
{
    public class DBService : IDBService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";
        public async Task<IEnumerable<Prescription>> GetPrescriptionListAsync()
        {

            string sql = "SELECT * FROM Prescription ORDER BY Date DESC";
            List<Prescription> result = new();

            await using SqlConnection connection = new(ConString);
            await using SqlCommand command = new(sql, connection);

            await connection.OpenAsync();
            await using SqlDataReader dr = await command.ExecuteReaderAsync();

            while(await dr.ReadAsync())
            {

                result.Add(new Prescription
                {
                    IdPrescription = int.Parse(dr["IdPrescription"].ToString()),
                    Date = DateTime.Parse(dr["Date"].ToString()),
                    DueDate = DateTime.Parse(dr["DueDate"].ToString()),
                    IdPatient = int.Parse(dr["IdPatient"].ToString()),
                    IdDoctor = int.Parse(dr["IdDoctor"].ToString())
                });

            }

            await connection.CloseAsync();
            return result;

        }
        public async Task<IEnumerable<Prescription>> GetPrescriptionListAsync(string lastName)
        {

            /* string sql = "SELECT * FROM Prescription prescription " +
                    "JOIN Patient patient ON prescription.IdPatient = patient.IdPatient " +
                    $"WHERE patient.LastName = '{lastName}' " +
                    "ORDER BY Date DESC"; */

            string sql = "SELECT [Prescription].* FROM [Prescription] JOIN [Patient] ON [Prescription].[IdPatient] = [Patient].[IdPatient] WHERE LastName = @LASTNAME";

            List<Prescription> result = new();

            await using SqlConnection connection = new(ConString);
            await using SqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("LASTNAME", lastName);

            await connection.OpenAsync();
            await using SqlDataReader dr = await command.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {

                result.Add(new Prescription
                {
                    IdPrescription = int.Parse(dr["IdPrescription"].ToString()),
                    Date = DateTime.Parse(dr["Date"].ToString()),
                    DueDate = DateTime.Parse(dr["DueDate"].ToString()),
                    IdPatient = int.Parse(dr["IdPatient"].ToString()),
                    IdDoctor = int.Parse(dr["IdDoctor"].ToString())
                });

            }

            await connection.CloseAsync();
            return result;

        }
        public async Task<int> AddNewMedicamentToPrescription(int prescriptionId, IEnumerable<PrescriptionMedicament> prescriptionMedicamentList)
        {

            /*string sql = "INSERT INTO Prescription_Medicament VALUES(" + 
            $"{prescriptionMedicament.IdMedicament}, {prescriptionMedicament.IdPrescription}, " +
            $"{prescriptionMedicament.Dose}, {prescriptionMedicament.Details})";*/

            string sql = "INSERT INTO [Prescription_Medicament] VALUES (@IdMedicament, @IdPrescription, @Dose, @Details)";
            
            await using SqlConnection connection = new(ConString);
           
            await connection.OpenAsync();

            await using SqlCommand command = new(sql, connection);
            command.Transaction = (SqlTransaction)await connection.BeginTransactionAsync();

            int affectedRows = 0;

            try
            {
                foreach (var prescriptionMedicament in prescriptionMedicamentList)
                {

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("IdMedicament", prescriptionMedicament.IdMedicament);
                    command.Parameters.AddWithValue("IdPrescription", prescriptionId);
                    command.Parameters.AddWithValue("Dose", prescriptionMedicament.Dose);
                    command.Parameters.AddWithValue("Details", prescriptionMedicament.Details);
                    affectedRows += await command.ExecuteNonQueryAsync();

                }

                await command.Transaction.CommitAsync();

            }
            catch(SqlException e)
            {
                await command.Transaction.RollbackAsync();
                Console.WriteLine(e);
            }
            catch(Exception e)
            {
                await command.Transaction.RollbackAsync();
                Console.WriteLine(e);
            }

            return affectedRows;

        }
    }
}