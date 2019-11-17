using CodingChallange.Shared.Models.Patient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallange.Repositories.Patient
{
    public interface IPatientRepository
    {
        Task AddPatientAsync(PatientModel patientModel);
        Task<PatientModel> UpdatePatientModelAsync(PatientModel patientModel);
        Task<PatientModel> GetPatientByIdAsync(Guid id);
        Task<List<PatientModel>> GetAllPatientModelAsync();



    }
}
