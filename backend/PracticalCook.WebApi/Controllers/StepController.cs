using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Step;
using PracticalCook.Application.Services.StepService;

namespace PracticalCook.WebApi.Controllers
{
    [ApiController]
    [Route("Recipe/[controller]")]
    public class StepController(IStepService stepService, ILogger<StepController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Response<List<GetStepDto>>>> GetSteps()
        {
            logger.LogInformation("GET /steps - Fetching all steps");
            try
            {
                var response = await stepService.GetSteps();
                var count = response.Data?.Count ?? 0;

                if (count == 0)
                    logger.LogWarning("GET /steps - No steps found");
                else
                    logger.LogInformation("GET /steps - Retrieved {Count} steps", count);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /steps - Error fetching steps");
                return StatusCode(500, new Response<List<GetStepDto>>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetStepDto>>> GetStepById(int id)
        {
            logger.LogInformation("GET /steps/{Id} - Fetching step", id);
            try
            {
                var response = await stepService.GetStepById(id);
                if (response.Data is null)
                {
                    logger.LogWarning("GET /steps/{Id} - Step not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("GET /steps/{Id} - Retrieved step {Name}", id, response.Data.Title);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /steps/{Id} - Error fetching step", id);
                return StatusCode(500, new Response<GetStepDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Response<GetStepDto>>> AddStep([FromBody] AddStepDto newStep)
        {
            logger.LogInformation("POST /steps - Adding new step: {Name}", newStep.Title);
            try
            {
                var response = await stepService.AddStep(newStep);
                if (!response.Success)
                    logger.LogWarning("POST /steps - Failed to add step: {Message}", response.Message);
                else
                    logger.LogInformation("POST /steps - Step added with ID {Id}", response.Data.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /steps - Error adding step");
                return StatusCode(500, new Response<GetStepDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetStepDto>>> UpdateStep([FromBody] UpdateStepDto stepDto)
        {
            logger.LogInformation("PUT /steps/{Id} - Updating step: {Name}", stepDto.Id, stepDto.Title);
            try
            {
                var response = await stepService.UpdateStep(stepDto);
                if (response.Data is null)
                {
                    logger.LogWarning("PUT /steps/{Id} - Step not found", stepDto.Id);
                    return NotFound(response);
                }

                logger.LogInformation("PUT /steps/{Id} - Step updated successfully", stepDto.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PUT /steps/{Id} - Error updating step", stepDto.Id);
                return StatusCode(500, new Response<GetStepDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetStepDto>>> RemoveStep(int id)
        {
            logger.LogInformation("DELETE /steps/{Id} - Removing step", id);
            try
            {
                var response = await stepService.DeleteStep(id);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /steps/{Id} - Step not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /steps/{Id} - Step removed", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /steps/{Id} - Error removing step", id);
                return StatusCode(500, new Response<GetStepDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }
    }
}