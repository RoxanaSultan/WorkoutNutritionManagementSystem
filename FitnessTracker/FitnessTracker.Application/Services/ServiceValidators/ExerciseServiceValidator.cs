using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.ServiceValidators;

public class ExerciseServiceValidator : IExerciseServiceValidator
{
    private readonly IExerciseRepository _exerciseRepository;

    public ExerciseServiceValidator(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Result> ValidateCreateAsync(CreateExerciseDto dto)
    {
        var name = dto.Name;
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Fail(ErrorMessages.ExerciseNameRequired);
        }

        var exists = await _exerciseRepository.ExistsByNameAsync(name);
        if (exists)
        {
            return Result.Fail(ErrorMessages.ExerciseNameAlreadyExists);
        }

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(int id, UpdateExerciseDto dto)
    {
        var name = dto.Name;
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Fail(ErrorMessages.ExerciseNameRequired);
        }

        var exists = await _exerciseRepository.ExistsByNameAsync(name, ignoreExerciseId: id);
        if (exists)
        {
            return Result.Fail(ErrorMessages.ExerciseNameAlreadyExists);
        }

        return Result.Ok();
    }

    public async Task<Result> ValidateDeleteAsync(int id)
    {
        var usedInPast = await _exerciseRepository.IsUsedInPastWorkoutsAsync(id, DateTime.UtcNow);
        if (usedInPast)
        {
            return Result.Fail(ErrorMessages.ExerciseCannotDeleteUsedInPastWorkouts);
        }

        return Result.Ok();
    }
}
