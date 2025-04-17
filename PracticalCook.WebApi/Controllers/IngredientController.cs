using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Services.IngredientService;

namespace PracticalCook.Controllers
{
    [ApiController]
    [Route("Ingredients")]
    public class IngredientController(IIngredientService ingredientService, ILogger logger) : ControllerBase
    {
        private readonly IIngredientService ingredientService = ingredientService;

        private readonly ILogger logger = logger;

        [HttpGet]
        public async Task<ActionResult<Response<List<GetIngredientDto>>>> GetIngredients()
        {
            return Ok(await this.ingredientService.GetIngredients());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetIngredientDto>>> GetIngredientById(int id)
        {
            var result = await this.ingredientService.GetIngredientById(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetIngredientDto>>> AddIngredient(AddIngredientDto newIngredient)
        {
            return Ok(await this.ingredientService.AddIngredient(newIngredient));
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetIngredientDto>>> UpdateIngredient(UpdateIngredientDto Ingredient)
        {
            var result = await this.ingredientService.UpdateIngredient(Ingredient);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetIngredientDto>>> RemoveIngredient(int id)
        {
            var result = await this.ingredientService.DeleteIngredient(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}