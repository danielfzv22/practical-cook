using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Utensil
{
    public class GetUtensilDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsGlobal { get; set; }
    }
}