using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Dtos.Step;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.StepService
{
    public class StepService(IStepRepository stepRepository, IMapper mapper) : IStepService
    {
        private readonly IStepRepository _stepRepository = stepRepository;

        private readonly IMapper _mapper = mapper;

        public async Task<Response<GetStepDto>> AddStep(AddStepDto newStep)
        {
            var response = new Response<GetStepDto>();
            try
            {
                var step = _mapper.Map<Step>(newStep);
                await _stepRepository.AddAsync(step);

                response.Data = _mapper.Map<GetStepDto>(step);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetStepDto>> DeleteStep(int id)
        {
            var response = new Response<GetStepDto>();
            try
            {
                var stepToRemove = await _stepRepository.RemoveAsync(id) ?? throw new Exception($"Step with id {id} not found!");
                response.Data = _mapper.Map<GetStepDto>(stepToRemove);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetStepDto>> GetStepById(int id)
        {
            var response = new Response<GetStepDto>();
            try
            {
                var step = await _stepRepository.GetByIdAsync(id) ?? throw new Exception($"Step with id {id} not found!");

                response.Data = _mapper.Map<GetStepDto>(step);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<GetStepDto>>> GetSteps()
        {
            var response = new Response<List<GetStepDto>>();
            try
            {
                var steps = await _stepRepository.GetAllAsync();
                response.Data = steps.Select(i => _mapper.Map<GetStepDto>(i)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<GetStepDto>> UpdateStep(UpdateStepDto updatedStep)
        {
            var response = new Response<GetStepDto>();
            try
            {
                var stepToUpdate = await _stepRepository.GetByIdAsync(updatedStep.Id) ?? throw new Exception($"Step with id {updatedStep.Id} not found!");

                stepToUpdate.Title = updatedStep.Title;
                stepToUpdate.Description = updatedStep.DefaultDescription;

                await _stepRepository.UpdateAsync(stepToUpdate);

                response.Data = _mapper.Map<GetStepDto>(stepToUpdate);
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