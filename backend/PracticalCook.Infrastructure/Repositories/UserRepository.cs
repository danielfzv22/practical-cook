using Microsoft.EntityFrameworkCore;
using PracticalCook.Application.Services.Auth;
using PracticalCook.Application.Services.RecipeService;
using PracticalCook.Infrastructure.DataAccess;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Infrastructure.Repositories
{
    public class UserRepository(DataContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User?> GetByGuidAsync(Guid userId)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
