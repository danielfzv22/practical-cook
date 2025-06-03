using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Auth;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<Response<GetUserDto>> RegisterAsync(UserDto request);

        Task<Response<TokenResponseDto>> LoginAsync(UserDto request);

        Task<Response<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
