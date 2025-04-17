using Microsoft.Extensions.DependencyInjection;
using PracticalCook.Application.Mappings;
using PracticalCook.Application.Services.IngredientService;
using PracticalCook.Application.Services.RecipeService;
using PracticalCook.Application.Services.StepService;
using PracticalCook.Application.Services.UtensilService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUtensilService, UtensilService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IStepService, StepService>();
            return services;
        }
    }
}
