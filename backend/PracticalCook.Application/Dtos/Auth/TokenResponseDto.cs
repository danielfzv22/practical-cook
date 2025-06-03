using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCook.Application.Dtos.Auth
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; } = string.Empty;

        public required string RefreshToken { get; set; } = string.Empty;        
    }
}
