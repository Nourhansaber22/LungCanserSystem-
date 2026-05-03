using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; }= string.Empty;
        public string PasswordHash { get; set; }= string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedBy { get; set; }
        public User? Creator { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public ICollection<User> CreatedUsers { get; set; } = new List<User>();
        public ICollection<Patient> CreatedPatients { get; set; } = new List<Patient>();
        public ICollection<Scan> UploadedScans { get; set; } = new List<Scan>();
        public ICollection<SmartReport> ReviewedReports { get; set; } = new List<SmartReport>();
        public ICollection<TreatmentPlan> AuthoredTreatmentPlans { get; set; } = new List<TreatmentPlan>();
    }

}
