using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly IScanService _scanService;

        public ScanController(IScanService scanService)
        {
            _scanService = scanService;
        }

        [HttpPost("upload")]
        [Authorize(Roles = "Assistant,Clinician")]
        public async Task<IActionResult> Upload([FromForm] UploadScanRequestDto dto)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out int uploadedBy))
            {
                return Unauthorized("Invalid user token");
            }

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            var filePath = await _scanService.UploadScanAsync(dto.PatientId, dto.File, uploadedBy);

            return Ok(new { Message = "Upload successful", FilePath = filePath });
        }

    }
}