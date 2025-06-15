using Microsoft.EntityFrameworkCore;
using PracticalCook.Application.Services.UtensilService;
using PracticalCook.Infrastructure.DataAccess;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Infrastructure.Repositories
{
    public class UtensilRepository(DataContext context) : GenericRepository<Utensil>(context), IUtensilRepository
    {
        public async Task<List<Utensil>> GetUserUtensilsAsync(Guid userId)
        {
            var utensils = await _dbSet.Where(i => i.IsGlobal).ToListAsync();
            var user = await _context.Users
               .Include(u => u.UserUtensils)
               .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                utensils.AddRange(user.UserUtensils);
            }

            return utensils;
        }
    }
}
