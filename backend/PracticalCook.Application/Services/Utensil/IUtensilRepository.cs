using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.UtensilService
{
    public interface IUtensilRepository : IGenericRepository<Utensil>
    {
        Task<List<Utensil>> GetUserUtensilsAsync(Guid userId);
    }
}
