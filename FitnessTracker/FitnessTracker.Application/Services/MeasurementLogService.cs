using AutoMapper;
using FitnessTracker.Application.DTOs.MeasurementLog;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Application.Services.Validators;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class MeasurementLogService : IMeasurementLogService
{
    private readonly IMeasurementLogRepository _measurementLogRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMeasurementLogServiceValidator _validator;

    public MeasurementLogService(
        IMeasurementLogRepository measurementLogRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IMeasurementLogServiceValidator validator)
    {
        _measurementLogRepository = measurementLogRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<MeasurementLogDto>>> GetAllAsync()
    {
        var logs = await _measurementLogRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<MeasurementLogDto>>(logs);
        return Result<IEnumerable<MeasurementLogDto>>.Ok(dtos);
    }

    public async Task<Result<MeasurementLogDto>> GetByIdAsync(int id)
    {
        var log = await _measurementLogRepository.GetByIdAsync(id);
        if (log == null)
            return Result<MeasurementLogDto>.Fail("Measurement log not found.");

        return Result<MeasurementLogDto>.Ok(_mapper.Map<MeasurementLogDto>(log));
    }

    public async Task<Result<MeasurementLogDto>> CreateAsync(CreateMeasurementLogDto dto)
    {
        // existence check
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            return Result<MeasurementLogDto>.Fail($"User with id {dto.UserId} not found.");

        // map to entity first (ca să dai validatorului aceleași tipuri ca în DB)
        var entity = _mapper.Map<MeasurementLog>(dto);

        // business: compare with previous measurement
        var previous = await _measurementLogRepository.GetLatestBeforeAsync(dto.UserId, dto.Date);
        var validation = await _validator.ValidateCreateAsync(previous, entity);
        if (!validation.Success)
            return Result<MeasurementLogDto>.Fail(validation.Error);

        await _measurementLogRepository.CreateAsync(entity);

        return Result<MeasurementLogDto>.Ok(_mapper.Map<MeasurementLogDto>(entity));
    }

    public async Task<Result<MeasurementLogDto>> UpdateAsync(int id, UpdateMeasurementLogDto dto)
    {
        var existing = await _measurementLogRepository.GetByIdAsync(id);
        if (existing == null)
            return Result<MeasurementLogDto>.Fail("Measurement log not found.");

        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
            return Result<MeasurementLogDto>.Fail("User not found.");

        // aplică update pe entity
        _mapper.Map(dto, existing);

        // business: compare with previous measurement, ignoring self
        var previous = await _measurementLogRepository.GetLatestBeforeAsync(dto.UserId, existing.Date, ignoreId: id);
        var validation = await _validator.ValidateUpdateAsync(previous, existing);
        if (!validation.Success)
            return Result<MeasurementLogDto>.Fail(validation.Error);

        await _measurementLogRepository.UpdateAsync(existing);

        return Result<MeasurementLogDto>.Ok(_mapper.Map<MeasurementLogDto>(existing));
    }

    public async Task<Result<MeasurementLogDto>> DeleteAsync(int id)
    {
        var existing = await _measurementLogRepository.GetByIdAsync(id);
        if (existing == null)
            return Result<MeasurementLogDto>.Fail("Measurement log not found.");

        await _measurementLogRepository.DeleteAsync(id);

        return Result<MeasurementLogDto>.Ok(_mapper.Map<MeasurementLogDto>(existing));
    }
}
