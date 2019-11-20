using CodingChallange.Shared.Models.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallange.Repositories.Patient
{
    public interface IPatientRepository
    {
        Task<PatientModel> AddPatientAsync(PatientModel patientModel);
        Task<PatientModel> GetPatientByIdAsync(Guid Id);
        Task<PatientModel> UpdatePatientAsync(PatientModel patientModel);
        IQueryable<PatientModel> GetQueryablePatient();
        bool SaveChanges();
    }
}
