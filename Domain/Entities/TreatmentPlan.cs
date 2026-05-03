using System;

namespace Domain.Entities
{
    public class TreatmentPlan
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

        public int ClinicianId { get; set; }
        public User Clinician { get; set; } = default!;

        public string PlanText { get; set; } = string.Empty;
        public DateTime PlanDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}