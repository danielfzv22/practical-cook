namespace PracticalCook.Application.Dtos.Recipe
{
    public class AddRecipeStepDto
    {
        public string Title { get; set; } = string.Empty;

        public int StepOrder { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
