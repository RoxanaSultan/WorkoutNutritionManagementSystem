using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;
    public ExerciseService(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<Result<Exercise>> CreateAsync(Exercise exercise)
    {
        await _exerciseRepository.CreateAsync(exercise);
        return Result<Exercise>.Success(exercise);
    }

    public async Task<Result<Exercise>> DeleteAsync(int id)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(id);

        if (exercise == null)
        {
            return Result<Exercise>.Failure("Exercise not found.");
        }

        await _exerciseRepository.DeleteAsync(exercise);
        return Result<Exercise>.Success(exercise);
    }

    public bool ExerciseExists(int id)
    {
        return _exerciseRepository.ExerciseExists(id);
    }

    public async Task<Result<IEnumerable<Exercise>>> GetAllAsync()
    {
        var exercises = await _exerciseRepository.GetAllAsync();
        return Result<IEnumerable<Exercise>>.Success(exercises);
    }

    public async Task<Result<Exercise>> GetByIdAsync(int id)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(id);

        if (exercise == null)
        {
            return Result<Exercise>.Failure("Exercise not found.");
        }

        return Result<Exercise>.Success(exercise);
    }

    public async Task<Result<Exercise>> UpdateAsync(int id, Exercise exercise)
    {
        if (id != exercise.Id)
        {
            return Result<Exercise>.Failure("Id mismatch.");
        }

        var existingExercise = await _exerciseRepository.GetByIdAsync(id);
        if (existingExercise == null)
        {
            return Result<Exercise>.Failure("Exercise not found.");
        }

        await _exerciseRepository.UpdateAsync(exercise);
        return Result<Exercise>.Success(exercise);
    }
}
