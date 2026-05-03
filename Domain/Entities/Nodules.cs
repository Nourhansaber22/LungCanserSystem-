using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Nodule
    {
        public int Id { get; set; }

        public int ReportId { get; set; }
        public SmartReport Report { get; set; } = default!;

        public int NoduleIndex { get; set; }

        public double? VolumeCm3 { get; set; }
        public double? DiameterMm { get; set; }

        public double MalignancyScore { get; set; }

        public Diagnosis Classification { get; set; }

        public string? Location { get; set; }
        public string? Morphology { get; set; }

        public int? CoordinatesX { get; set; }
        public int? CoordinatesY { get; set; }
        public int? CoordinatesZ { get; set; }
    }
}