using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure.Repositories;

public class MeasurementLogRepository : 
    GenericRepository<MeasurementLog>,
    IMeasurementLogRepository
{
    public MeasurementLogRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        
    }
}
