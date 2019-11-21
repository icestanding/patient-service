using CodingChallange.Repositories.Patient;
using CodingChallange.Shared.Models.Patient;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CodingChallange.Shared.Models.Pagination;
using CodingChallange.Services.Patient.Sieve;

namespace CodingChallange.Services.Patient
{
    public interface IPatientManager
    {
        Task<PatientModel> AddNewPatientAsync(PatientModel patientModel);
        Task<PatientModel> UpdatePatientAsync(PatientModel patientModel);
        Task<PatientModel> GetPatientByIdAsync(Guid id);
        Task<PagedResult<PatientModel>> GetPagedPatientAsync(SieveModel sieveModel);
        Task<List<PatientModel>> GetAllPatients();
    }

    public class PatientManager: IPatientManager
    {
        private readonly ILogger _logger;
        private readonly IPatientRepository _patientRepository;
        private readonly ISieveProcessor _sieveProcessor;

        public PatientManager(ILogger<PatientManager> logger, IPatientRepository patientRepository, ISieveProcessor sieveProcessor) 
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _sieveProcessor = sieveProcessor;
        }


        public async Task<PatientModel> GetPatientByIdAsync(Guid id)
        {
            if (id == Guid.Empty) 
            {
                return null;
            }

            PatientModel patientModel = await _patientRepository.GetPatientByIdAsync(id);

            return patientModel;
        }

        public async Task<PatientModel> AddNewPatientAsync(PatientModel patientModel) 
        {
            var newPatient = await _patientRepository.AddPatientAsync(patientModel);

            if (newPatient != null)
            {
                if (_patientRepository.SaveChanges())
                    return newPatient;
                else
                    throw new Exception($"Failed to save patient to database, patient name: {patientModel.FirstName}");
            }
            else
                throw new Exception($"Failed to create patient, patient name: {patientModel.FirstName}");
        }

        public async Task<PatientModel> UpdatePatientAsync(PatientModel patientModel)
        {
            var updatePatient = await _patientRepository.UpdatePatientAsync(patientModel);

            if (_patientRepository.SaveChanges())
                return updatePatient;
            else
                throw new Exception($"Failed to save updated patient to database, patientId: {patientModel.Id}");

        }

        public async Task<PagedResult<PatientModel>> GetPagedPatientAsync(SieveModel sieveModel)
        {
            sieveModel.Page = (sieveModel.Page == null) ? 1 : sieveModel.Page;
            sieveModel.PageSize = (sieveModel.PageSize == null) ? 50 : sieveModel.PageSize;

            var queryablePatient = _patientRepository.GetQueryablePatient();
            var Pagedresult =  _sieveProcessor.GetPaged(queryablePatient, sieveModel);

            return Pagedresult;
        }

        public async Task<List<PatientModel>> GetAllPatients()
        {
            var queryablePatient = _patientRepository.GetQueryablePatient();

            return await queryablePatient.ToListAsync();
        }
    }
}
