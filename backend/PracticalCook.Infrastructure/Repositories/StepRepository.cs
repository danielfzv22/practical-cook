using Microsoft.EntityFrameworkCore;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Services.StepService;
using PracticalCook.Infrastructure.DataAccess;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Infrastructure.Repositories
{
    public class StepRepository(DataContext context) : GenericRepository<Step>(context), IStepRepository
    {
    }
}
