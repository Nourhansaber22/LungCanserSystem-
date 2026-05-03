using LungCancer.Domain.Enums;
using Luvia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancer.Domain.Entities
{
    public class SmartReport
    {
        public int Id { get; private set; }

        public int ScanId { get; private set; }

        public DiagnosisType Diagnosis { get; private set; }
        public decimal ConfidenceScore { get; private set; }

        public string? HeatmapPath { get; private set; }

        public bool HasPriorScan { get; private set; }
        public int? PriorScanId { get; private set; }

        public decimal? VolumeChangePct { get; private set; }

        public string? PredictionData { get; private set; }

        public DateTime ReportDate { get; private set; }

        public int? ReviewedById { get; private set; }
        public DateTime? ReviewedAt { get; private set; }

        public Scan Scan { get; private set; }
        public Scan? PriorScan { get; private set; }
        public User? Reviewer { get; private set; }

        public ICollection<Nodule> Nodules { get; private set; } = new List<Nodule>();

        private SmartReport() { }
    }
}
