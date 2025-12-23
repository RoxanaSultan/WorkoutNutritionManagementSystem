using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services.Validators;

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
            return Result.Fail("Name is required.");

        var exists = await _exerciseRepository.ExistsByNameAsync(name);
        if (exists)
            return Result.Fail("An exercise with the same name already exists.");

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(int id, UpdateExerciseDto dto)
    {
        var name = dto.Name;

        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Name is required.");

        var exists = await _exerciseRepository.ExistsByNameAsync(name, ignoreExerciseId: id);
        if (exists)
            return Result.Fail("An exercise with the same name already exists.");

        return Result.Ok();
    }

    public async Task<Result> ValidateDeleteAsync(int id)
    {
        var usedInPast = await _exerciseRepository.IsUsedInPastWorkoutsAsync(id, DateTime.Now);
        if (usedInPast)
            return Result.Fail("Exercise can't be deleted because it belongs to previous workouts.");

        return Result.Ok();
    }
}
