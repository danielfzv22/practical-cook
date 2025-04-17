using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class AddRecipeUtensilDto
    {
        public int RecipeId { get; set; }

        public int UtensilId { get; set; }
    }
}