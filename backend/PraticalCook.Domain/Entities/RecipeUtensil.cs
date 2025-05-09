namespace PraticalCook.Domain.Entities
{
    public class RecipeUtensil
    {
        public Recipe Recipe { get; set; }

        public Utensil Utensil { get; set; }

        public int RecipeId { get; set; }

        public int UtensilId { get; set; }
        public RecipeUtensil(Recipe recipe, Utensil utensil)
        {
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
            Utensil = utensil ?? throw new ArgumentNullException(nameof(utensil), "Utensil cannot be null");
            RecipeId = recipe.Id;
            UtensilId = utensil.Id;
        }

        private RecipeUtensil()
        {
            // Parameterless constructor for EF Core
        }

    }
}
