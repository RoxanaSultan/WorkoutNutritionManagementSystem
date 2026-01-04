using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IWorkoutServiceValidator
{
    Task<Result> ValidateCreateAsync(User user, CreateWorkoutDto dto);
    Task<Result> ValidateUpdateAsync(User user, UpdateWorkoutDto dto, int workoutId);
}
