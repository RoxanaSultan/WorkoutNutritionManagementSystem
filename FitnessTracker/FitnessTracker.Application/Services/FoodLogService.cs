using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class FoodLogService : GenericService<FoodLog>, IFoodLogService
{
    public FoodLogService(IFoodLogRepository foodLogRepository)
        : base(foodLogRepository)
    {
        
    }
}
