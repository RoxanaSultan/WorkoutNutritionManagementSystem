using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByEmailExceptUserIdAsync(string email, int userId);
}
