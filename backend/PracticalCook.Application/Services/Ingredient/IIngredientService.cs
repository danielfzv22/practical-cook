using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;

namespace PracticalCook.Application.Services.IngredientService
{
    public interface IIngredientService
    {
        Task<Response<List<GetIngredientDto>>> GetIngredients(Guid userId);

        Task<Response<GetIngredientDto>> GetIngredientById(int id);

        Task<Response<GetIngredientDto>> AddIngredient(AddIngredientDto newIngredient, Guid userId);

        Task<Response<GetIngredientDto>> UpdateIngredient(UpdateIngredientDto updatedIngredient);

        Task<Response<GetIngredientDto>> DeleteIngredient(int id);
    }
}