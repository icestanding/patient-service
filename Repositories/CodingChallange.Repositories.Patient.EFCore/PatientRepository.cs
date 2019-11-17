using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingChallange.Repositories.Patient;
using CodingChallange.Shared.Models.Patient;

namespace CodingChallange.Repositories.Patient.EFCore
{
    public class PatientRepository : IPatientRepository
    {
        public Task AddPatientAsync(PatientModel patientModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<PatientModel>> GetAllPatientModelAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PatientModel> GetPatientByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PatientModel> UpdatePatientModelAsync(PatientModel patientModel)
        {
            throw new NotImplementedException();
        }
    }
}
