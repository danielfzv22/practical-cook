using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Services.RecipeService;

namespace PracticalCook.WebApi.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Response<GetRecipeDto>>> GetRecipes()
        {
            logger.LogInformation("GET /recipes - Fetching all recipes");
            try
            {
                var response = await recipeService.GetRecipes();
                var count = response.Data?.Count ?? 0;

                if (count == 0)
                    logger.LogWarning("GET /recipes - No recipes found");
                else
                    logger.LogInformation("GET /recipes - Retrieved {Count} recipes", count);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /recipes - Error fetching recipes");
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetRecipeInformationDto>>> GetRecipeById(int id)
        {
            logger.LogInformation("GET /recipes/{Id} - Fetching recipe", id);
            try
            {
                var response = await recipeService.GetRecipeById(id);
                if (response.Data is null)
                {
                    logger.LogWarning("GET /recipes/{Id} - Recipe not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("GET /recipes/{Id} - Found recipe: {Name}", id, response.Data.Name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /recipes/{Id} - Error fetching recipe", id);
                return StatusCode(500, new Response<GetRecipeInformationDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddRecipe([FromBody] AddRecipeDto newRecipe)
        {
            logger.LogInformation("POST /recipes - Adding new recipe: {Name}", newRecipe.Name);
            try
            {
                var response = await recipeService.AddRecipe(newRecipe);
                if (!response.Success)
                    logger.LogWarning("POST /recipes - Failed to add recipe: {Message}", response.Message);
                else
                    logger.LogInformation("POST /recipes - Recipe added with ID {Id}", response.Data.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /recipes - Error adding recipe");
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost("add-multiple")]
        public async Task<ActionResult<Response<List<GetRecipeDto>>>> AddMultipleRecipes([FromBody] List<AddRecipeDto> recipes)
        {
            logger.LogInformation("POST /recipes/add-multiple - Adding {Count} recipes", recipes.Count);
            try
            {
                var response = await recipeService.AddMultipleRecipes(recipes);
                logger.LogInformation("POST /recipes/add-multiple - Added {Count} recipes", response.Data?.Count ?? 0);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /recipes/add-multiple - Error adding multiple recipes");
                return StatusCode(500, new Response<List<GetRecipeDto>>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetRecipeDto>>> UpdateRecipe([FromBody] UpdateRecipeDto recipeDto)
        {
            logger.LogInformation("PUT /recipes/{Id} - Updating recipe: {Name}", recipeDto.Id, recipeDto.Name);
            try
            {
                var response = await recipeService.UpdateRecipe(recipeDto);
                if (response.Data is null)
                {
                    logger.LogWarning("PUT /recipes/{Id} - Recipe not found", recipeDto.Id);
                    return NotFound(response);
                }

                logger.LogInformation("PUT /recipes/{Id} - Recipe updated successfully", recipeDto.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PUT /recipes/{Id} - Error updating recipe", recipeDto.Id);
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> RemoveRecipe(int id)
        {
            logger.LogInformation("DELETE /recipes/{Id} - Removing recipe", id);
            try
            {
                var response = await recipeService.DeleteRecipe(id);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /recipes/{Id} - Recipe not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /recipes/{Id} - Recipe removed", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /recipes/{Id} - Error removing recipe", id);
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost("ingredients")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddIngredientToRecipe(AddRecipeIngredientDto dto)
        {
            logger.LogInformation("POST /recipes/Ingredients - Adding ingredient {IngredientId} to recipe {RecipeId}", dto.IngredientId, dto.RecipeId);
            try
            {
                var response = await recipeService.AddIngredientToRecipe(dto);
                logger.LogInformation("POST /recipes/Ingredients - Ingredient added to recipe");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /recipes/Ingredients - Error adding ingredient to recipe");
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost("utensils")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddUtensilToRecipe(AddRecipeUtensilDto dto)
        {
            logger.LogInformation("POST /recipes/Utensils - Adding utensil {UtensilId} to recipe {RecipeId}", dto.UtensilId, dto.RecipeId);
            try
            {
                var response = await recipeService.AddUtensilToRecipe(dto);
                logger.LogInformation("POST /recipes/Utensils - Utensil added to recipe");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /recipes/Utensils - Error adding utensil to recipe");
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost("step")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddStepToRecipe(AddRecipeStepDto dto)
        {
            logger.LogInformation("POST /recipes/Step - Adding step to recipe {RecipeId}", dto.RecipeId);
            try
            {
                var response = await recipeService.AddStepToRecipe(dto);
                logger.LogInformation("POST /recipes/Step - Step added to recipe");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /recipes/Step - Error adding step to recipe");
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }
    }
}