using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class UserRecipe
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public string Notes { get; set; } = string.Empty; // Default to empty string
        public bool IsFavorite { get; set; } = false;

        public bool IsCustom { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
