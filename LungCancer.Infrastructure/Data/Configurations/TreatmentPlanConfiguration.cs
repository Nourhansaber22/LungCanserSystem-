using LungCancer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TreatmentPlanConfiguration : IEntityTypeConfiguration<TreatmentPlan>
{
    public void Configure(EntityTypeBuilder<TreatmentPlan> builder)
    {
        builder.ToTable("TreatmentPlans");

        builder.HasKey(t => t.Id);

        // ========== Properties ==========

        builder.Property(t => t.PlanText)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(t => t.PlanDate)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .IsRequired(false);

        // ========== Relationships ==========

        // TreatmentPlan → Patient
        builder.HasOne(t => t.Patient)
            .WithMany(p => p.TreatmentPlans)
            .HasForeignKey(t => t.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // TreatmentPlan → Clinician (User)
        builder.HasOne(t => t.Clinician)
            .WithMany()
            .HasForeignKey(t => t.ClinicianId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}