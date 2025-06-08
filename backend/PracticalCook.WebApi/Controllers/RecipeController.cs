using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<Response<List<GetRecipeDto>>>> GetRecipes()
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
                return StatusCode(500, new Response<List<GetRecipeDto>>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> GetRecipeById(int id)
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
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [Authorize]
        [HttpGet("user-recipes")]
        public async Task<ActionResult<Response<GetRecipeDto>>> GetUserRecipes()
        {
            logger.LogInformation("GET /recipes - Fetching all recipes");
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdString, out Guid userId))
                {
                    return Unauthorized(new Response<GetRecipeDto> { Success = false, Message = "Invalid user token." });
                }

                var response = await recipeService.GetUserRecipes(userId);
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddRecipe([FromBody] AddRecipeDto newRecipe)
        {
            logger.LogInformation("POST /recipes - Adding new recipe: {Name}", newRecipe.Name);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized(new Response<GetRecipeDto> { Success = false, Message = "Invalid user token." });
            }

            try
            {
                var response = await recipeService.AddRecipe(newRecipe, userId);
                if (!response.Success)
                    logger.LogWarning("POST /recipes - Failed to add recipe: {Message}", response.Message);
                else
                    logger.LogInformation("POST /recipes - Recipe added with ID {Id}", response.Data?.Id);

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

        [HttpPost("{recipeId}/ingredients")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddIngredientToRecipe(int recipeId, [FromBody] AddRecipeIngredientDto dto)
        {
            logger.LogInformation("POST /recipes/Ingredients - Adding ingredient {IngredientId} to recipe {RecipeId}", dto.IngredientId, recipeId);
            try
            {
                var response = await recipeService.AddIngredientToRecipe(recipeId, dto);
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

        [HttpDelete("{recipeId}/ingredients/{ingredientId}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            logger.LogInformation("DELETE /recipes/{RecipeId}/ingredients/{IngredientId} - Removing Ingredient from recipe", recipeId, ingredientId);
            try
            {
                var response = await recipeService.RemoveIngredientFromRecipe(recipeId, ingredientId);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /recipes/{RecipeId}/ingredients/{IngredientId} - Recipe not found", recipeId, ingredientId);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /recipes/{RecipeId}/ingredients/{IngredientId} - Ingredient removed from Recipe", recipeId, ingredientId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /recipes/{RecipeId}/ingredients/{IngredientId} - Error removing ingredient from Recipe", recipeId, ingredientId);
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost("{recipeId}/utensils")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddUtensilToRecipe(int recipeId, [FromBody] AddRecipeUtensilDto dto)
        {
            logger.LogInformation("POST /recipes/Utensils - Adding utensil {UtensilId} to recipe {RecipeId}", dto.UtensilId, recipeId);
            try
            {
                var response = await recipeService.AddUtensilToRecipe(recipeId, dto);
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

        [HttpDelete("{recipeId}/utensils/{utensilId}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> RemoveUtensilFromRecipe(int recipeId, int utensilId)
        {
            logger.LogInformation("DELETE /recipes/{RecipeId}/utensils/{UtensilId} - Removing Utensil from recipe", recipeId, utensilId);
            try
            {
                var response = await recipeService.RemoveUtensilFromRecipe(recipeId, utensilId);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /recipes/{RecipeId}/utensils/{UtensilId} - Recipe not found", recipeId, utensilId);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /recipes/{RecipeId}/utensils/{UtensilId} - Utensil removed from Recipe", recipeId, utensilId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /recipes/{RecipeId}/utensils/{UtensilId} - Error removing utensil from Recipe", recipeId, utensilId);
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPost("{recipeId}/steps")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddStepToRecipe(int recipeId, [FromBody] AddRecipeStepDto dto)
        {
            logger.LogInformation("POST /recipes/Step - Adding step to recipe {RecipeId}", recipeId);
            try
            {
                var response = await recipeService.AddStepToRecipe(recipeId, dto);
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

        [HttpDelete("{recipeId}/steps/{stepId}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> RemoveStepFromRecipe(int recipeId, int stepId)
        {
            logger.LogInformation("DELETE /recipes/{RecipeId}/steps/{StepId} - Removing Step from recipe", recipeId, stepId);
            try
            {
                var response = await recipeService.RemoveStepFromRecipe(recipeId, stepId);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /recipes/{RecipeId}/steps/{StepId} - Recipe not found", recipeId, stepId);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /recipes/{RecipeId}/steps/{StepId} - Step removed from Recipe", recipeId, stepId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /recipes/{RecipeId}/steps/{StepId} - Error removing step from Recipe", recipeId, stepId);
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPut("{recipeId}/steps")]
        public async Task<ActionResult<Response<GetRecipeDto>>> UpdateStepInRecipe(int recipeId, [FromBody] UpdateRecipeStepDto dto)
        {
            logger.LogInformation("PUT /recipes/{RecipeId}/steps - Updating step in recipe", recipeId);
            try
            {
                var response = await recipeService.UpdateStepInRecipe(recipeId, dto);
                if (response.Data is null)
                {
                    logger.LogWarning("PUT /recipes/{RecipeId}/steps - Recipe not found", recipeId);
                    return NotFound(response);
                }
                logger.LogInformation("PUT /recipes/{RecipeId}/steps - Step updated in Recipe", recipeId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PUT /recipes/{RecipeId}/steps - Error updating step in Recipe", recipeId);
                return StatusCode(500, new Response<GetRecipeDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }

        }
    }
}