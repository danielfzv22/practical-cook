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
        public async Task<Recipe> GetRecipeWithDetails(int id)
        {
            var recipe = await _dbSet
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.RecipeUtensils)
                    .ThenInclude(ru => ru.Utensil)
                .Include(r => r.Steps)
                .Include(r => r.CreatedByUser)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
                throw new Exception("Recipe not found");

            return recipe;
        }

        public async Task<List<Recipe>> AddMultipleRecipes(List<Recipe> recipes)
        {
            _dbSet.AddRange(recipes);
            await context.SaveChangesAsync();
            return recipes;
        }

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

        public async Task<Recipe> RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            var recipe = await _dbSet
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            var recipeIngredient = recipe.RecipeIngredients
                .FirstOrDefault(ri => ri.IngredientId == ingredientId);

            if (recipeIngredient == null)
                throw new Exception("Ingredient not found in recipe");

            recipe.RecipeIngredients.Remove(recipeIngredient);

            await context.SaveChangesAsync();
            return recipe;
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

        public async Task<Recipe> RemoveUtensilFromRecipe(int recipeId, int utensilId)
        {
            var recipe = await _dbSet
                .Include(r => r.RecipeUtensils)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            var recipeUtensil = recipe.RecipeUtensils
                .FirstOrDefault(ru => ru.UtensilId == utensilId);

            if (recipeUtensil == null)
                throw new Exception("Utensil not found in recipe");

            recipe.RecipeUtensils.Remove(recipeUtensil);
            await context.SaveChangesAsync();

            return recipe;
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

        public async Task<Recipe> RemoveStepFromRecipe(int recipeId, int stepId)
        {
            var recipe = await _dbSet
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            var recipeStep = recipe.Steps
                .FirstOrDefault(rs => rs.Id == stepId);

            if (recipeStep == null)
                throw new Exception("Step not found in recipe");

            recipe.Steps.Remove(recipeStep);
            await context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> UpdateStepInRecipe(int recipeId, Step updatedStep)
        {
            var recipe = await _dbSet
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
                throw new Exception("Recipe not found");

            var recipeStep = recipe.Steps
                .FirstOrDefault(rs => rs.Id == updatedStep.Id);
            if (recipeStep == null)
                throw new Exception("Step not found in recipe");

            recipeStep.Title = updatedStep.Title;
            recipeStep.Description = updatedStep.Description;
            recipeStep.StepOrder = updatedStep.StepOrder;

            await context.SaveChangesAsync();
            return recipe;
        }

        public async Task<List<Recipe>> GetUserRecipesAsync(Guid userId)
        {
            var user = await _context.Users
               .Include(u => u.UserRecipes)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return new List<Recipe>();

            var recipeIds = user.UserRecipes.Select(ur => ur.RecipeId);

            var recipes = await _context.Recipes
                .Where(r => recipeIds.Contains(r.Id))
                .ToListAsync();

            return recipes;
        }
    }
}
