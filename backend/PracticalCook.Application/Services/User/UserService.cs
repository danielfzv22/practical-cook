using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PracticalCook.Application.Common.Responses;
using PracticalCook.Application.Dtos.Recipe;
using PracticalCook.Application.Dtos.User;
using PracticalCook.Application.Services.RecipeService;
using PracticalCook.Application.Services.UserService;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Application.Services.UserService
{
    public class UserService(IMapper mapper, IUserRepository userRepository) : IUserService
    {
    }
}
