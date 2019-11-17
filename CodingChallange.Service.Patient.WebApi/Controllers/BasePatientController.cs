using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingChallange.Services.Patient.WebApi.Controllers
{
    public class BasePatientController: Controller
    {
        protected readonly ILogger<BasePatientController> _logger;
        public BasePatientController(ILogger<BasePatientController> logger) {
            _logger = logger;
        }
    }
}
