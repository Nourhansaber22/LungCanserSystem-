using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SmartReportConfiguration : IEntityTypeConfiguration<SmartReport>
{
    public void Configure(EntityTypeBuilder<SmartReport> builder)
    {
        builder.ToTable("SmartReports");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Diagnosis)
            .HasConversion<string>();

        // ✅ التعديل الوحيد
        builder.Property(x => x.ConfidenceScore)
            .HasConversion<double>();

        builder.HasOne(x => x.Scan)
            .WithOne(s => s.SmartReport)
            .HasForeignKey<SmartReport>(x => x.ScanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ScanId).IsUnique();

        builder.HasOne(x => x.PriorScan)
            .WithMany()
            .HasForeignKey(x => x.PriorScanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}