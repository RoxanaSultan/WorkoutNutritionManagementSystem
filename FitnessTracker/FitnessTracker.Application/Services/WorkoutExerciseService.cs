using AutoMapper;
using FitnessTracker.Application.DTOs.WorkoutExercise;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class WorkoutExerciseService : IWorkoutExerciseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWorkoutExerciseServiceValidator _validator;

    public WorkoutExerciseService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IWorkoutExerciseServiceValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<WorkoutExerciseDto>>> GetAllAsync()
    {
        var items = await _unitOfWork.WorkoutExercises.GetAllAsync();
        return Result<IEnumerable<WorkoutExerciseDto>>.Ok(_mapper.Map<IEnumerable<WorkoutExerciseDto>>(items));
    }

    public async Task<Result<WorkoutExerciseDto>> GetByIdAsync(int workoutId, int exerciseId)
    {
        var entity = await _unitOfWork.WorkoutExercises.GetByIdAsync(workoutId, exerciseId);
        if (entity == null)
            return Result<WorkoutExerciseDto>.Fail("WorkoutExercise not found.");

        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(entity));
    }

    public async Task<Result<WorkoutExerciseDto>> CreateAsync(CreateWorkoutExerciseDto dto)
    {
        var validation = await _validator.ValidateCreateAsync(dto);
        if (!validation.Success)
            return Result<WorkoutExerciseDto>.Fail(validation.Error);

        var entity = _mapper.Map<WorkoutExercise>(dto);
        await _unitOfWork.WorkoutExercises.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(entity));
    }

    public async Task<Result<WorkoutExerciseDto>> UpdateAsync(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto)
    {
        var existing = await _unitOfWork.WorkoutExercises.GetByIdAsync(workoutId, exerciseId);
        if (existing == null)
            return Result<WorkoutExerciseDto>.Fail("WorkoutExercise not found.");

        _mapper.Map(dto, existing);
        await _unitOfWork.WorkoutExercises.UpdateAsync(existing);
        await _unitOfWork.SaveChangesAsync();

        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(existing));
    }

    public async Task<Result<WorkoutExerciseDto>> DeleteAsync(int workoutId, int exerciseId)
    {
        var existing = await _unitOfWork.WorkoutExercises.GetByIdAsync(workoutId, exerciseId);
        if (existing == null)
            return Result<WorkoutExerciseDto>.Fail("WorkoutExercise not found.");

        await _unitOfWork.WorkoutExercises.DeleteAsync(workoutId, exerciseId);
        await _unitOfWork.SaveChangesAsync();

        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(existing));
    }
}