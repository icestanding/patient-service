using System.Collections.Generic;
using CodingChallange.Shared.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodingChallange.Services.Patient.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/patients")]
    [ApiController]
    public class PatientController : BasePatientController
    {
        IPatientManager _patientManager;

        public PatientController(ILogger<BasePatientController> logger, IPatientManager patientManager) : base(logger)
        {
            _patientManager = patientManager;
        }


        [HttpGet]
        public ActionResult<IEnumerable<PatientViewModel>> Get()
        {
            return null;
        }




    }
}
