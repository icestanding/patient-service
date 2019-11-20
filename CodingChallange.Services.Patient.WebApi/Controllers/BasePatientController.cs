using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodingChallange.Services.Patient.WebApi.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class BasePatientsController: Controller
    {
        protected readonly ILogger<BasePatientsController> _logger;
        public BasePatientsController(ILogger<BasePatientsController> logger) {
            _logger = logger;
        }
    }
}
