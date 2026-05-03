using LungCancer.Domain.Entities;
using LungCancer.Domain.Enums;
using Luvia.Domain.Entities;

public class Patient
{
    public int Id { get; private set; }

    public string PatientCode { get; private set; }
    public string NationalId { get; private set; }
    public string FullName { get; private set; }

    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }

    public string? ContactNumber { get; private set; }

    public int CreatedById { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public User CreatedBy { get; private set; }

    public ICollection<Scan> Scans { get; private set; } = new List<Scan>();
    public ICollection<TreatmentPlan> TreatmentPlans { get; private set; } = new List<TreatmentPlan>();

    private Patient() { }
}