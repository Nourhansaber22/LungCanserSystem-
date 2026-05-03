using LungCancer.Domain.Entities;
using Luvia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luvia.Infrastructure.Data.Configurations;

public class ScanConfiguration : IEntityTypeConfiguration<Scan>
{
    public void Configure(EntityTypeBuilder<Scan> builder)
    {
        builder.ToTable("Scans");

        builder.HasKey(s => s.Id);

        // ========== Properties ==========

        builder.Property(s => s.FilePath)
            .IsRequired()
            .HasMaxLength(500);   // مطابق للـ schema

        builder.Property(s => s.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(s => s.FileSizeKb)
            .IsRequired(false);

        builder.Property(s => s.ScanDate)
            .IsRequired()
            .HasColumnType("date"); // عشان يبقى DATE مش datetime

        builder.Property(s => s.UploadDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue("pending")
            .IsRequired();

        builder.Property(s => s.Notes)
            .HasColumnType("text")
            .IsRequired(false);

        // ========== Relationships ==========

        // Scan → Patient (Many to One)
        builder.HasOne(s => s.Patient)
            .WithMany(p => p.Scans)
            .HasForeignKey(s => s.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Scan → Uploaded By User
        builder.HasOne(s => s.UploadedBy)
            .WithMany()
            .HasForeignKey(s => s.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Scan → SmartReport (One-to-One)
        builder.HasOne(s => s.SmartReport)
            .WithOne(r => r.Scan)
            .HasForeignKey<SmartReport>(r => r.ScanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}