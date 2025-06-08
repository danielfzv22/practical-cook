using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Dtos.Utensil;
using PracticalCook.Application.Services.UtensilService;

namespace PracticalCook.WebApi.Controllers
{
    [ApiController]
    [Route("utensils")]
    public class UtensilController(IUtensilService utensilService, ILogger<UtensilController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Response<List<GetUtensilDto>>>> GetUtensils()
        {
            logger.LogInformation("GET /utensils - Fetching all utensils");
            try
            {
                var response = await utensilService.GetUtensils();
                var count = response.Data?.Count ?? 0;

                if (count == 0)
                    logger.LogWarning("GET /utensils - No utensils found");
                else
                    logger.LogInformation("GET /utensils - Retrieved {Count} utensils", count);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /utensils - Error fetching utensils");
                return StatusCode(500, new Response<List<GetUtensilDto>>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetUtensilDto>>> GetUtensilById(int id)
        {
            logger.LogInformation("GET /utensils/{Id} - Fetching utensil", id);
            try
            {
                var response = await utensilService.GetUtensilById(id);
                if (response.Data is null)
                {
                    logger.LogWarning("GET /utensils/{Id} - Utensil not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("GET /utensils/{Id} - Retrieved utensil {Name}", id, response.Data.Name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "GET /utensils/{Id} - Error fetching utensil", id);
                return StatusCode(500, new Response<GetUtensilDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Response<GetUtensilDto>>> AddUtensil([FromBody] AddUtensilDto newUtensil)
        {
            logger.LogInformation("POST /utensils - Adding new utensil: {Name}", newUtensil.Name);
            try
            {
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdString, out Guid userId))
                {
                    return Unauthorized(new Response<GetRecipeDto> { Success = false, Message = "Invalid user token." });
                }

                var response = await utensilService.AddUtensil(newUtensil, userId);
                if (!response.Success)
                    logger.LogWarning("POST /utensils - Failed to add utensil: {Message}", response.Message);
                else
                    logger.LogInformation("POST /utensils - Utensil added with ID {Id}", response.Data.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "POST /utensils - Error adding utensil");
                return StatusCode(500, new Response<GetUtensilDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetUtensilDto>>> UpdateUtensil([FromBody] UpdateUtensilDto utensilDto)
        {
            logger.LogInformation("PUT /utensils/{Id} - Updating utensil: {Name}", utensilDto.Id, utensilDto.Name);
            try
            {
                var response = await utensilService.UpdateUtensil(utensilDto);
                if (response.Data is null)
                {
                    logger.LogWarning("PUT /utensils/{Id} - Utensil not found", utensilDto.Id);
                    return NotFound(response);
                }

                logger.LogInformation("PUT /utensils/{Id} - Utensil updated successfully", utensilDto.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PUT /utensils/{Id} - Error updating utensil", utensilDto.Id);
                return StatusCode(500, new Response<GetUtensilDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<GetUtensilDto>>> RemoveUtensil(int id)
        {
            logger.LogInformation("DELETE /utensils/{Id} - Removing utensil", id);
            try
            {
                var response = await utensilService.DeleteUtensil(id);
                if (response.Data is null)
                {
                    logger.LogWarning("DELETE /utensils/{Id} - Utensil not found", id);
                    return NotFound(response);
                }

                logger.LogInformation("DELETE /utensils/{Id} - Utensil removed", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DELETE /utensils/{Id} - Error removing utensil", id);
                return StatusCode(500, new Response<GetUtensilDto>
                {
                    Success = false,
                    Message = "Internal server error."
                });
            }
        }
    }
}