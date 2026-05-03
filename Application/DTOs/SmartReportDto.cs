using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record SmartReportDto(
        int Id,
        int ScanId,
        string Diagnosis,
        double ConfidenceScore,
        string? HeatmapPath,
        bool HasPriorScan,
        int? PriorScanId,
        double? VolumeChangePct,
        string? PredictionData,
        DateTime ReportDate,
        int? ReviewedBy,
        DateTime? ReviewedAt,
        ICollection<NoduleDto> Nodules
    );

    public record NoduleDto(
        int Id,
        int NoduleIndex,
        double? VolumeCm3,
        double? DiameterMm,
        double MalignancyScore,
        string Classification,
        string? Location,
        string? Morphology,
        int? X,
        int? Y,
        int? Z
    );
}
