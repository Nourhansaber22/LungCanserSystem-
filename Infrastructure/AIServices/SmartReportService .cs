using Application.DTOs;
using Application.Interfaces;
using Luvia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Infrastructure.Services
{
    public class SmartReportService : ISmartReportService
    {
        private readonly LuviaDbContext _context;

        public SmartReportService(LuviaDbContext context)
        {
            _context = context;
        }

        public async Task<SmartReportDto> GetReportAsync(int scanId)
        {
            var report = await _context.SmartReports
                .Include(r => r.Nodules)
                .Include(r => r.Scan)
                .FirstOrDefaultAsync(r => r.ScanId == scanId);

            if (report == null)
                throw new Exception("Report not found");

            return new SmartReportDto(
                report.Id,
                report.ScanId,
                report.Diagnosis.ToString(),
                report.ConfidenceScore,
                report.HeatmapPath,
                report.HasPriorScan,
                report.PriorScanId,
                report.VolumeChangePct,
                report.PredictionData,
                report.ReportDate,
                report.ReviewedBy,
                report.ReviewedAt,
                report.Nodules.Select(n => new NoduleDto(
                    n.Id,
                    n.NoduleIndex,
                    n.VolumeCm3,
                    n.DiameterMm,
                    n.MalignancyScore,
                    n.Classification.ToString(),
                    n.Location,
                    n.Morphology,
                    n.CoordinatesX,
                    n.CoordinatesY,
                    n.CoordinatesZ
                )).ToList()
            );
        }

        public async Task<byte[]> GeneratePdfAsync(int scanId)
        {
            var report = await _context.SmartReports
                .Include(r => r.Nodules)
                .FirstOrDefaultAsync(r => r.ScanId == scanId);

            if (report == null)
                throw new Exception("Report not found");

            // محاكاة PDF بسيط كـ string → byte[]
            var pdfContent = new StringBuilder();
            pdfContent.AppendLine($"Smart Report for Scan {scanId}");
            pdfContent.AppendLine($"Diagnosis: {report.Diagnosis}");
            pdfContent.AppendLine($"Confidence: {report.ConfidenceScore}%");
            pdfContent.AppendLine($"Report Date: {report.ReportDate}");
            pdfContent.AppendLine($"Nodules Count: {report.Nodules.Count}");

            foreach (var n in report.Nodules)
            {
                pdfContent.AppendLine($"- Nodule {n.NoduleIndex}: {n.Classification}, Volume: {n.VolumeCm3} cm3, Malignancy: {n.MalignancyScore}%");
            }

            return Encoding.UTF8.GetBytes(pdfContent.ToString());
        }
    }
}