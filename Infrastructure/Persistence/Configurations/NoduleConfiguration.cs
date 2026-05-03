using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NoduleConfiguration : IEntityTypeConfiguration<Nodule>
{
    public void Configure(EntityTypeBuilder<Nodule> builder)
    {
        builder.ToTable("Nodules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Classification)
            .HasConversion<string>();

        builder.HasOne(x => x.Report)
            .WithMany(r => r.Nodules)
            .HasForeignKey(x => x.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}