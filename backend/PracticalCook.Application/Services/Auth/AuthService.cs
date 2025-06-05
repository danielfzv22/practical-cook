using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Auth;
using PracticalCook.Application.Dtos.Ingredient;
using PracticalCook.Application.Dtos.User;
using PracticalCook.Application.Services.UserService;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.Auth
{
    public class AuthService(IMapper mapper, IConfiguration configuration, IUserRepository userRepository) : IAuthService
    {
        public async Task<Response<GetUserDto>> RegisterAsync(UserDto request)
        {
            var response = new Response<GetUserDto>();
            try
            {
                var userExist = await userRepository.GetByUsernameAsync(request.Username);
                if (userExist != null)
                {
                    response.Success = false;
                    response.Message = "User already registered";

                    return response;
                }

                var user = new User();
                var hashedPassword = new PasswordHasher<User>()
                    .HashPassword(user, request.Password);

                user.Username = request.Username;
                user.PasswordHash = hashedPassword;
                user.Role = "User"; // Default role, can be changed later

                await userRepository.AddAsync(user);

                response.Data = mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<TokenResponseDto>> LoginAsync(UserDto request)
        {
            var response = new Response<TokenResponseDto>();
            try
            {
                var user = await userRepository.GetByUsernameAsync(request.Username);
                if (user is null)
                {
                    response.Success = false;
                    response.Message = "User not found";

                    return response;
                }

                var passwordVerificationResult = new PasswordHasher<User>()
                    .VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    response.Success = false;
                    response.Message = "Invalid credentials";

                    return response;
                }

                TokenResponseDto tokenResponse = await CreateTokenResponse(user);
                response.Data = tokenResponse;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var response = new Response<TokenResponseDto>();
            var user = await userRepository.GetByGuidAsync(request.UserId);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                response.Success = false;
                response.Message = "Invalid user or refresh token";
                return response;
            }

            response.Data = await CreateTokenResponse(user);

            return response;
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await userRepository.UpdateAsync(user);

            return refreshToken;
        }
    }
}
