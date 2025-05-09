namespace PraticalCook.Domain.Entities
{
    public class Step
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int StepOrder { get; set; }

        public string Description { get; set; } = string.Empty;

        public int StepId { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public Step(Recipe recipe)
        {
            Recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null");
            RecipeId = recipe.Id;
        }
        private Step()
        {
            // Parameterless constructor for EF Core
        }
    }
}
