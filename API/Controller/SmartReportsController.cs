using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "Clinician")]
    public class SmartReportController : ControllerBase
    {
        private readonly ISmartReportService _reportService;

        public SmartReportController(ISmartReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{scanId}")]
        public async Task<IActionResult> GetReport(int scanId)
        {
            var report = await _reportService.GetReportAsync(scanId);
            return Ok(report);
        }

        [HttpGet("{scanId}/pdf")]
        public async Task<IActionResult> DownloadPdf(int scanId)
        {
            var pdf = await _reportService.GeneratePdfAsync(scanId);
            return File(pdf, "application/pdf", $"SmartReport_Scan_{scanId}.pdf");
        }
    }
}