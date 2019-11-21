using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Threading.Tasks;
using Sieve.Models;
using Newtonsoft.Json.Linq;
using CodingChallange.Shared.Models.Pagination;
using CodingChallange.Shared.ViewModels.Patient;
using CodingChallange.Shared.Models.Patient;
using CodingChallange.Shared.ViewModels;
using CodingChallange.Services.Patient;

namespace CodingChallange.Services.Patient.WebApi.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/patients")]
    [ApiController]
    public class PatientsController : BasePatientsController
    {
        IPatientManager _patientManager;
        IMapper _mapper;

        public PatientsController(ILogger<PatientsController> logger, IPatientManager patientManager, IMapper mapper) : base(logger)
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
                try
                {
                    _logger.LogInformation($"Get all Patients info from db");
                    var allPatients = await _patientManager.GetAllPatients();
                    var allPatientsViewModel = _mapper.Map<List<PatientViewModel>>(allPatients);
                    return Ok(JObject.FromObject(new { patients = allPatientsViewModel }));
                }
                catch (Exception e){
                    throw new HttpStatusCodeException(500,
                    "Error happend when process get all patients request." + e.Message);
                }
            }

            try
            {
                var pagedModel = await _patientManager.GetPagedPatientAsync(sieveModel);
                _logger.LogInformation($"Get paged patient result from DB PageSize {sieveModel.PageSize}, PageNo: {sieveModel.Page}");

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
            catch (Exception e)
            {
                throw new HttpStatusCodeException(500,
                "Error happend when get paged result request." + e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PatientViewModel>> GetPatientById(Guid id)
        {
            _logger.LogInformation($"Get patient by id {id.ToString()}");
            if (id == Guid.Empty)
            {
                return NotFound("No such patient exists");
            }
            try
            {
                var model = await _patientManager.GetPatientByIdAsync(id);

                if (model == null)
                {
                    return NotFound("No such patient exists");
                }
                var result = _mapper.Map<PatientViewModel>(model);

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(500,
                "Error happend when get patient by Id." + e.Message);
            }
        }

        // Add New Patient
        [HttpPost]
        public async Task<ActionResult<PatientViewModel>> Post([FromBody] PatientRequestViewModel patientRequestViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid JSON");

            try
            {
                _logger.LogInformation($"Add new patient FirstName :{patientRequestViewModel.FirstName} ");
                var model = _mapper.Map<PatientModel>(patientRequestViewModel);
                model = await _patientManager.AddNewPatientAsync(model);
                var result = _mapper.Map<PatientViewModel>(model);
                return StatusCode(201, result);
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(500,
                "Error happend when create new patient." + e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PatientViewModel>> Put(Guid id, [FromBody] PatientRequestViewModel patientRequestViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid JSON");

            try
            {
                _logger.LogInformation($"Update patient FirstName :{patientRequestViewModel.FirstName} ");
                var model = _mapper.Map<PatientModel>(patientRequestViewModel);
                model.Id = id;
                model = await _patientManager.UpdatePatientAsync(model);

                if (model == null)
                {
                    return NotFound("No such patient exists");
                }

                var result = _mapper.Map<PatientViewModel>(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(500,
                "Error happend when update a patient." + e.Message);
            }
        }

    }
}
