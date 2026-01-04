using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class GoalRepository :
    GenericRepository<Goal>,
    IGoalRepository
{
    FitnessTrackerDbContext _context;
    public GoalRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsOverlappingGoalAsync(
        int userId,
        string goalType,
        DateOnly startDate,
        DateOnly endDate,
        int? ignoreGoalId = null)
    {
        return await _context.Goals.AnyAsync(g =>
            g.UserId == userId &&
            g.GoalType != goalType &&
            (ignoreGoalId == null || g.Id != ignoreGoalId.Value) &&
            g.StartDate <= endDate &&
            startDate <= g.EndDate
        );
    }
}
