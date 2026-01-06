using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class FoodLogRepository : GenericRepository<FoodLog>, IFoodLogRepository
{
    FitnessTrackerDbContext _context;   
    public FoodLogRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task DeleteByUserIdAsync(int userId)
    {
        await _context.FoodLogs
            .Where(fl => fl.UserId == userId)
            .ExecuteDeleteAsync();
    }
}
