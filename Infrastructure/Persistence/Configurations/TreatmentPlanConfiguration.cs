using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TreatmentPlanConfiguration : IEntityTypeConfiguration<TreatmentPlan>
{
    public void Configure(EntityTypeBuilder<TreatmentPlan> builder)
    {
        builder.ToTable("TreatmentPlans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlanText)
            .IsRequired();

        builder.HasOne(x => x.Patient)
            .WithMany(p => p.TreatmentPlans)
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey(x => x.ClinicianId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}