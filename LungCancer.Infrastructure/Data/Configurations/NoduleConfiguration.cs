using LungCancer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NoduleConfiguration : IEntityTypeConfiguration<Nodule>
{
    public void Configure(EntityTypeBuilder<Nodule> builder)
    {
        builder.ToTable("Nodules");

        builder.HasKey(n => n.Id);

        // ========== Core Fields ==========

        builder.Property(n => n.NoduleIndex)
            .IsRequired();

        builder.Property(n => n.VolumeCm3)
            .HasColumnType("decimal(8,4)")
            .IsRequired(false);

        builder.Property(n => n.DiameterMm)
            .HasColumnType("decimal(6,2)")
            .IsRequired(false);

        builder.Property(n => n.MalignancyScore)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(n => n.Classification)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(n => n.Location)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(n => n.Morphology)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(n => n.CoordinatesX)
            .IsRequired(false);

        builder.Property(n => n.CoordinatesY)
            .IsRequired(false);

        builder.Property(n => n.CoordinatesZ)
            .IsRequired(false);

        // ========== Relationship ==========

        builder.HasOne(n => n.Report)
            .WithMany(r => r.Nodules)
            .HasForeignKey(n => n.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}