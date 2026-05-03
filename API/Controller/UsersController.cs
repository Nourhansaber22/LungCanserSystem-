using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // بس Admin يقدر يتحكم في Users
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // ===============================
        // Create User
        // POST: api/User
        // ===============================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            await _userService.CreateUserAsync(dto);
            return Ok(new { message = "User created successfully" });
        }

        // ===============================
        // Get All Users
        // GET: api/User
        // ===============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // ===============================
        // Get User by ID
        // GET: api/User/{id}
        // ===============================
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // ===============================
        // Update User
        // PUT: api/User/{id}
        // ===============================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            await _userService.UpdateUserAsync(id, dto);
            return Ok(new { message = "User updated successfully" });
        }

        // ===============================
        // Toggle User Active/Inactive
        // PATCH: api/User/{id}/toggle
        // ===============================
        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id, [FromBody] bool isActive)
        {
            await _userService.ToggleUserStatusAsync(id, isActive);
            return Ok(new { message = $"User is now {(isActive ? "Active" : "Inactive")}" });
        }
    }
}