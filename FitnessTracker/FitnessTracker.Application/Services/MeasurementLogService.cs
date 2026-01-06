using AutoMapper;
using FitnessTracker.Application.DTOs.MeasurementLog;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class MeasurementLogService : IMeasurementLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMeasurementLogServiceValidator _validator;

    public MeasurementLogService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMeasurementLogServiceValidator validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<MeasurementLogDto>>> GetAllAsync()
    {
        var measurementLogs = await _unitOfWork.MeasurementLogs.GetAllAsync();
        var measurementLogDtos = _mapper.Map<IEnumerable<MeasurementLogDto>>(measurementLogs);

        return Result<IEnumerable<MeasurementLogDto>>.Ok(measurementLogDtos);
    }

    public async Task<Result<MeasurementLogDto>> GetByIdAsync(int id)
    {
        var measurementLog = await _unitOfWork.MeasurementLogs.GetByIdAsync(id);

        if (measurementLog == null)
        {
            return Result<MeasurementLogDto>.Fail("Measurement log not found.");
        }

        var measurementLogDto = _mapper.Map<MeasurementLogDto>(measurementLog);

        return Result<MeasurementLogDto>.Ok(measurementLogDto);
    }

    public async Task<Result<MeasurementLogDto>> CreateAsync(CreateMeasurementLogDto createMeasurementLogDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(createMeasurementLogDto.UserId);

        if (user == null)
        {
            return Result<MeasurementLogDto>.Fail($"User with id {createMeasurementLogDto.UserId} not found.");
        }

        var measurementLog = _mapper.Map<MeasurementLog>(createMeasurementLogDto);

        var previous = await _unitOfWork.MeasurementLogs.GetLatestBeforeAsync(createMeasurementLogDto.UserId, createMeasurementLogDto.Date);
        var validation = await _validator.ValidateCreateAsync(previous, measurementLog);

        if (!validation.Success)
        {
            return Result<MeasurementLogDto>.Fail(validation.Error);
        }

        await _unitOfWork.MeasurementLogs.CreateAsync(measurementLog);
        await _unitOfWork.SaveChangesAsync();

        var measurementLogDto = _mapper.Map<MeasurementLogDto>(measurementLog);

        return Result<MeasurementLogDto>.Ok(measurementLogDto);
    }

    public async Task<Result<MeasurementLogDto>> UpdateAsync(int id, UpdateMeasurementLogDto updateMEasurementLogDto)
    {
        var existingMeasurementLog = await _unitOfWork.MeasurementLogs.GetByIdAsync(id);

        if (existingMeasurementLog == null)
        {
            return Result<MeasurementLogDto>.Fail("Measurement log not found.");
        }

        var user = await _unitOfWork.Users.GetByIdAsync(existingMeasurementLog.UserId);

        if (user == null)
        {
            return Result<MeasurementLogDto>.Fail("User not found.");
        }

        _mapper.Map(updateMEasurementLogDto, existingMeasurementLog);

        var previous = await _unitOfWork.MeasurementLogs.GetLatestBeforeAsync(existingMeasurementLog.UserId, existingMeasurementLog.Date, ignoreId: id);
        var validation = await _validator.ValidateUpdateAsync(previous, existingMeasurementLog);
        if (!validation.Success)
        {
            return Result<MeasurementLogDto>.Fail(validation.Error);
        }

        await _unitOfWork.MeasurementLogs.UpdateAsync(existingMeasurementLog);
        await _unitOfWork.SaveChangesAsync();

        var measurementLogDto = _mapper.Map<MeasurementLogDto>(existingMeasurementLog);

        return Result<MeasurementLogDto>.Ok(measurementLogDto);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var existingMeasurementLogDto = await _unitOfWork.MeasurementLogs.ExistsAsync(id);

        if (!existingMeasurementLogDto)
        {
            return Result.Fail("Measurement log not found.");
        }

        await _unitOfWork.MeasurementLogs.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
