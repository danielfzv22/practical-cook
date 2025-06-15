using PraticalCook.Domain.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Ingredient
{
    public class GetIngredientDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public FoodType Type { get; set; }

        public bool IsGlobal { get; set; }
    }
}