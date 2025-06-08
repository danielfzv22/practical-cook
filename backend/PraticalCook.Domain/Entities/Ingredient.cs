using PraticalCook.Domain.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public FoodType Type { get; set; }

        public bool IsGlobal { get; set; } = false;

        public Guid? CreatedByUserId { get; set; }

        public User? CreatedByUser { get; set; }
    }
}