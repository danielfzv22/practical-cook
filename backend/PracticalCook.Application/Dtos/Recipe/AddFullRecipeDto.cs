using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraticalCook.Domain.Entities;
using PraticalCook.Domain.Entities.Enumerations;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class AddFullRecipeDto
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Difficulty Difficulty { get; set; }

        public int Calories { get; set; }

        public int Servings { get; set; }

        public int Rating { get; set; }

        public int PreparationTime { get; set; }

        public List<AddRecipeIngredientDto> RecipeIngredients { get; set; } = [];

        public List<AddRecipeUtensilDto> RecipeUtensils { get; set; } = [];

        public List<AddRecipeStepDto> Steps { get; set; } = [];
    }
}
