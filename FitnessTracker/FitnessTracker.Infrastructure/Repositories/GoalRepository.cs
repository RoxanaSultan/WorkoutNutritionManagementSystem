using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure.Repositories;

public class GoalRepository : 
    GenericRepository<Goal>,
    IGoalRepository
{
    public GoalRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        
    }
}
