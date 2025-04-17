using PracticalCook.Application.Services.IngredientService;
using PracticalCook.Application.Services.RecipeService;
using PracticalCook.Infrastructure.DataAccess;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Infrastructure.Repositories
{
    public class RecipeRepository(DataContext context) : GenericRepository<Recipe>(context), IRecipeRepository
    {
        public Task<Recipe> AddIngredientToRecipe(RecipeIngredient recipeIngredient)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> AddStepToRecipe(RecipeStep recipeStep)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> AddUtensilToRecipe(RecipeUtensil recipeUtensil)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe> GetRecipeWithDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
