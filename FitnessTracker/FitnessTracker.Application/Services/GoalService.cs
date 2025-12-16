using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services;

public class GoalService :
    GenericService<Goal>,
    IGoalService
{
    public GoalService(IGoalRepository goalRepository)
        : base(goalRepository)
    {
        
    }
}
