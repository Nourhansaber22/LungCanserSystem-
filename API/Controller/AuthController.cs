using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ===============================
        // 🔐 Login
        // ===============================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //[HttpGet("test-auth")]
        //[Authorize]
        //public IActionResult TestAuth()
        //{
        //    return Ok("Authenticated ✅");
        //}


        //[HttpGet("debug-token")]
        //public IActionResult DebugToken()
        //{
        //    var authHeader = Request.Headers["Authorization"].ToString();

        //    return Ok(new
        //    {
        //        Header = authHeader,
        //        UserAuthenticated = User.Identity?.IsAuthenticated,
        //        Claims = User.Claims.Select(c => new { c.Type, c.Value })
        //    });
        //}


        //===========


        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        //[Authorize]
        //[AllowAnonymous]
        //[HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            await _authService.RegisterAsync(dto, adminId);

            return Ok(new { message = "User created successfully" });
        }

        //[HttpPost("register-open")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterOpen(RegisterUserDto dto)
        //{
        //    await _authService.RegisterAsync(dto, null);
        //    return Ok(new { message = "User created successfully (open)" });
        //}




        //[HttpPost("register-first")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterFirst(RegisterUserDto dto)
        //{
        //    await _authService.RegisterAsync(dto, null);
        //    return Ok(new { message = "First admin created" });
        //}


        // ===============================
        // 🚪 Logout
        // ===============================
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return BadRequest(new { message = "Invalid token" });

            var token = authHeader.Replace("Bearer ", "");

            await _authService.LogoutAsync(token);

            return Ok(new { message = "Logged out successfully" });
        }
    }
}