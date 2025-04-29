using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Services.RecipeService;

namespace PracticalCook.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger) : ControllerBase
    {
        private readonly IRecipeService recipeService = recipeService;

        private readonly ILogger<RecipeController> logger = logger;

        [HttpGet]
        public async Task<ActionResult<Response<GetRecipeDto>>> GetRecipes()
        {
            logger.LogInformation("Fetching all recipes");
            return Ok(await this.recipeService.GetRecipes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetRecipeInformationDto>>> GetRecipeById(int id)
        {
            logger.LogInformation("Fetching recipe with id: {id}", id);
            var result = await this.recipeService.GetRecipeById(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddRecipe([FromBody] AddRecipeDto newRecipe)
        {
            return Ok(await this.recipeService.AddRecipe(newRecipe));
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetRecipeDto>>> UpdateRecipe([FromBody] UpdateRecipeDto Recipe)
        {
            var result = await this.recipeService.UpdateRecipe(Recipe);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> RemoveRecipe(int id)
        {
            var result = await this.recipeService.DeleteRecipe(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("Ingredients")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddIngredientToRecipe(AddRecipeIngredientDto newRecipeIngredient)
        {
            return Ok(await this.recipeService.AddIngredientToRecipe(newRecipeIngredient));
        }

        [HttpPost("Utensils")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddUtensilToRecipe(AddRecipeUtensilDto newRecipeUtensil)
        {
            return Ok(await this.recipeService.AddUtensilToRecipe(newRecipeUtensil));
        }

        [HttpPost("Step")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddStepToRecipe(AddRecipeStepDto newRecipeStep)
        {
            return Ok(await this.recipeService.AddStepToRecipe(newRecipeStep));
        }
    }
}