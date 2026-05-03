using LungCancer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SmartReportConfiguration : IEntityTypeConfiguration<SmartReport>
{
    public void Configure(EntityTypeBuilder<SmartReport> builder)
    {
        builder.ToTable("SmartReports");

        builder.HasKey(r => r.Id);

        // ========== Core Fields ==========

        builder.Property(r => r.Diagnosis)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(r => r.ConfidenceScore)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(r => r.HeatmapPath)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(r => r.HasPriorScan)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(r => r.VolumeChangePct)
            .HasColumnType("decimal(6,2)")
            .IsRequired(false);

        builder.Property(r => r.PredictionData)
            .HasColumnType("nvarchar(max)") // SQL Server JSON
            .IsRequired(false);

        builder.Property(r => r.ReportDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(r => r.ReviewedAt)
            .IsRequired(false);

        // ========== Relationships ==========

        // 1️⃣ One-to-One with Scan
        builder.HasOne(r => r.Scan)
            .WithOne(s => s.SmartReport)
            .HasForeignKey<SmartReport>(r => r.ScanId)
            .OnDelete(DeleteBehavior.Cascade);

        // 2️⃣ Self reference → Prior Scan
        builder.HasOne(r => r.PriorScan)
            .WithMany()
            .HasForeignKey(r => r.PriorScanId)
            .OnDelete(DeleteBehavior.Restrict);

        // 3️⃣ Reviewed By Clinician
        builder.HasOne(r => r.Reviewer)
            .WithMany()
            .HasForeignKey(r => r.ReviewedById)
            .OnDelete(DeleteBehavior.SetNull);

        // 4️⃣ SmartReport → Nodules (One-to-Many)
        builder.HasMany(r => r.Nodules)
            .WithOne(n => n.Report)
            .HasForeignKey(n => n.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}