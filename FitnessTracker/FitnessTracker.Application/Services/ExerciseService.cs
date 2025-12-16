using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class ExerciseService : GenericService<Exercise>, IExerciseService
{
    //private readonly IExerciseRepository _exerciseRepository;
    public ExerciseService(IExerciseRepository exerciseRepository)
        : base(exerciseRepository)
    {
        //_exerciseRepository = exerciseRepository;
    }

    //public async Task<Result<Exercise>> CreateAsync(Exercise exercise)
    //{
    //    await _exerciseRepository.CreateAsync(exercise);
    //    return Result<Exercise>.Success(exercise);
    //}

    //public async Task<Result<Exercise>> DeleteAsync(int id)
    //{
    //    var exercise = await _exerciseRepository.GetByIdAsync(id);

    //    if (exercise == null)
    //    {
    //        return Result<Exercise>.Failure("Exercise not found.");
    //    }

    //    await _exerciseRepository.DeleteAsync(exercise);
    //    return Result<Exercise>.Success(exercise);
    //}

    //public async Task<Result<IEnumerable<Exercise>>> GetAllAsync()
    //{
    //    var exercises = await _exerciseRepository.GetAllAsync();
    //    return Result<IEnumerable<Exercise>>.Success(exercises);
    //}

    //public async Task<Result<Exercise>> GetByIdAsync(int id)
    //{
    //    var exercise = await _exerciseRepository.GetByIdAsync(id);

    //    if (exercise == null)
    //    {
    //        return Result<Exercise>.Failure("Exercise not found.");
    //    }

    //    return Result<Exercise>.Success(exercise);
    //}

    //public async Task<Result<Exercise>> UpdateAsync(int id, Exercise exercise)
    //{
    //    if (id != exercise.Id)
    //    {
    //        return Result<Exercise>.Failure("Id mismatch.");
    //    }

    //    var existingExercise = await _exerciseRepository.GetByIdAsync(id);
    //    if (existingExercise == null)
    //    {
    //        return Result<Exercise>.Failure("Exercise not found.");
    //    }

    //    await _exerciseRepository.UpdateAsync(exercise);
    //    return Result<Exercise>.Success(exercise);
    //}
}
