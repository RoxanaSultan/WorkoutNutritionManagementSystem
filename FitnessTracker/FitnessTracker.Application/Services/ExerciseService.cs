using AutoMapper;
using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IExerciseServiceValidator _validator;

    public ExerciseService(IUnitOfWork unitOfWork, IMapper mapper, IExerciseServiceValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<ExerciseDto>>> GetAllAsync()
    {
        var exercises = await _unitOfWork.Exercises.GetAllAsync();
        var exerciseDtos = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        return Result<IEnumerable<ExerciseDto>>.Ok(exerciseDtos);
    }

    public async Task<Result<ExerciseDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Exercises.GetByIdAsync(id);
        if (entity == null) return Result<ExerciseDto>.Fail("Exercise not found.");
        return Result<ExerciseDto>.Ok(_mapper.Map<ExerciseDto>(entity));
    }

    public async Task<Result<ExerciseDto>> CreateAsync(CreateExerciseDto createExerciseDto)
    {
        var validation = await _validator.ValidateCreateAsync(createExerciseDto);
        if (!validation.Success)
        {
            return Result<ExerciseDto>.Fail(validation.Error);
        }

        var exercise = _mapper.Map<Exercise>(createExerciseDto);
        await _unitOfWork.Exercises.CreateAsync(exercise);
        await _unitOfWork.SaveChangesAsync();

        var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
        return Result<ExerciseDto>.Ok(exerciseDto);
    }

    public async Task<Result<ExerciseDto>> UpdateAsync(int id, UpdateExerciseDto updateExerciseDto)
    {
        var existingExercise = await _unitOfWork.Exercises.GetByIdAsync(id);
        if (existingExercise == null)
        {
            return Result<ExerciseDto>.Fail("Exercise not found.");
        }

        var validation = await _validator.ValidateUpdateAsync(id, updateExerciseDto);
        if (!validation.Success)
        {
            return Result<ExerciseDto>.Fail(validation.Error);
        }

        _mapper.Map(updateExerciseDto, existingExercise);
        await _unitOfWork.Exercises.UpdateAsync(existingExercise);
        await _unitOfWork.SaveChangesAsync();

        var exerciseDto = _mapper.Map<ExerciseDto>(existingExercise);
        return Result<ExerciseDto>.Ok(exerciseDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existingExercise = await _unitOfWork.Exercises.GetByIdAsync(id);
        if (existingExercise == null)
        {
            return Result.Fail("Exercise not found.");
        }

        var validation = await _validator.ValidateDeleteAsync(id);
        if (!validation.Success)
        {
            return Result.Fail(validation.Error);
        }

        await _unitOfWork.Exercises.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }
}
