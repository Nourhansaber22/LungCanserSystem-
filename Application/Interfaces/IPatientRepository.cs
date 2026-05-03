using Application.Interfaces.Common;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IEnumerable<Patient>> SearchAsync(string term);
    }
}