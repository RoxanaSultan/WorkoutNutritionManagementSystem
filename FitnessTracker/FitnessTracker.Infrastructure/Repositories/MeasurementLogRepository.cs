using FitnessTracker.Application.Interfaces;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Repositories;

public class MeasurementLogRepository : GenericRepository<MeasurementLog>, IMeasurementLogRepository
{
    private readonly FitnessTrackerDbContext _context;

    public MeasurementLogRepository(FitnessTrackerDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MeasurementLog?> GetLatestBeforeAsync(int userId, DateTime date, int? ignoreId = null)
    {
        var q = _context.MeasurementLogs
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.Date < date);

        if (ignoreId.HasValue)
            q = q.Where(x => x.Id != ignoreId.Value);

        return await q
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();
    }
}