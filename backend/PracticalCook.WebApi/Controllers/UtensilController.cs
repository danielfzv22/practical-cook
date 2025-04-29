using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Utensil;
using PracticalCook.Application.Services.UtensilService;

namespace PracticalCook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtensilController : ControllerBase
    {
        private readonly IUtensilService utensilService;
        public UtensilController(IUtensilService utensilService)
        {
            this.utensilService = utensilService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetUtensilDto>>>> GetUtensils()
        {
            return Ok(await this.utensilService.GetUtensils());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetUtensilDto>>> GetUtensilById(int id)
        {
            var result = await this.utensilService.GetUtensilById(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetUtensilDto>>> AddUtensil(AddUtensilDto newUtensil)
        {
            return Ok(await this.utensilService.AddUtensil(newUtensil));
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetUtensilDto>>> UpdateUtensil(UpdateUtensilDto Utensil)
        {
            var result = await this.utensilService.UpdateUtensil(Utensil);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetUtensilDto>>> RemoveUtensil(int id)
        {
            var result = await this.utensilService.DeleteUtensil(id);
            if (result.Data is null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}