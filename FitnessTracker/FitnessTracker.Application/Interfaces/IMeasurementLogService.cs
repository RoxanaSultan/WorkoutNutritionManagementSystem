using FitnessTracker.Application.DTOs.MeasurementLog;
using FitnessTracker.Application.Results;

namespace FitnessTracker.Application.Interfaces;

public interface IMeasurementLogService
{
    Task<Result<IEnumerable<MeasurementLogDto>>> GetAllAsync();
    Task<Result<MeasurementLogDto>> GetByIdAsync(int id);
    Task<Result<MeasurementLogDto>> CreateAsync(CreateMeasurementLogDto dto);
    Task<Result<MeasurementLogDto>> UpdateAsync(int id, UpdateMeasurementLogDto dto);
    Task<Result<MeasurementLogDto>> DeleteAsync(int id);
}