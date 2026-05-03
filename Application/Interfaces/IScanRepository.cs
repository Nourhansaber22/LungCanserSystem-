using Application.Interfaces.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IScanRepository : IRepository<Scan>
    {
        Task<IEnumerable<Scan>> GetByPatientIdAsync(int patientId);
    }
}
