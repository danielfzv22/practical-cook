using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<Step> Steps { get; set; } = [];

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];

        public ICollection<RecipeUtensil> RecipeUtensils { get; set; } = [];
    }
}