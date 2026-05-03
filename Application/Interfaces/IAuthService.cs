using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Auth;
namespace Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticate user and return JWT token
        /// </summary>
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);

        /// <summary>
        /// Register a new user
        /// </summary>
        Task RegisterAsync(DTOs.Auth.RegisterUserDto dto, int? adminId);

        /// <summary>
        /// Check if any user exists
        /// </summary>
        Task<bool> HasAnyUsersAsync();

        /// <summary>
        /// Logout user by revoking JWT token
        /// </summary>
        Task LogoutAsync(string token);
    }
}
