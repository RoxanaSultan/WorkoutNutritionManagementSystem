using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IExerciseServiceValidator
{
    Task<Result> ValidateCreateAsync(CreateExerciseDto dto);
    Task<Result> ValidateUpdateAsync(int id, UpdateExerciseDto dto);
    Task<Result> ValidateDeleteAsync(int id);
}
