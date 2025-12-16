using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure.Repositories;

public class FoodLogRepository : GenericRepository<FoodLog>, IFoodLogRepository
{
    public FoodLogRepository(FitnessTrackerDbContext context)
        : base(context)
    {

    }
}
