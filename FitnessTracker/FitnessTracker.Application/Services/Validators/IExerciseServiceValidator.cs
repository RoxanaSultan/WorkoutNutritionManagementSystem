using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.Validators;

public interface IExerciseServiceValidator
{
    Task<Result> ValidateCreateAsync(CreateExerciseDto dto);
    Task<Result> ValidateUpdateAsync(int id, UpdateExerciseDto dto);
    Task<Result> ValidateDeleteAsync(int id);
}
