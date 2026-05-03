using LungCancer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luvia.Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        // Primary Key
        builder.HasKey(p => p.Id);

        // Properties
        builder.Property(p => p.PatientCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.NationalId)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(p => p.NationalId)
               .IsUnique();

        builder.Property(p => p.FullName)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.DateOfBirth)
               .IsRequired();

        builder.Property(p => p.Gender)
               .HasConversion<string>()   // تخزين enum كـ string
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(p => p.ContactNumber)
               .HasMaxLength(20);

        builder.Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()")
               .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.Property(p => p.IsDeleted)
               .HasDefaultValue(false);

        // Relationship with User (CreatedBy)
      builder.HasOne(p => p.CreatedBy)
       .WithMany(u => u.Patients)
       .HasForeignKey(p => p.CreatedById)
       .OnDelete(DeleteBehavior.Restrict);

        // Relationship with Scan
        builder.HasMany(p => p.Scans)
               .WithOne(s => s.Patient)
               .HasForeignKey(s => s.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // Relationship with TreatmentPlan
        builder.HasMany(p => p.TreatmentPlans)
               .WithOne(t => t.Patient)
               .HasForeignKey(t => t.PatientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}