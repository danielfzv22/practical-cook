using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Services.IngredientService;

namespace PracticalCook.WebApi.Controllers
{
    [ApiController]
    [Route("ingredients")]
    public class IngredientController(IIngredientService ingredientService, ILogger<IngredientController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Response<List<GetIngredientDto>>>> GetIngredients()
        {
            logger.LogInformation("GET /ingredients - Fetching all ingredients");
            try
            {
                var response = await ingredientService.GetIngredients();
                var count = response.Data?.Count ?? 0;

                if (count == 0)
                    logger.LogWarning("GET /ingredients - No ingredients found");
                else
                    logger.LogInformation("GET /ingredients - Retrieved {Count} ingredients", count);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /ingredients - Error fetching ingredients");
                return StatusCode(500, new Response<List<GetIngredientDto>>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetIngredientDto>>> GetIngredientById(int id)
        {
            logger.LogInformation("GET /ingredients/{Id} - Fetching ingredient", id);
            try
            {
                var response = await ingredientService.GetIngredientById(id);
                if (response.Data is null)
                {
                    logger.LogWarning("GET /ingredients/{Id} - Ingredient not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("GET /ingredients/{Id} - Retrieved ingredient {Name}", id, response.Data.Name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /ingredients/{Id} - Error fetching ingredient", id);
                return StatusCode(500, new Response<GetIngredientDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Response<GetIngredientDto>>> AddIngredient([FromBody] AddIngredientDto newIngredient)
        {
            logger.LogInformation("POST /ingredients - Adding new ingredient: {Name}", newIngredient.Name);
            try
            {
                var response = await ingredientService.AddIngredient(newIngredient);
                if (!response.Success)
                    logger.LogWarning("POST /ingredients - Failed to add ingredient: {Message}", response.Message);
                else
                    logger.LogInformation("POST /ingredients - Ingredient added with ID {Id}", response.Data.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /ingredients - Error adding ingredient");
                return StatusCode(500, new Response<GetIngredientDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetIngredientDto>>> UpdateIngredient([FromBody] UpdateIngredientDto ingredientDto)
        {
            logger.LogInformation("PUT /ingredients/{Id} - Updating ingredient: {Name}", ingredientDto.Id, ingredientDto.Name);
            try
            {
                var response = await ingredientService.UpdateIngredient(ingredientDto);
                if (response.Data is null)
                {
                    logger.LogWarning("PUT /ingredients/{Id} - Ingredient not found", ingredientDto.Id);
                    return NotFound(response);
                }

                logger.LogInformation("PUT /ingredients/{Id} - Ingredient updated successfully", ingredientDto.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PUT /ingredients/{Id} - Error updating ingredient", ingredientDto.Id);
                return StatusCode(500, new Response<GetIngredientDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetIngredientDto>>> RemoveIngredient(int id)
        {
            logger.LogInformation("DELETE /ingredients/{Id} - Removing ingredient", id);
            try
            {
                var response = await ingredientService.DeleteIngredient(id);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /ingredients/{Id} - Ingredient not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /ingredients/{Id} - Ingredient removed", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /ingredients/{Id} - Error removing ingredient", id);
                return StatusCode(500, new Response<GetIngredientDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }
    }
}