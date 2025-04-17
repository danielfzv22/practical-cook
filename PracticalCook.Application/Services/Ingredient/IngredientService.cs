using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.IngredientService
{
    public class IngredientService(IMapper mapper, IIngredientRepository ingredientRepository) : IIngredientService
    {
        private readonly IMapper _mapper = mapper;

        private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

        public async Task<Response<GetIngredientDto>> AddIngredient(AddIngredientDto newIngredient)
        {
            var response = new Response<GetIngredientDto>();
            try
            {
                var ingredient = _mapper.Map<Ingredient>(newIngredient);
                var createdIngredient = await _ingredientRepository.AddAsync(ingredient);

                response.Data = _mapper.Map<GetIngredientDto>(createdIngredient);
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
                var ingredientToRemove = await _ingredientRepository.RemoveAsync(id) ?? throw new Exception($"Ingredient with id {id} not found!");

                response.Data = _mapper.Map<GetIngredientDto>(ingredientToRemove);
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
                var ingredient = await _ingredientRepository.GetByIdAsync(id) ?? throw new Exception($"Ingredient with id {id} not found!");

                response.Data = _mapper.Map<GetIngredientDto>(ingredient);
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
                var ingredients = await _ingredientRepository.GetAllAsync();
                response.Data = ingredients.Select(i => _mapper.Map<GetIngredientDto>(i)).ToList();
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
                var ingredientToUpdate = await _ingredientRepository.GetByIdAsync(updatedIngredient.Id) ?? throw new Exception($"Ingredient with id {updatedIngredient.Id} not found!");

                ingredientToUpdate.Name = updatedIngredient.Name;
                ingredientToUpdate.Type = updatedIngredient.Type;
                var updated = await _ingredientRepository.UpdateAsync(ingredientToUpdate);

                response.Data = _mapper.Map<GetIngredientDto>(updated);
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