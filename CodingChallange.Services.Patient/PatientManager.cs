﻿using CodingChallange.Repositories.Patient;
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

namespace CodingChallange.Services.Patient
{
    public interface IPatientManager
    {
        Task<PatientModel> AddNewPatientAsync(PatientModel patientModel);
        Task<PatientModel> UpdatePatientAsync(PatientModel patientModel);
        Task<PatientModel> GetPatientByIdAsync(Guid id);
        Task<PagedResult<PatientModel>> GetPagedPatientAsync(SieveModel sieveModel);
    }

    public class PatientManager: IPatientManager
    {
        private readonly ILogger _logger;
        private readonly IPatientRepository _patientRepository;
        private readonly SieveProcessor _sieveProcessor;

        public PatientManager(ILogger<PatientManager> logger, IPatientRepository patientRepository, SieveProcessor sieveProcessor) 
        {
            _logger = logger;
            _patientRepository = patientRepository;
            _sieveProcessor = sieveProcessor;
        }


        public async Task<PatientModel> GetPatientByIdAsync(Guid id)
        {

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

            if (updatePatient != null)
            {
                if (_patientRepository.SaveChanges())
                    return updatePatient;
                else
                    throw new Exception($"Failed to save updated patient to database, patientId: {patientModel.Id}");
            }
            else
                throw new Exception($"Failed to update patient, patientId: {patientModel.Id}");
        }


        public async Task<PagedResult<PatientModel>> GetPagedPatientAsync(SieveModel sieveModel)
        {
            var queryablePatient = _patientRepository.GetQueryablePatient();
            var pagedPatients = _sieveProcessor.Apply(sieveModel, queryablePatient);

            var pageResult = new PagedResult<PatientModel>()
            {
                PageNumber = sieveModel.Page.Value,
                PageSize = sieveModel.PageSize.Value,
                TotalNumberOfPages = (int)Math.Ceiling((decimal)queryablePatient.Count() / sieveModel.PageSize.Value),
                TotalNumberOfRecords = queryablePatient.Count(),
                Results = await pagedPatients.ToListAsync()
            };

            return pageResult;
        }
    }
}
