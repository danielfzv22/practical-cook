using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class UpdateRecipeStepDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int StepOrder { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
