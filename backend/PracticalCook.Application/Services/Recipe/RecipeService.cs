using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Services.UserService;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.RecipeService
{
    public class RecipeService(IMapper mapper, IRecipeRepository recipeRepository, IUserRepository userRepository) : IRecipeService
    {
        public async Task<Response<GetRecipeDto>> AddRecipe(AddRecipeDto newRecipe, Guid userId)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var user = await userRepository.GetByGuidAsync(userId) ?? throw new Exception($"User with id {userId} not found!");
                var recipe = mapper.Map<Recipe>(newRecipe);
                recipe.CreatedByUser = user;

                await recipeRepository.AddAsync(recipe);

                user.CreatedRecipes.Add(recipe);
                var userRecipe = new UserRecipe
                {
                    UserId = user.Id,
                    RecipeId = recipe.Id
                };

                user.UserRecipes.Add(userRecipe);
                await userRepository.UpdateAsync(user);

                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> AddFullRecipe(AddFullRecipeDto newRecipe, Guid userId)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var user = await userRepository.GetByGuidAsync(userId) ?? throw new Exception($"User with id {userId} not found!");
                var recipe = mapper.Map<Recipe>(newRecipe);
                recipe.CreatedByUser = user;

                await recipeRepository.AddAsync(recipe);

                user.CreatedRecipes.Add(recipe);
                var userRecipe = new UserRecipe
                {
                    UserId = user.Id,
                    RecipeId = recipe.Id
                };

                user.UserRecipes.Add(userRecipe);
                await userRepository.UpdateAsync(user);

                response.Data = mapper.Map<GetRecipeDto>(recipe);
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
                    recipesToAdd.Add(mapper.Map<Recipe>(recipe));
                }

                await recipeRepository.AddMultipleRecipes(recipesToAdd);
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
                var recipeToRemove = await recipeRepository.RemoveAsync(id) ?? throw new Exception($"Recipe with id {id} not found!");

                response.Data = mapper.Map<GetRecipeDto>(recipeToRemove);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> GetRecipeById(int id)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipe = await recipeRepository.GetRecipeWithDetails(id) ?? throw new Exception($"Recipe with id {id} not found!");

                response.Data = mapper.Map<GetRecipeDto>(recipe);
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
                var recipes = await recipeRepository.GetAllAsync();
                response.Data = recipes.Select(i => mapper.Map<GetRecipeDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetRecipeDto>>> GetUserRecipes(Guid userId)
        {
            var response = new Response<List<GetRecipeDto>>();
            try
            {
                var recipes = await recipeRepository.GetUserRecipesAsync(userId);
                response.Data = recipes.Select(i => mapper.Map<GetRecipeDto>(i)).ToList();
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
                var recipeToUpdate = await recipeRepository.GetByIdAsync(updatedRecipe.Id) ?? throw new Exception($"Recipe with id {updatedRecipe.Id} not found!");

                recipeToUpdate.Name = updatedRecipe.Name;
                recipeToUpdate.Description = updatedRecipe.Description;

                await recipeRepository.UpdateAsync(recipeToUpdate);

                response.Data = mapper.Map<GetRecipeDto>(recipeToUpdate);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> AddIngredientToRecipe(int recipeId, AddRecipeIngredientDto newIngredientRecipe)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeIngredient = mapper.Map<RecipeIngredient>(newIngredientRecipe);
                recipeIngredient.RecipeId = recipeId;

                var recipe = await recipeRepository.AddIngredientToRecipe(recipeIngredient);

                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipe = await recipeRepository.RemoveIngredientFromRecipe(recipeId, ingredientId);
                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<GetRecipeDto>> AddUtensilToRecipe(int recipeId, AddRecipeUtensilDto newUtensilRecipe)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeUtensil = mapper.Map<RecipeUtensil>(newUtensilRecipe);
                recipeUtensil.RecipeId = recipeId;

                var recipe = await recipeRepository.AddUtensilToRecipe(recipeUtensil);

                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> RemoveUtensilFromRecipe(int recipeId, int utensilId)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipe = await recipeRepository.RemoveUtensilFromRecipe(recipeId, utensilId);
                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<GetRecipeDto>> AddStepToRecipe(int recipeId, AddRecipeStepDto newRecipeStep)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeStep = mapper.Map<Step>(newRecipeStep);
                recipeStep.RecipeId = recipeId;

                var recipe = await recipeRepository.AddStepToRecipe(recipeStep);

                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetRecipeDto>> RemoveStepFromRecipe(int recipeId, int stepId)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipe = await recipeRepository.RemoveStepFromRecipe(recipeId, stepId);
                response.Data = mapper.Map<GetRecipeDto>(recipe);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<GetRecipeDto>> UpdateStepInRecipe(int recipeId, UpdateRecipeStepDto updatedStep)
        {
            var response = new Response<GetRecipeDto>();
            try
            {
                var recipeStep = await recipeRepository.GetByIdAsync(recipeId) ?? throw new Exception($"Recipe with id {recipeId} not found!");

                var stepUpdated = new Step(recipeStep)
                {
                    Id = updatedStep.Id,
                    Title = updatedStep.Title,
                    Description = updatedStep.Description,
                    StepOrder = updatedStep.StepOrder
                };

                var recipe = await recipeRepository.UpdateStepInRecipe(recipeId, stepUpdated);
                response.Data = mapper.Map<GetRecipeDto>(recipe);
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