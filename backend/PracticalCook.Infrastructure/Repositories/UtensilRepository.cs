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
    }
}
