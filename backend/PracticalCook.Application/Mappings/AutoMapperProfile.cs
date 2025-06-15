using AutoMapper;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Dtos.User;
using PracticalCook.Application.Dtos.Utensil;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Recipe, GetRecipeDto>();
            CreateMap<AddRecipeDto, Recipe>();
            CreateMap<AddFullRecipeDto, Recipe>();

            CreateMap<Ingredient, GetIngredientDto>();
            CreateMap<AddIngredientDto, Ingredient>();

            CreateMap<Utensil, GetUtensilDto>();
            CreateMap<AddUtensilDto, Utensil>();

            CreateMap<Step, GetRecipeStepDto>();
            CreateMap<AddRecipeStepDto, Step>();

            CreateMap<AddRecipeIngredientDto, RecipeIngredient>();
            CreateMap<RecipeIngredient, GetRecipeIngredientDto>();

            CreateMap<AddRecipeUtensilDto, RecipeUtensil>();
            CreateMap<RecipeUtensil, GetRecipeUtensilDto>();

            CreateMap<AddRecipeStepDto, Step>();
            CreateMap<Step, GetRecipeStepDto>();

            CreateMap<User, GetUserDto>();
        }
    }
}