using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Luvia.Infrastructure.Persistence;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.AIServices
{
    public class ScanService : IScanService
    {
        private readonly IHostEnvironment _env;
        private readonly LuviaDbContext _context;

        // السماح بامتدادات معينة فقط
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".png", ".dcm" };

        public ScanService(IHostEnvironment env, LuviaDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<string> UploadScanAsync(int patientId, IFormFile file, int uploadedBy)
        {
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded");

            // تنظيف اسم الملف
            var sanitizedFileName = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(sanitizedFileName).ToLower();
            if (!_allowedExtensions.Contains(extension))
                throw new Exception("Unsupported file type");

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{sanitizedFileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var scan = new Scan
            {
                PatientId = patientId,
                UploadedBy = uploadedBy,
                FilePath = filePath,
                FileName = sanitizedFileName,
                UploadDate = DateTime.UtcNow,
                Status = ScanStatus.Pending,
                Notes = string.Empty // تجنب null
            };

            _context.Scans.Add(scan);
            await _context.SaveChangesAsync();

            // Fire-and-forget AI processing
            _ = ProcessScanAsync(filePath);

            return filePath;
        }

        public async Task<string> ProcessScanAsync(string filePath)
        {
            try
            {
                await Task.Delay(5000); // محاكاة عملية AI

                var result = "No Tumor Detected"; // مثال نتيجة

                var scan = await _context.Scans.FirstOrDefaultAsync(s => s.FilePath == filePath);
                if (scan != null)
                {
                    scan.Status = ScanStatus.Completed;
                    scan.Notes = result;
                    await _context.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                // سجل الخطأ بدل ما يضيع
                Console.WriteLine($"Error processing scan: {ex.Message}");
                return "Processing failed";
            }
        }
    }
}