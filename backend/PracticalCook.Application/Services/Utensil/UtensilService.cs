using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Utensil;
using PracticalCook.Application.Services.UserService;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.UtensilService
{
    public class UtensilService(IMapper mapper, IUtensilRepository utensilRepository, IUserRepository userRepository) : IUtensilService
    {
        public async Task<Response<GetUtensilDto>> AddUtensil(AddUtensilDto newUtensil, Guid userId)
        {
            var response = new Response<GetUtensilDto>();
            try
            {
                var utensil = mapper.Map<Utensil>(newUtensil);
                var user = await userRepository.GetByGuidAsync(userId) ?? throw new Exception($"User with id {userId} not found!");
                utensil.CreatedByUser = user;

                await utensilRepository.AddAsync(utensil);

                response.Data = mapper.Map<GetUtensilDto>(utensil);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetUtensilDto>> DeleteUtensil(int id)
        {
            var response = new Response<GetUtensilDto>();
            try
            {
                var utensilToRemove = await utensilRepository.RemoveAsync(id) ?? throw new Exception($"Utensil with id {id} not found!");
                response.Data = mapper.Map<GetUtensilDto>(utensilToRemove);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetUtensilDto>> GetUtensilById(int id)
        {
            var response = new Response<GetUtensilDto>();
            try
            {
                var utensil = await utensilRepository.GetByIdAsync(id) ?? throw new Exception($"Utensil with id {id} not found!");

                response.Data = mapper.Map<GetUtensilDto>(utensil);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetUtensilDto>>> GetUtensils(Guid userId)
        {
            var response = new Response<List<GetUtensilDto>>();
            try
            {
                var utensils = await utensilRepository.GetUserUtensilsAsync(userId);
                response.Data = utensils.Select(i => mapper.Map<GetUtensilDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetUtensilDto>> UpdateUtensil(UpdateUtensilDto updatedUtensil)
        {
            var response = new Response<GetUtensilDto>();
            try
            {
                var utensilToUpdate = await utensilRepository.GetByIdAsync(updatedUtensil.Id) ?? throw new Exception($"Utensil with id {updatedUtensil.Id} not found!");

                utensilToUpdate.Name = updatedUtensil.Name;

                await utensilRepository.UpdateAsync(utensilToUpdate);

                response.Data = mapper.Map<GetUtensilDto>(utensilToUpdate);
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