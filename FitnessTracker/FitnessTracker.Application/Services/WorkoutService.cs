using AutoMapper;
using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IWorkoutServiceValidator _validator;

    public WorkoutService(
        IWorkoutRepository workoutRepository,
        IExerciseRepository exerciseRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IWorkoutServiceValidator validator)
    {
        _workoutRepository = workoutRepository;
        _exerciseRepository = exerciseRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<WorkoutDto>>> GetAllAsync()
    {
        var workouts = await _workoutRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<WorkoutDto>>(workouts);
        return Result<IEnumerable<WorkoutDto>>.Ok(dtos);
    }

    public async Task<Result<WorkoutDto>> GetByIdAsync(int id)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);
        if (workout == null)
            return Result<WorkoutDto>.Fail("Workout not found.");

        return Result<WorkoutDto>.Ok(_mapper.Map<WorkoutDto>(workout));
    }

    public async Task<Result<WorkoutDto>> CreateAsync(CreateWorkoutDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            return Result<WorkoutDto>.Fail($"User with id {dto.UserId} not found.");

        var validation = await _validator.ValidateCreateAsync(user, dto);
        if (!validation.Success)
            return Result<WorkoutDto>.Fail(validation.Error);

        var workout = _mapper.Map<Workout>(dto);
        await _workoutRepository.CreateAsync(workout);

        return Result<WorkoutDto>.Ok(_mapper.Map<WorkoutDto>(workout));
    }

    public async Task<Result<WorkoutDto>> UpdateAsync(int id, UpdateWorkoutDto dto)
    {
        var existing = await _workoutRepository.GetByIdAsync(id);
        if (existing == null)
            return Result<WorkoutDto>.Fail("Workout not found.");

        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            return Result<WorkoutDto>.Fail("User not found.");

        var validation = await _validator.ValidateUpdateAsync(user, dto, id);
        if (!validation.Success)
            return Result<WorkoutDto>.Fail(validation.Error);

        _mapper.Map(dto, existing);
        await _workoutRepository.UpdateAsync(existing);

        return Result<WorkoutDto>.Ok(_mapper.Map<WorkoutDto>(existing));
    }

    public async Task<Result<WorkoutDto>> DeleteAsync(int id)
    {
        var workout = await _workoutRepository.GetByIdAsync(id);
        if (workout == null)
            return Result<WorkoutDto>.Fail("Workout not found.");

        await _workoutRepository.DeleteAsync(id);
        return Result<WorkoutDto>.Ok(_mapper.Map<WorkoutDto>(workout));
    }

    public async Task<Result<IEnumerable<WorkoutExerciseDto>>> GetExercisesByWorkout(int workoutId)
    {
        var workout = await _workoutRepository.GetByIdAsync(workoutId);
        if (workout == null)
            return Result<IEnumerable<WorkoutExerciseDto>>.Fail("Workout not found.");

        var workoutExercises = await _workoutRepository.GetWorkoutExercisesByWorkoutIdAsync(workoutId);

        var dtos = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercises);
        return Result<IEnumerable<WorkoutExerciseDto>>.Ok(dtos);
    }

    public async Task<Result> AddExerciseAsync(int workoutId, CreateWorkoutExerciseDto dto)
    {
        var workout = await _workoutRepository.GetByIdAsync(workoutId);
        if (workout == null)
            return Result.Fail("Workout not found.");

        var exercise = await _exerciseRepository.GetByIdAsync(dto.ExerciseId);
        if (exercise == null)
            return Result.Fail("Exercise not found.");

        var exists = await _workoutRepository.WorkoutExerciseExistsAsync(workoutId, dto.ExerciseId);
        if (exists)
            return Result.Fail("This exercise is already added to the workout.");

        var entity = _mapper.Map<WorkoutExercise>(dto);
        entity.WorkoutId = workoutId;

        await _workoutRepository.AddWorkoutExerciseAsync(entity);
        return Result.Ok();
    }

    public async Task<Result> UpdateExerciseAsync(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto)
    {
        var existing = await _workoutRepository.GetWorkoutExerciseAsync(workoutId, exerciseId);
        if (existing == null)
            return Result.Fail("Workout exercise not found.");

        existing.Sets = dto.Sets;
        existing.Reps = dto.Reps;
        existing.WeightUsed = dto.WeightUsed;

        await _workoutRepository.UpdateWorkoutExerciseAsync(existing);
        return Result.Ok();
    }

    public async Task<Result> RemoveExerciseAsync(int workoutId, int exerciseId)
    {
        var existing = await _workoutRepository.GetWorkoutExerciseAsync(workoutId, exerciseId);
        if (existing == null)
            return Result.Fail("Workout exercise not found.");

        await _workoutRepository.RemoveWorkoutExerciseAsync(existing);
        return Result.Ok();
    }
}
