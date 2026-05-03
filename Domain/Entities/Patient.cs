using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? ContactNumber { get; set; }

        public int CreatedBy { get; set; }
        public User CreatedByUser { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<Scan> Scans { get; set; } = new List<Scan>();
        public ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();
    }
}