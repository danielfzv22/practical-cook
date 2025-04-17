using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Utensil
{
    public class UpdateUtensilDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}