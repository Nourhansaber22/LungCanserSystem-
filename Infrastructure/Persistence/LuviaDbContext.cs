using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Luvia.Infrastructure.Persistence;
public class LuviaDbContext : DbContext
{
    public LuviaDbContext(DbContextOptions<LuviaDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Patient> Patients { get; set; } = default!;
    public DbSet<Scan> Scans { get; set; } = default!;
    public DbSet<SmartReport> SmartReports { get; set; } = default!;
    public DbSet<Nodule> Nodules { get; set; } = default!;
    public DbSet<TreatmentPlan> TreatmentPlans { get; set; } = default!;
    public DbSet<RevokedToken> RevokedTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new ScanConfiguration());
        modelBuilder.ApplyConfiguration(new SmartReportConfiguration());
        modelBuilder.ApplyConfiguration(new TreatmentPlanConfiguration());
        modelBuilder.ApplyConfiguration(new NoduleConfiguration());

        // Apply all configurations from assembly as backup
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuviaDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}