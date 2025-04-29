namespace PraticalCook.Domain.Entities
{
    public class RecipeStep
    {
        public int Id { get; set; }

        public Recipe Recipe { get; set; }

        public Step Step { get; set; }

        public int Order { get; set; }

        public string CustomDescription { get; set; } = string.Empty;

        public int RecipeId { get; set; }

        public int StepId { get; set; }

        public RecipeStep(Recipe recipe, Step step, int order)
        {
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
            Step = step ?? throw new ArgumentNullException(nameof(step), "Step cannot be null");
            Order = order;
            RecipeId = recipe.Id;
            StepId = step.Id;
        }

        private RecipeStep()
        {
            // Parameterless constructor for EF Core
        }

        public ICollection<RecipeStepUtensil> StepUtensils { get; set; } = [];
    }
}
