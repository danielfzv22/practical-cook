using PraticalCook.Domain.Entities.Enumerations;
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

        public Difficulty Difficulty { get; set; }

        public int Calories { get; set; }

        public int Servings { get; set; }

        public int Rating { get; set; }

        public int PreparationTime { get; set; }

        public bool IsPublic { get; set; } = false;

        public bool HasBeenPublished { get; set; } = false;

        public bool IsAnonymous { get; set; } = false;

        public string SpecialNotes { get; set; } = string.Empty;

        public Guid? CreatedByUserId { get; set; }

        public User? CreatedByUser { get; set; }

        public ICollection<Step> Steps { get; set; } = [];

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];

        public ICollection<RecipeUtensil> RecipeUtensils { get; set; } = [];

        public ICollection<RecipeTag> RecipeTags { get; set; } = [];
    }
}