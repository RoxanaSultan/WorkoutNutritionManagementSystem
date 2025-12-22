using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces;

public interface IMeasurementLogRepository : IGenericRepository<MeasurementLog>
{
    Task<MeasurementLog?> GetLatestBeforeAsync(int userId, DateTime logDate, int? ignoreId = null);
}
