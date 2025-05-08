using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.Recipe;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.RecipeService
{
    public class RecipeService(IMapper mapper, IRecipeRepository recipeRepository) : IRecipeService
    {
        private readonly IMapper _mapper = mapper;

        private readonly IRecipeRepository _recipeRepository = recipeRepository;

        public async Task<Response<GetRecipeDto>> AddRecipe(AddRecipeDto newRecipe)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipe = _mapper.Map<Recipe>(newRecipe);
                await _recipeRepository.AddAsync(recipe);

                response.Data = _mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetRecipeDto>>> AddMultipleRecipes(List<AddRecipeDto> newRecipes)
        {
            var response = new Response<List<GetRecipeDto>>();

            try
            {
                List<Recipe> recipesToAdd = [];
                foreach (var recipe in newRecipes)
                {
                    recipesToAdd.Add(_mapper.Map<Recipe>(recipe));
                }

                await _recipeRepository.AddMultipleRecipes(recipesToAdd);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<GetRecipeDto>> DeleteRecipe(int id)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeToRemove = await _recipeRepository.RemoveAsync(id) ?? throw new Exception($"Recipe with id {id} not found!");

                response.Data = _mapper.Map<GetRecipeDto>(recipeToRemove);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeInformationDto>> GetRecipeById(int id)
        {
            var response = new Response<GetRecipeInformationDto>();
            try
            {
                var recipe = await _recipeRepository.GetRecipeWithDetails(id) ?? throw new Exception($"Recipe with id {id} not found!");

                response.Data = _mapper.Map<GetRecipeInformationDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetRecipeDto>>> GetRecipes()
        {
            var response = new Response<List<GetRecipeDto>>();
            try
            {
                var recipes = await _recipeRepository.GetAllAsync();
                response.Data = recipes.Select(i => _mapper.Map<GetRecipeDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> UpdateRecipe(UpdateRecipeDto updatedRecipe)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeToUpdate = await _recipeRepository.GetByIdAsync(updatedRecipe.Id) ?? throw new Exception($"Recipe with id {updatedRecipe.Id} not found!");

                recipeToUpdate.Name = updatedRecipe.Name;
                recipeToUpdate.Description = updatedRecipe.Description;

                await _recipeRepository.UpdateAsync(recipeToUpdate);

                response.Data = _mapper.Map<GetRecipeDto>(recipeToUpdate);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> AddIngredientToRecipe(AddRecipeIngredientDto newIngredientRecipe)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeIngredient = _mapper.Map<RecipeIngredient>(newIngredientRecipe);

                var recipe = await _recipeRepository.AddIngredientToRecipe(recipeIngredient);

                response.Data = _mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> AddUtensilToRecipe(AddRecipeUtensilDto newUtensilRecipe)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeUtensil = _mapper.Map<RecipeUtensil>(newUtensilRecipe);

                var recipe = await _recipeRepository.AddUtensilToRecipe(recipeUtensil);

                response.Data = _mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public async Task<Response<GetRecipeDto>> AddStepToRecipe(AddRecipeStepDto newRecipeStep)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeStep = _mapper.Map<RecipeStep>(newRecipeStep);

                var recipe = await _recipeRepository.AddStepToRecipe(recipeStep);

                response.Data = _mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}