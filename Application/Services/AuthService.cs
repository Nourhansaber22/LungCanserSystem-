using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenBlacklistService tokenBlacklistService,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenBlacklistService = tokenBlacklistService;
            _tokenService = tokenService;
        }

        // ===============================
        // 🔥 Login + JWT Generation
        // ===============================
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("User is deactivated");

            // ✅ Generate JWT token with Claims
            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto(token, user.Name, user.Role);
        }

        // ===============================
        // 🔹 Register New User
        // ===============================
        public async Task RegisterAsync(RegisterUserDto dto, int? adminId)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already in use");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = _passwordHasher.Hash(dto.Password),
                Role = dto.Role,
                IsActive = true,
                CreatedBy = adminId , // 0 لو أول مستخدم
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        // ===============================
        // 🔹 Check if Any Users Exist
        // ===============================
        public async Task<bool> HasAnyUsersAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            return allUsers != null && allUsers.Any();
        }

        // ===============================
        // 🔥 Logout & Revoke JWT Token
        // ===============================
        public async Task LogoutAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be empty");

            await _tokenBlacklistService.RevokeTokenAsync(token);
        }
    }
}