using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class FoodItemRepository : GenericRepository<FoodItem>, IFoodItemRepository
{
    private readonly FitnessTrackerDbContext _context;

    public FoodItemRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameAsync(string name, int? ignoreId = null)
    {
        var query = _context.FoodItems.AsNoTracking().AsQueryable();

        if (ignoreId.HasValue)
        {
            query = query.Where(x => x.Id != ignoreId.Value);
        }

        return await query.AnyAsync(x =>
            x.Name != null &&
            x.Name == name
        );
    }
}
