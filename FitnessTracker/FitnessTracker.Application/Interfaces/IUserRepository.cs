using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByEmailExceptUserIdAsync(string email, int userId);
}
