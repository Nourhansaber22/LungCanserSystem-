using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class SmartReport
    {
        public int Id { get; set; }

        public int ScanId { get; set; }
        public Scan Scan { get; set; } = default!;

        public Diagnosis Diagnosis { get; set; }
        public decimal ConfidenceScore { get; set; }

        public string? HeatmapPath { get; set; }

        public bool HasPriorScan { get; set; } = false;

        public int? PriorScanId { get; set; }
        public Scan? PriorScan { get; set; }

        public decimal? VolumeChangePct { get; set; }

        public string? PredictionData { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.UtcNow;

        public int? ReviewedBy { get; set; }
        public User? ReviewedByUser { get; set; }
        public DateTime? ReviewedAt { get; set; }

        // Navigation
        public ICollection<Nodule> Nodules { get; set; } = new List<Nodule>();
    }
}