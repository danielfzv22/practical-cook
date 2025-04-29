using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.IngredientService
{
    public interface IIngredientRepository : IGenericRepository<Ingredient>
    {
    }
}
