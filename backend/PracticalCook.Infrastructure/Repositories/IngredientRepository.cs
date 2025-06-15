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
    public class IngredientRepository(DataContext context) : GenericRepository<Ingredient>(context), IIngredientRepository
    {
        public async Task<List<Ingredient>> GetUserIngredientsAsync(Guid userId)
        {

            var ingredients = await _dbSet.Where(i => i.IsGlobal).ToListAsync();
            var user = await _context.Users
               .Include(u => u.UserIngredients)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                ingredients.AddRange(user.UserIngredients);
            }

            return ingredients;
        }
    }
}
