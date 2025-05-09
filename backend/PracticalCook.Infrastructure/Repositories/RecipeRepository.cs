using Microsoft.EntityFrameworkCore;
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
        public async Task<Recipe> AddIngredientToRecipe(RecipeIngredient recipeIngredient)
        {
            var recipe = await _dbSet
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipeIngredient.RecipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            var exists = recipe.RecipeIngredients.Any(ri =>
                ri.IngredientId == recipeIngredient.IngredientId);

            if (exists)
                throw new Exception("Ingredient already added to recipe");

            recipe.RecipeIngredients.Add(recipeIngredient);

            await context.SaveChangesAsync();

            return recipe;
        }

        public async Task<List<Recipe>> AddMultipleRecipes(List<Recipe> recipes)
        {
            _dbSet.AddRange(recipes);
            await context.SaveChangesAsync();
            return recipes;
        }

        public async Task<Recipe> AddStepToRecipe(Step recipeStep)
        {
            var recipe = await _dbSet
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.Id == recipeStep.RecipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            recipe.Steps.Add(recipeStep);
            await context.SaveChangesAsync();
            return recipeStep.Recipe;
        }

        public async Task<Recipe> AddUtensilToRecipe(RecipeUtensil recipeUtensil)
        {
            var recipe = await _dbSet
                .Include(r => r.RecipeUtensils)
                .FirstOrDefaultAsync(r => r.Id == recipeUtensil.RecipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            var exists = recipe.RecipeUtensils.Any(ru =>
                ru.UtensilId == recipeUtensil.UtensilId);

            if (exists)
                throw new Exception("Utensil already added to recipe");

            recipe.RecipeUtensils.Add(recipeUtensil);
            await context.SaveChangesAsync();

            return recipeUtensil.Recipe;
        }

        public async Task<Recipe> GetRecipeWithDetails(int id)
        {
            var recipe = await _dbSet
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeUtensils)
                    .ThenInclude(ru => ru.Utensil)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
                throw new Exception("Recipe not found");

            return recipe;
        }
    }
}
