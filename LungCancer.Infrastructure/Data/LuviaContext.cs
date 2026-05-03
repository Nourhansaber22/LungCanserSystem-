using LungCancer.Domain.Entities;
using Luvia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LungCancer.Infrastructure.DbContexts
{
    public class LuviaContext : DbContext
    {
        public LuviaContext(DbContextOptions<LuviaContext>options):base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Scan> Scans => Set<Scan>();
        public DbSet<SmartReport> SmartReports => Set<SmartReport>();
        public DbSet<Nodule> Nodules => Set<Nodule>();
        public DbSet<TreatmentPlan> TreatmentPlans => Set<TreatmentPlan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuviaContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
