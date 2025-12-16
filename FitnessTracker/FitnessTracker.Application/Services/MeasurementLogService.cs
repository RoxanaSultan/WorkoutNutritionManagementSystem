using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class MeasurementLogService :
    GenericService<MeasurementLog>,
    IMeasurementLogService
{
    public MeasurementLogService(IMeasurementLogRepository measurementLogRepository)
        : base(measurementLogRepository)
    {
        
    }
}
