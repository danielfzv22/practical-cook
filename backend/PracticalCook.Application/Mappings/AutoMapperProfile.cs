using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Dtos.Step;
using PracticalCook.Application.Dtos.Utensil;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Recipe, GetRecipeInformationDto>();

            CreateMap<Recipe, GetRecipeDto>();
            CreateMap<AddRecipeDto, Recipe>();

            CreateMap<Ingredient, GetIngredientDto>();
            CreateMap<AddIngredientDto, Ingredient>();

            CreateMap<Utensil, GetUtensilDto>();
            CreateMap<AddUtensilDto, Utensil>();

            CreateMap<Step, GetStepDto>();
            CreateMap<AddStepDto, Step>();

            CreateMap<AddRecipeIngredientDto, RecipeIngredient>();
            CreateMap<RecipeIngredient, GetRecipeIngredientDto>();

            CreateMap<AddRecipeUtensilDto, RecipeUtensil>();
            CreateMap<RecipeUtensil, GetRecipeUtensilDto>();

            CreateMap<AddRecipeStepDto, Step>();
            CreateMap<Step, GetRecipeStepDto>();


        }
    }
}