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
            .IsRequired()
            .HasColumnType("TEXT"); // مناسب لـ SQLite

        builder.Property(x => x.PlanDate)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP"); // مهم لـ SQLite

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        // العلاقة مع Patient
        builder.HasOne(x => x.Patient)
            .WithMany(p => p.TreatmentPlans)
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // العلاقة مع Clinician
        builder.HasOne(x => x.Clinician)
            .WithMany()
            .HasForeignKey(x => x.ClinicianId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}