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

        Task<List<Recipe>> AddMultipleRecipes(List<Recipe> recipes);

        Task<Recipe> AddIngredientToRecipe(RecipeIngredient recipeIngredient);

        Task<Recipe> AddUtensilToRecipe(RecipeUtensil recipeUtensil);
        
        Task<Recipe> AddStepToRecipe(RecipeStep recipeStep);

    }
}
