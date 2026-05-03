using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ScanConfiguration : IEntityTypeConfiguration<Scan>
{
    public void Configure(EntityTypeBuilder<Scan> builder)
    {
        builder.ToTable("Scans");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FilePath).IsRequired();
        builder.Property(x => x.FileName).IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<string>()
               .HasDefaultValue(ScanStatus.Pending);

        builder.Property(x => x.UploadDate)
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.Patient)
            .WithMany(p => p.Scans)
            .HasForeignKey(x => x.PatientId);

        builder.HasOne(x => x.UploadedByUser)
            .WithMany()
            .HasForeignKey(x => x.UploadedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}