using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class RecipeTag
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

        public RecipeTag(Tag tag, Recipe recipe)
        {
            Tag = tag ?? throw new ArgumentNullException(nameof(tag), "Tag cannot be null");
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
            TagId = tag.Id;
            RecipeId = recipe.Id;
        }

        private RecipeTag()
        {
            // Parameterless constructor for EF Core
        }
    }
}
