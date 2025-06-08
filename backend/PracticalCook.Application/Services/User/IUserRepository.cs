using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.UserService
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string username);

        Task<User?> GetByGuidAsync(Guid userId);
    }
}
