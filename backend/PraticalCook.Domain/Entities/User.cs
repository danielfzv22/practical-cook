using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ICollection<UserRecipe> UserRecipes { get; set; } = [];

        public ICollection<Recipe> CreatedRecipes { get; set; } = [];

        public ICollection<Ingredient> UserIngredients { get; set; } = [];

        public ICollection<Utensil> UserUtensils { get; set; } = [];
    }
}
