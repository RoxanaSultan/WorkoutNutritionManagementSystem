using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class WorkoutService : GenericService<Workout>, IWorkoutService
{
    //private readonly IWorkoutRepository _workoutRepository;
    public WorkoutService(IWorkoutRepository workoutRepository)
        : base(workoutRepository)
    {
        //_workoutRepository = workoutRepository;
    }

    //public async Task<Result<Workout>> CreateAsync(Workout workout)
    //{
    //    await _workoutRepository.CreateAsync(workout);
    //    return Result<Workout>.Success(workout);
    //}

    //public async Task<Result<Workout>> DeleteAsync(int id)
    //{
    //    var workout = await _workoutRepository.GetByIdAsync(id);

    //    if (workout == null)
    //    {
    //        return Result<Workout>.Failure("Workout not found.");
    //    }

    //    await _workoutRepository.DeleteAsync(workout);
    //    return Result<Workout>.Success(workout);
    //}

    //public async Task<Result<IEnumerable<Workout>>> GetAllAsync()
    //{
    //    var workouts = await _workoutRepository.GetAllAsync();
    //    return Result<IEnumerable<Workout>>.Success(workouts);
    //}

    //public async Task<Result<Workout>> GetByIdAsync(int id)
    //{
    //    var workout = await _workoutRepository.GetByIdAsync(id);

    //    if (workout == null)
    //    {
    //        return Result<Workout>.Failure("Workout not found.");
    //    }

    //    return Result<Workout>.Success(workout);
    //}

    //public async Task<Result<Workout>> UpdateAsync(int id, Workout workout)
    //{
    //    if (id != workout.Id)
    //    {
    //        return Result<Workout>.Failure("Id mismatch.");
    //    }

    //    var existingWorkout = await _workoutRepository.GetByIdAsync(id);
    //    if (existingWorkout == null)
    //    {
    //        return Result<Workout>.Failure("Workout not found.");
    //    }

    //    await _workoutRepository.UpdateAsync(workout);
    //    return Result<Workout>.Success(workout);
    //}
}
