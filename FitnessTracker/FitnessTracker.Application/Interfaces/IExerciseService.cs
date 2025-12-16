using FitnessTracker.Domain.Entities;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces;

public interface IExerciseService
{
    Task<Result<IEnumerable<Exercise>>> GetAllAsync();
    Task<Result<Exercise>> GetByIdAsync(int id);
    Task<Result<Exercise>> CreateAsync(Exercise exercise);
    Task<Result<Exercise>> UpdateAsync(int id, Exercise exercise);
    Task<Result<Exercise>> DeleteAsync(int id);
    bool ExerciseExists(int id);
}
