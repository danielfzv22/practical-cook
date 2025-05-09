using PraticalCook.Domain.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class GetRecipeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Difficulty Difficulty { get; set; }

        public int Calories { get; set; }

        public int Servings { get; set; }

        public int Rating { get; set; }

        public int PreparationTime { get; set; }

        public List<GetRecipeIngredientDto> Ingredients { get; set; } = new List<GetRecipeIngredientDto>();

        public List<GetRecipeUtensilDto> Utensils { get; set; } = new List<GetRecipeUtensilDto>();

        public List<GetRecipeStepDto> Steps { get; set; } = new List<GetRecipeStepDto>();

    }
}