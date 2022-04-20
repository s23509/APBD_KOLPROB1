using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;

namespace APBD_KOLPROB1.Services
{
    public class DBService : IDBService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True";
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptionListAsync()
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
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptionListAsync(string lastName)
        {

            string sql = "SELECT * FROM Prescription prescription " +
                " JOIN Patient patient ON prescription.IdPatient = patient.IdPatient " +
               $" WHERE patient.LastName = '{lastName}'" +
                " ORDER BY Date DESC";

            List<Prescription> result = new();

            await using SqlConnection connection = new(ConString);
            await using SqlCommand command = new(sql, connection);

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
        public async Task AddNewMedicamentToPrescription(List<PrescriptionMedicament> prescriptionMedicamentList)
        {
            
            foreach(var prescriptionMedicament in prescriptionMedicamentList)
            {

                string sql = "INSERT INTO Prescription_Medicament VALUES(" + 
                    $"{prescriptionMedicament.IdMedicament}, {prescriptionMedicament.IdPrescription}, " +
                    $"{prescriptionMedicament.Dose}, {prescriptionMedicament.Details})";

                await using SqlConnection connection = new(ConString);
                await using SqlCommand command = new(sql, connection);

                await connection.OpenAsync();

                //???
                await command.ExecuteNonQueryAsync();
               

            }

            

        }
    }
}