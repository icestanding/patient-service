using CodingChallange.Repositories.Patient;
using Microsoft.Extensions.Logging;
using System;

namespace CodingChallange.Services.Patient
{
   
    public class PatientManager: IPatientManager
    {
        private readonly ILogger _logger;
        private readonly IPatientRepository _patientRepository;

        public PatientManager(ILogger logger, IPatientRepository patientRepository) 
        {
            _logger = logger;
            _patientRepository = patientRepository;
        }


    }
}
