using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // ✅ CREATE PATIENT (Assistant only)
        [HttpPost]
        [Authorize(Roles = "Assistant")]
        public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out int assistantId))
            {
                return Unauthorized("Invalid user token");
            }

            var result = await _patientService.CreatePatientAsync(dto, assistantId);

            return Ok(result);
        }

        // ✅ UPDATE PATIENT (Assistant only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Assistant")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto dto)
        {
            var result = await _patientService.UpdatePatientAsync(id, dto);

            return Ok(result);
        }

        // ✅ SEARCH PATIENTS (Assistant + Clinician + Technician)
        [HttpGet]
        [Authorize(Roles = "Assistant,Clinician,Technician")]
        public async Task<IActionResult> Search([FromQuery] string? term)
        {
            var result = await _patientService.SearchPatientsAsync(term);

            return Ok(result);
        }

        // ✅ GET PATIENT BY ID (All roles)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);

            if (result == null)
                return NotFound("Patient not found");

            return Ok(result);
        }
    }
}