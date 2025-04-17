using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalCook.Application.Dtos.Ingredient;
using PraticalCook.Domain.Entities.Enumerations;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class GetRecipeIngredientDto
    {
        public GetIngredientDto Ingredient { get; set; } = new GetIngredientDto();

        public double Quantity { get; set; }

        public Measurement Measure { get; set; }
    }
}