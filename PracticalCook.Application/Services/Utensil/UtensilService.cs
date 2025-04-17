using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Utensil;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.UtensilService
{
    public class UtensilService(IMapper mapper, IUtensilRepository utensilRepository) : IUtensilService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUtensilRepository _utensilRepository = utensilRepository;

        public async Task<Response<GetUtensilDto>> AddUtensil(AddUtensilDto newUtensil)
        {
            var response = new Response<GetUtensilDto>();
            try
            {
                var utensil = _mapper.Map<Utensil>(newUtensil);
                await _utensilRepository.AddAsync(utensil);

                response.Data = _mapper.Map<GetUtensilDto>(utensil);
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
                var utensilToRemove = await _utensilRepository.RemoveAsync(id) ?? throw new Exception($"Utensil with id {id} not found!");
                response.Data = _mapper.Map<GetUtensilDto>(utensilToRemove);
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
                var utensil = await _utensilRepository.GetByIdAsync(id) ?? throw new Exception($"Utensil with id {id} not found!");

                response.Data = _mapper.Map<GetUtensilDto>(utensil);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetUtensilDto>>> GetUtensils()
        {
            var response = new Response<List<GetUtensilDto>>();
            try
            {
                var utensils = await _utensilRepository.GetAllAsync();
                response.Data = utensils.Select(i => _mapper.Map<GetUtensilDto>(i)).ToList();
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
                var utensilToUpdate = await _utensilRepository.GetByIdAsync(updatedUtensil.Id) ?? throw new Exception($"Utensil with id {updatedUtensil.Id} not found!");

                utensilToUpdate.Name = updatedUtensil.Name;

                await _utensilRepository.UpdateAsync(utensilToUpdate);

                response.Data = _mapper.Map<GetUtensilDto>(utensilToUpdate);
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