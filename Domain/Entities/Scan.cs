using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Scan
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

        public int UploadedBy { get; set; }
        public User UploadedByUser { get; set; } = default!;

        public string FilePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        public DateTime? ScanDate { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public ScanStatus Status { get; set; } = ScanStatus.Pending;
        public string? Result { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public SmartReport? SmartReport { get; set; }
    }
}