using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraticalCook.Domain.Entities
{
    public class Step
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        
        public string DefaultDescription { get; set; } = string.Empty;
    }
}