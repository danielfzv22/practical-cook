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
    }
}
