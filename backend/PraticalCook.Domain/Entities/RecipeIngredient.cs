using System;
using System.Collections.Generic;
using System.Linq;
using PraticalCook.Domain.Entities.Enumerations;

namespace PraticalCook.Domain.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        public double Quantity { get; set; }

        public Measurement Measure { get; set; }

        public Ingredient Ingredient { get; set; }

        public Recipe Recipe { get; set; }

        public int IngredientId { get; set; }

        public int RecipeId { get; set; }

        public RecipeIngredient(Ingredient ingredient, Recipe recipe)
        {
            Ingredient = ingredient ?? throw new ArgumentNullException(nameof(ingredient), "Ingredient cannot be null");
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
            IngredientId = ingredient.Id;
            RecipeId = recipe.Id;
        }

        private RecipeIngredient()
        {
            // Parameterless constructor for EF Core
        }
    }
}