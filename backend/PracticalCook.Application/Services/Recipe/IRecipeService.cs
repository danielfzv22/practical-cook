using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.Recipe;

namespace PracticalCook.Application.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<Response<List<GetRecipeDto>>> GetRecipes();

        Task<Response<GetRecipeInformationDto>> GetRecipeById(int id);

        Task<Response<GetRecipeDto>> AddRecipe(AddRecipeDto newRecipe);

        Task<Response<List<GetRecipeDto>>> AddMultipleRecipes(List<AddRecipeDto> newRecipes);

        Task<Response<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe);

        Task<Response<GetRecipeDto>> DeleteRecipe(int id);

        Task<Response<GetRecipeDto>> AddIngredientToRecipe(AddRecipeIngredientDto recipeIngredient);

        Task<Response<GetRecipeDto>> AddUtensilToRecipe(AddRecipeUtensilDto recipeUtensil);

        Task<Response<GetRecipeDto>> AddStepToRecipe(AddRecipeStepDto recipeStep);
    }
}