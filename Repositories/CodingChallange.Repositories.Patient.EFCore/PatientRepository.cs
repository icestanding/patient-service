using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChallange.Repositories.Patient;
using CodingChallange.Shared.Models.Patient;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace CodingChallange.Repositories.Patient.EFCore
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;

        public PatientRepository(PatientDbContext context) 
        {
            _context = context;
        }

        private static void UpdateAuditColumns(PatientModel entity)
        {
            if (entity.CreateTime == DateTime.MinValue)
            {
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = entity.CreateTime;
            }
            else
            {
                entity.UpdateTime = DateTime.UtcNow;
            }
        }

        public async Task<PatientModel> GetPatientByIdAsync(Guid id)
        {
            if (id == null || id == Guid.Empty) {
                return null;
            }
            return await _context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<PatientModel> GetQueryablePatient()
        {
            return _context.Patients.AsNoTracking();
        }

        public async Task<PatientModel> AddPatientAsync(PatientModel patientModel)
        {
            patientModel.Id = new Guid();
            UpdateAuditColumns(patientModel);

            return (await _context.Patients.AddAsync(patientModel)).Entity;
        }

        public async Task<PatientModel> UpdatePatientAsync(PatientModel patientModel)
        {
            var targetPatientModel = await GetPatientByIdAsync(patientModel.Id);

            if (targetPatientModel == null) {
                return null;
            }
            targetPatientModel = patientModel;
            UpdateAuditColumns(targetPatientModel);

            return  _context.Patients.Update(targetPatientModel).Entity;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}
