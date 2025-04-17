using PracticalCook.Application.Dtos.Step;
using PracticalCook.Application.Dtos.Utensil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class GetRecipeUtensilDto
    {
        public GetUtensilDto Utensil { get; set; } = new GetUtensilDto();

    }
}
