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
using Newtonsoft.Json.Linq;

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
        public async Task<ActionResult> GetPatient([FromQuery]SieveModel sieveModel)
        {
            // return all patients
            if (sieveModel.PageSize == null && sieveModel.Page == null)
            {
                var allPatients = await _patientManager.GetAllPatients();
                var allPatientsViewModel = _mapper.Map<List<PatientViewModel>>(allPatients);
                
                return Ok(JObject.FromObject(new { patients = allPatientsViewModel }));
            }

            var pagedModel = await _patientManager.GetPagedPatientAsync(sieveModel);
            var reuslt = new PagedResult<PatientViewModel>()
            {
                PageNumber = pagedModel.PageNumber,
                PageSize = pagedModel.PageSize,
                TotalNumberOfPages = pagedModel.TotalNumberOfPages,
                TotalNumberOfRecords = pagedModel.TotalNumberOfRecords,
                Results = _mapper.Map<List<PatientViewModel>>(pagedModel.Results)
            };

            return Ok(reuslt);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PatientViewModel>> GetPatientById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var model = await _patientManager.GetPatientByIdAsync(id);
            var result = _mapper.Map<PatientViewModel>(model);

            return Ok(result);
        }

        // Add New Patient
        [HttpPost]
        public async Task<ActionResult<PatientViewModel>> Post([FromBody] PatientRequestViewModel patientRequestViewModel)
        {

            var model = _mapper.Map<PatientModel>(patientRequestViewModel);
            model =  await _patientManager.AddNewPatientAsync(model);
            var result = _mapper.Map<PatientViewModel>(model);
                
            return StatusCode(201, result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PatientViewModel>> Put(Guid id, [FromBody] PatientRequestViewModel patientRequestViewModel)
        {

            var model = _mapper.Map<PatientModel>(patientRequestViewModel);
            model.Id = id;
            model = await _patientManager.UpdatePatientAsync(model);
            var result = _mapper.Map<PatientViewModel>(model);

            return Ok(result);
        }

    }
}
