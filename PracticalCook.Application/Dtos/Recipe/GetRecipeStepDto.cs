using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.Step;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class GetRecipeStepDto
    {
        public GetStepDto Step { get; set; } = new GetStepDto();
    }
}
