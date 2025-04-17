using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Step;
using PracticalCook.Application.Services.StepService;

namespace PracticalCook.Controllers
{
    [ApiController]
    [Route("Recipe/[controller]")]
    public class StepController(IStepService stepService, ILogger logger) : ControllerBase
    {
        private readonly IStepService _stepService = stepService;

        private readonly ILogger logger = logger;

        [HttpGet]
        public async Task<ActionResult<Response<GetStepDto>>> GetSteps()
        {
            return Ok(await _stepService.GetSteps());
        }
    }
}