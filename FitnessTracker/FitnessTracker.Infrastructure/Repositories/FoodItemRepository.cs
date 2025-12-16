using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;

namespace FitnessTracker.Infrastructure.Repositories;

public class FoodItemRepository : GenericRepository<FoodItem>, IFoodItemRepository
{
    //private readonly FitnessTrackerDbContext _context;

    public FoodItemRepository(FitnessTrackerDbContext context)
        : base(context)
    {
        //_context = context;
    }

    //public async Task<IEnumerable<FoodItem>> GetAllAsync()
    //{
    //    return await _context.FoodItems.ToListAsync();
    //}

    //public async Task<FoodItem?> GetByIdAsync(int id)
    //{
    //    return await _context.FoodItems.FindAsync(id);
    //}

    //public async Task CreateAsync(FoodItem foodItem)
    //{
    //    _context.FoodItems.Add(foodItem);
    //    await _context.SaveChangesAsync();
    //}

    //public async Task UpdateAsync(FoodItem foodItem)
    //{
    //    _context.Entry(foodItem).State = EntityState.Modified;
    //    await _context.SaveChangesAsync();
    //}

    //public async Task DeleteAsync(FoodItem foodItem)
    //{
    //    _context.FoodItems.Remove(foodItem);
    //    await _context.SaveChangesAsync();
    //}
}
