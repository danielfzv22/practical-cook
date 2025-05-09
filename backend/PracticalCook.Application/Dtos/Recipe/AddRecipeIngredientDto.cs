using PraticalCook.Domain.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Recipe
{
    public class AddRecipeIngredientDto
    {
        public int IngredientId { get; set; }

        public double Quantity { get; set; }

        public Measurement Measure { get; set; }
    }
}