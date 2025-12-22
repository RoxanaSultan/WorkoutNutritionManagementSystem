using AutoMapper;
using FitnessTracker.Application.DTOs.Exercise;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _repo;
    private readonly IMapper _mapper;
    private readonly IExerciseServiceValidator _validator;

    public ExerciseService(IExerciseRepository repo, IMapper mapper, IExerciseServiceValidator validator)
    {
        _repo = repo;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<ExerciseDto>>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return Result<IEnumerable<ExerciseDto>>.Ok(_mapper.Map<IEnumerable<ExerciseDto>>(list));
    }

    public async Task<Result<ExerciseDto>> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return Result<ExerciseDto>.Fail("Exercise not found.");
        return Result<ExerciseDto>.Ok(_mapper.Map<ExerciseDto>(entity));
    }

    public async Task<Result<ExerciseDto>> CreateAsync(CreateExerciseDto dto)
    {
        var validation = await _validator.ValidateCreateAsync(dto);
        if (!validation.Success) return Result<ExerciseDto>.Fail(validation.Error);

        // Normalize fields before persist (optional but recommended)
        dto.Name = dto.Name.Trim();
        dto.MuscleGroup = string.IsNullOrWhiteSpace(dto.MuscleGroup) ? null : dto.MuscleGroup.Trim().ToLowerInvariant();
        dto.DifficultyLevel = string.IsNullOrWhiteSpace(dto.DifficultyLevel) ? null : dto.DifficultyLevel.Trim().ToLowerInvariant();

        var entity = _mapper.Map<Exercise>(dto);
        await _repo.CreateAsync(entity);

        return Result<ExerciseDto>.Ok(_mapper.Map<ExerciseDto>(entity));
    }

    public async Task<Result<ExerciseDto>> UpdateAsync(int id, UpdateExerciseDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return Result<ExerciseDto>.Fail("Exercise not found.");

        var validation = await _validator.ValidateUpdateAsync(id, dto);
        if (!validation.Success) return Result<ExerciseDto>.Fail(validation.Error);

        dto.Name = dto.Name.Trim();
        dto.MuscleGroup = string.IsNullOrWhiteSpace(dto.MuscleGroup) ? null : dto.MuscleGroup.Trim().ToLowerInvariant();
        dto.DifficultyLevel = string.IsNullOrWhiteSpace(dto.DifficultyLevel) ? null : dto.DifficultyLevel.Trim().ToLowerInvariant();

        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);

        return Result<ExerciseDto>.Ok(_mapper.Map<ExerciseDto>(existing));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return Result.Fail("Exercise not found.");

        var validation = await _validator.ValidateDeleteAsync(id);
        if (!validation.Success) return Result.Fail(validation.Error);

        await _repo.DeleteAsync(id);
        return Result.Ok();
    }
}
