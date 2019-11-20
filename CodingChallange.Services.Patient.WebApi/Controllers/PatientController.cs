using System;
using System.Collections.Generic;
using CodingChallange.Shared.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CodingChallange.Shared.Models.Patient;
using System.Threading.Tasks;
using Sieve.Models;
using CodingChallange.Shared.Models.Pagination;

namespace CodingChallange.Services.Patient.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/patients")]
    [ApiController]
    public class PatientsController : BasePatientsController
    {
        IPatientManager _patientManager;
        IMapper _mapper;

        public PatientsController(ILogger<BasePatientsController> logger, IPatientManager patientManager, IMapper mapper) : base(logger)
        {
            _patientManager = patientManager;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<PagedResult<PatientViewModel>> GetPatient(SieveModel sieveModel)
        {
            
            var PatientViewModel = new PatientViewModel();
            var result = new List<PatientViewModel>();
            result.Add(PatientViewModel);


            return Ok(PatientViewModel);
        }



        [HttpGet]
        [Route("{id}")]
        public ActionResult<PatientViewModel> GetPatientById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var PatientViewModel = new PatientViewModel();
            var result = new List<PatientViewModel>();
            result.Add(PatientViewModel);


            return Ok(PatientViewModel);
        }


        // Add New Patient
        [HttpPost]
        public async Task<ActionResult<PatientViewModel>> Post([FromBody] PatientRequestViewModel patientRequestViewModel)
        {

                var model = _mapper.Map<PatientModel>(patientRequestViewModel);
                model =  await _patientManager.AddNewPatientAsync(model);
                var result = _mapper.Map<PatientViewModel>(model);
                
                return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<PatientViewModel>> Put([FromBody] PatientRequestViewModel patientRequestViewModel)
        {

            var model = _mapper.Map<PatientModel>(patientRequestViewModel);
            model = await _patientManager.UpdatePatientAsync(model);
            var result = _mapper.Map<PatientViewModel>(model);

            return Ok(result);
        }




    }
}
