using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    //Task<IEnumerable<User>> GetAllAsync();
    //Task<User?> GetByIdAsync(int id);
    //Task CreateAsync(User user);
    //Task UpdateAsync(User user);
    //Task DeleteAsync(User user);
}
