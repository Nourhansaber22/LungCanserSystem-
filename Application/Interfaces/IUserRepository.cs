using Application.Interfaces.Common;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        //Task<User?> GetByIdAsync(int id);
        //Task<IEnumerable<User>> GetAllAsync();
        //Task AddAsync(User user);
        //void Update(User user);
        //Task SaveChangesAsync();
    }
}