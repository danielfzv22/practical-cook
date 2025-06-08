using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Recipe;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.RecipeService
{
    public interface IRecipeRepository : IGenericRepository<Recipe>
    {
        Task<Recipe> GetRecipeWithDetails(int id);

        Task<List<Recipe>> GetUserRecipesAsync(Guid userId);

        Task<List<Recipe>> AddMultipleRecipes(List<Recipe> recipes);

        Task<Recipe> AddIngredientToRecipe(RecipeIngredient recipeIngredient);

        Task<Recipe> RemoveIngredientFromRecipe(int recipeId, int ingredientId);

        Task<Recipe> AddUtensilToRecipe(RecipeUtensil recipeUtensil);

        Task<Recipe> RemoveUtensilFromRecipe(int recipeId, int utensilId);

        Task<Recipe> AddStepToRecipe(Step recipeStep);

        Task<Recipe> RemoveStepFromRecipe(int recipeId, int stepId);

        Task<Recipe> UpdateStepInRecipe(int recipeId, Step updatedStep);

    }
}
