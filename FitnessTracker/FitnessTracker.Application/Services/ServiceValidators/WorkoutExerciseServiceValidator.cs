//using FitnessTracker.Application.DTOs.WorkoutExercise;
//using FitnessTracker.Application.Interfaces;
//using FitnessTracker.Application.Results;

//namespace FitnessTracker.Application.Services.Validators;

//public class WorkoutExerciseServiceValidator : IWorkoutExerciseServiceValidator
//{
//    private readonly IWorkoutRepository _workoutRepository;
//    private readonly IExerciseRepository _exerciseRepository;
//    private readonly IWorkoutExerciseRepository _workoutExerciseRepository;

//    public WorkoutExerciseServiceValidator(
//        IWorkoutRepository workoutRepository,
//        IExerciseRepository exerciseRepository,
//        IWorkoutExerciseRepository workoutExerciseRepository)
//    {
//        _workoutRepository = workoutRepository;
//        _exerciseRepository = exerciseRepository;
//        _workoutExerciseRepository = workoutExerciseRepository;
//    }

//    public async Task<Result> ValidateCreateAsync(CreateWorkoutExerciseDto dto)
//    {
//        var workout = await _workoutRepository.GetByIdAsync(dto.);
//        if (workout == null)
//            return Result.Fail("Workout not found.");

//        var exercise = await _exerciseRepository.GetByIdAsync(dto.ExerciseId);
//        if (exercise == null)
//            return Result.Fail("Exercise not found.");

//        var exists = await _workoutExerciseRepository.ExistsAsync(dto.WorkoutId, dto.ExerciseId);
//        if (exists)
//            return Result.Fail("This exercise is already added to the workout.");

//        return Result.Ok();
//    }
//}
