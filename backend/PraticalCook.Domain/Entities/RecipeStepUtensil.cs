namespace PraticalCook.Domain.Entities
{
    public class RecipeStepUtensil
    {
        public RecipeStep RecipeStep { get; set; }

        public Utensil Utensil { get; set; }

        public int RecipeStepId { get; set; }

        public int UtensilId { get; set; }

        public RecipeStepUtensil(RecipeStep recipeStep, Utensil utensil)
        {
            RecipeStep = recipeStep ?? throw new ArgumentNullException(nameof(recipeStep), "Recipe step cannot be null");
            Utensil = utensil ?? throw new ArgumentNullException(nameof(utensil), "Utensil cannot be null");
            RecipeStepId = recipeStep.Id;
            UtensilId = utensil.Id;
        }

        private RecipeStepUtensil()
        {
            // Parameterless constructor for EF Core
        }
    }
}
