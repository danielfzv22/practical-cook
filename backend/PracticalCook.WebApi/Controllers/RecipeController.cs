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
            logger.LogInformation("Fetching all recipes");
            return Ok(await recipeService.GetRecipes());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetRecipeInformationDto>>> GetRecipeById(int id)
        {
            logger.LogInformation("Fetching recipe with id: {id}", id);
            var result = await recipeService.GetRecipeById(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddRecipe([FromBody] AddRecipeDto newRecipe)
        {
            logger.LogInformation("Adding new recipe: {RecipeName}", newRecipe.Name);
            return Ok(await recipeService.AddRecipe(newRecipe));
        }

        [HttpPost("add-multiple")]
        public async Task<ActionResult<Response<List<GetRecipeDto>>>> AddMultipleRecipes([FromBody] List<AddRecipeDto> recipes)
        {
            logger.LogInformation("Adding new recipes bulk: {count}", recipes.Count);
            return Ok(await recipeService.AddMultipleRecipes(recipes));
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetRecipeDto>>> UpdateRecipe([FromBody] UpdateRecipeDto Recipe)
        {
            var result = await recipeService.UpdateRecipe(Recipe);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetRecipeDto>>> RemoveRecipe(int id)
        {
            var result = await recipeService.DeleteRecipe(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("Ingredients")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddIngredientToRecipe(AddRecipeIngredientDto newRecipeIngredient)
        {
            return Ok(await recipeService.AddIngredientToRecipe(newRecipeIngredient));
        }

        [HttpPost("Utensils")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddUtensilToRecipe(AddRecipeUtensilDto newRecipeUtensil)
        {
            return Ok(await recipeService.AddUtensilToRecipe(newRecipeUtensil));
        }

        [HttpPost("Step")]
        public async Task<ActionResult<Response<GetRecipeDto>>> AddStepToRecipe(AddRecipeStepDto newRecipeStep)
        {
            return Ok(await recipeService.AddStepToRecipe(newRecipeStep));
        }
    }
}