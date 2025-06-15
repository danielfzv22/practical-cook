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

        Task<Response<GetRecipeDto>> GetRecipeById(int id);

        Task<Response<List<GetRecipeDto>>> GetUserRecipes(Guid userId);

        Task<Response<GetRecipeDto>> AddRecipe(AddRecipeDto newRecipe, Guid userId);

        Task<Response<GetRecipeDto>> AddFullRecipe(AddFullRecipeDto newRecipe, Guid userId);

        Task<Response<List<GetRecipeDto>>> AddMultipleRecipes(List<AddRecipeDto> newRecipes);

        Task<Response<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe);

        Task<Response<GetRecipeDto>> DeleteRecipe(int id);

        Task<Response<GetRecipeDto>> AddIngredientToRecipe(int recipeId, AddRecipeIngredientDto recipeIngredient);

        Task<Response<GetRecipeDto>> RemoveIngredientFromRecipe(int recipeId, int ingredientId);

        Task<Response<GetRecipeDto>> AddUtensilToRecipe(int recipeId, AddRecipeUtensilDto recipeUtensil);

        Task<Response<GetRecipeDto>> RemoveUtensilFromRecipe(int recipeId, int utensilId);

        Task<Response<GetRecipeDto>> AddStepToRecipe(int recipeId, AddRecipeStepDto recipeStep);

        Task<Response<GetRecipeDto>> RemoveStepFromRecipe(int recipeId, int stepId);

        Task<Response<GetRecipeDto>> UpdateStepInRecipe(int recipeId, UpdateRecipeStepDto updatedStep);
    }
}