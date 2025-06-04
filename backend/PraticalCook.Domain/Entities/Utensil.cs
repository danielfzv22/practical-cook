using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class Utensil
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsGlobal { get; set; } = false;

        public Guid? CreatedByUserId { get; set; }

        public User? CreatedByUser { get; set; }
    }
}