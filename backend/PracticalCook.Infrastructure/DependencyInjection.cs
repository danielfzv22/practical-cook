using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticalCook.Application.Services.IngredientService;
using PracticalCook.Application.Services.RecipeService;
using PracticalCook.Application.Services.UserService;
using PracticalCook.Application.Services.UtensilService;
using PracticalCook.Infrastructure.DataAccess;
using PracticalCook.Infrastructure.Repositories;

namespace PracticalCook.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IUtensilRepository, UtensilRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
