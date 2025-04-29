using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Utensil;

namespace PracticalCook.Application.Services.UtensilService
{
    public interface IUtensilService
    {
        Task<Response<List<GetUtensilDto>>> GetUtensils();

        Task<Response<GetUtensilDto>> GetUtensilById(int id);

        Task<Response<GetUtensilDto>> AddUtensil(AddUtensilDto newUtensil);

        Task<Response<GetUtensilDto>> UpdateUtensil(UpdateUtensilDto updatedUtensil);

        Task<Response<GetUtensilDto>> DeleteUtensil(int id);
    }
}