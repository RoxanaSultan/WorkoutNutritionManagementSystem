using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.Repositories;

public interface IExerciseRepository : IGenericRepository<Exercise>
{
    Task<bool> ExistsByNameAsync(string normalizedName, int? ignoreExerciseId = null);
    Task<bool> IsUsedInPastWorkoutsAsync(int exerciseId, DateTime now);
}