using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Services.UserService;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.IngredientService
{
    public class IngredientService(IMapper mapper, IIngredientRepository ingredientRepository, IUserRepository userRepository) : IIngredientService
    {
        public async Task<Response<GetIngredientDto>> AddIngredient(AddIngredientDto newIngredient, Guid userId)
        {
            var response = new Response<GetIngredientDto>();
            try
            {
                var user = await userRepository.GetByGuidAsync(userId) ?? throw new Exception($"User with id {userId} not found!");
                var ingredient = mapper.Map<Ingredient>(newIngredient);
                ingredient.CreatedByUser = user;
                var createdIngredient = await ingredientRepository.AddAsync(ingredient);

                response.Data = mapper.Map<GetIngredientDto>(createdIngredient);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetIngredientDto>> DeleteIngredient(int id)
        {
            var response = new Response<GetIngredientDto>();
            try
            {
                var ingredientToRemove = await ingredientRepository.RemoveAsync(id) ?? throw new Exception($"Ingredient with id {id} not found!");

                response.Data = mapper.Map<GetIngredientDto>(ingredientToRemove);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetIngredientDto>> GetIngredientById(int id)
        {
            var response = new Response<GetIngredientDto>();
            try
            {
                var ingredient = await ingredientRepository.GetByIdAsync(id) ?? throw new Exception($"Ingredient with id {id} not found!");

                response.Data = mapper.Map<GetIngredientDto>(ingredient);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetIngredientDto>>> GetIngredients()
        {
            var response = new Response<List<GetIngredientDto>>();
            try
            {
                var ingredients = await ingredientRepository.GetAllAsync();
                response.Data = ingredients.Select(i => mapper.Map<GetIngredientDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetIngredientDto>> UpdateIngredient(UpdateIngredientDto updatedIngredient)
        {
            var response = new Response<GetIngredientDto>();
            try
            {
                var ingredientToUpdate = await ingredientRepository.GetByIdAsync(updatedIngredient.Id) ?? throw new Exception($"Ingredient with id {updatedIngredient.Id} not found!");

                ingredientToUpdate.Name = updatedIngredient.Name;
                ingredientToUpdate.Type = updatedIngredient.Type;
                var updated = await ingredientRepository.UpdateAsync(ingredientToUpdate);

                response.Data = mapper.Map<GetIngredientDto>(updated);
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