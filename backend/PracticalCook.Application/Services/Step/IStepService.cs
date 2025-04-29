using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Step;

namespace PracticalCook.Application.Services.StepService
{
    public interface IStepService
    {
        Task<Response<List<GetStepDto>>> GetSteps();

        Task<Response<GetStepDto>> GetStepById(int id);

        Task<Response<GetStepDto>> AddStep(AddStepDto newStep);

        Task<Response<GetStepDto>> UpdateStep(UpdateStepDto updatedStep);

        Task<Response<GetStepDto>> DeleteStep(int id);


    }
}