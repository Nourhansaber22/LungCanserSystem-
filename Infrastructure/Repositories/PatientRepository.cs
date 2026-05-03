using Application.Interfaces;
using Domain.Entities;
//using Infrastructure.Persistence;
using Luvia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(LuviaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Patient>> SearchAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Enumerable.Empty<Patient>();

            var ctx = (LuviaDbContext)_context;

            var lowered = term.ToLowerInvariant();

            return await ctx.Patients
                .Where(p =>
                    p.FullName.ToLower().Contains(lowered) ||
                    p.PatientCode.ToLower().Contains(lowered))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}