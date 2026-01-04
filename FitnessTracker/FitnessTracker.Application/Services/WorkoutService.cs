using AutoMapper;
using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWorkoutServiceValidator _validator;

    public WorkoutService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IWorkoutServiceValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<WorkoutDto>>> GetAllAsync()
    {
        var workouts = await _unitOfWork.Workouts.GetAllAsync();
        var workoutDtos = _mapper.Map<IEnumerable<WorkoutDto>>(workouts);
        return Result<IEnumerable<WorkoutDto>>.Ok(workoutDtos);
    }

    public async Task<Result<WorkoutDto>> GetByIdAsync(int id)
    {
        var workout = await _unitOfWork.Workouts.GetByIdAsync(id);
        if (workout == null)
        {
            return Result<WorkoutDto>.Fail("Workout not found.");
        }

        return Result<WorkoutDto>.Ok(_mapper.Map<WorkoutDto>(workout));
    }

    public async Task<Result<WorkoutDto>> CreateAsync(CreateWorkoutDto createWorkoutDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(createWorkoutDto.UserId);
        if (user == null)
        {
            return Result<WorkoutDto>.Fail($"User with id {createWorkoutDto.UserId} not found.");
        }

        var validation = await _validator.ValidateCreateAsync(user, createWorkoutDto);
        if (!validation.Success)
        {
            return Result<WorkoutDto>.Fail(validation.Error);
        }

        var workout = _mapper.Map<Workout>(createWorkoutDto);
        await _unitOfWork.Workouts.CreateAsync(workout);
        await _unitOfWork.SaveChangesAsync();

        var workoutDto = _mapper.Map<WorkoutDto>(workout);
        return Result<WorkoutDto>.Ok(workoutDto);
    }

    public async Task<Result<WorkoutDto>> UpdateAsync(int id, UpdateWorkoutDto updateWorkoutDto)
    {
        var existingWorkout = await _unitOfWork.Workouts.GetByIdAsync(id);
        if (existingWorkout == null)
        {
            return Result<WorkoutDto>.Fail("Workout not found.");
        }

        var user = await _unitOfWork.Users.GetByIdAsync(existingWorkout.UserId);
        if (user == null)
        {
            return Result<WorkoutDto>.Fail("User not found.");
        }

        var validation = await _validator.ValidateUpdateAsync(user, updateWorkoutDto, id);
        if (!validation.Success)
        {
            return Result<WorkoutDto>.Fail(validation.Error);
        }

        _mapper.Map(updateWorkoutDto, existingWorkout);
        await _unitOfWork.Workouts.UpdateAsync(existingWorkout);
        await _unitOfWork.SaveChangesAsync();

        var workoutDto = _mapper.Map<WorkoutDto>(existingWorkout);
        return Result<WorkoutDto>.Ok(workoutDto);
    }

    public async Task<Result<WorkoutDto>> DeleteAsync(int id)
    {
        var workout = await _unitOfWork.Workouts.GetByIdAsync(id);
        if (workout == null)
        {
            return Result<WorkoutDto>.Fail("Workout not found.");
        }

        await _unitOfWork.Workouts.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        var workoutDto = _mapper.Map<WorkoutDto>(workout);
        return Result<WorkoutDto>.Ok(workoutDto);
    }

    public async Task<Result<IEnumerable<WorkoutExerciseDto>>> GetExercisesByWorkout(int workoutId)
    {
        var workout = await _unitOfWork.Exercises.GetByIdAsync(workoutId);
        if (workout == null)
        {
            return Result<IEnumerable<WorkoutExerciseDto>>.Fail("Workout not found.");
        }

        var workoutExercises = await _unitOfWork.Workouts.GetWorkoutExercisesByWorkoutIdAsync(workoutId);

        var dtos = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercises);
        return Result<IEnumerable<WorkoutExerciseDto>>.Ok(dtos);
    }

    public async Task<Result> AddExerciseAsync(int workoutId, CreateWorkoutExerciseDto dto)
    {
        var workout = await _unitOfWork.Workouts.GetByIdAsync(workoutId);
        if (workout == null)
        {
            return Result.Fail("Workout not found.");
        }

        var exercise = await _unitOfWork.Exercises.GetByIdAsync(dto.ExerciseId);
        if (exercise == null)
        {
            return Result.Fail("Exercise not found.");
        }

        var exists = await _unitOfWork.Workouts.WorkoutExerciseExistsAsync(workoutId, dto.ExerciseId);
        if (exists)
        {
            return Result.Fail("This exercise is already added to the workout.");
        }

        var entity = _mapper.Map<WorkoutExercise>(dto);
        entity.WorkoutId = workoutId;

        await _unitOfWork.Workouts.AddWorkoutExerciseAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> UpdateExerciseAsync(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto)
    {
        var existing = await _unitOfWork.Workouts.GetWorkoutExerciseAsync(workoutId, exerciseId);
        if (existing == null)
        {
            return Result.Fail("Workout exercise not found.");
        }

        existing.Sets = dto.Sets;
        existing.Reps = dto.Reps;
        existing.WeightUsed = dto.WeightUsed;

        await _unitOfWork.Workouts.UpdateWorkoutExerciseAsync(existing);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> RemoveExerciseAsync(int workoutId, int exerciseId)
    {
        var existing = await _unitOfWork.Workouts.GetWorkoutExerciseAsync(workoutId, exerciseId);
        if (existing == null)
        {
            return Result.Fail("Workout exercise not found.");
        }

        await _unitOfWork.Workouts.RemoveWorkoutExerciseAsync(existing);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }
}
