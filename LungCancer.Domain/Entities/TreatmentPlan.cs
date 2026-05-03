using Luvia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancer.Domain.Entities
{
    public class TreatmentPlan
    {

        public int Id { get; private set; }

        public int PatientId { get; private set; }
        public int ClinicianId { get; private set; }

        public string PlanText { get; private set; }

        public DateTime PlanDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Patient Patient { get; private set; }
        public User Clinician { get; private set; }

        private TreatmentPlan() { }

        public TreatmentPlan(int patientId, int clinicianId, string planText, DateTime planDate)
        {
            PatientId = patientId;
            ClinicianId = clinicianId;
            PlanText = planText;
            PlanDate = planDate;

            CreatedAt = DateTime.UtcNow;
        }

        public void UpdatePlan(string newText)
        {
            PlanText = newText;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
