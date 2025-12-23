using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.Validators;

public class WorkoutServiceValidator : IWorkoutServiceValidator
{
    private readonly IWorkoutRepository _workoutRepository;

    public WorkoutServiceValidator(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    private static DateTime TruncateToMinute(DateTime dt)
        => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, dt.Kind);

    public async Task<Result> ValidateCreateAsync(User user, CreateWorkoutDto dto)
    {
        var dateToMinute = TruncateToMinute(dto.Date);

        // Date: cannot be in the future
        if (dateToMinute > DateTime.Now)
            return Result.Fail("Workout date cannot be in the future.");

        // Prevent workouts before user registration date
        if (dateToMinute < user.RegistrationDate)
            return Result.Fail("Workout date must be greater than or equal to user's registration date.");

        // DurationMinutes: positive + realistic (5..300)
        if (dto.DurationInMinutes <= 0)
            return Result.Fail("Duration must be a positive number.");
        if (dto.DurationInMinutes < 5 || dto.DurationInMinutes > 300)
            return Result.Fail("Duration must be between 5 and 300 minutes.");

        // An user cannot log more than one workout with the same start time (to minute)
        var exists = await _workoutRepository.ExistsSameStartTimeAsync(dto.UserId, dateToMinute);
        if (exists)
            return Result.Fail("User cannot log more than one workout with the same start time.");

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(User user, UpdateWorkoutDto dto, int workoutId)
    {
        var dateToMinute = TruncateToMinute(dto.Date);

        if (dateToMinute > DateTime.Now)
            return Result.Fail("Workout date cannot be in the future.");

        if (dateToMinute < user.RegistrationDate)
            return Result.Fail("Workout date must be greater than or equal to user's registration date.");

        if (dto.DurationInMinutes <= 0)
            return Result.Fail("Duration must be a positive number.");
        if (dto.DurationInMinutes < 5 || dto.DurationInMinutes > 300)
            return Result.Fail("Duration must be between 5 and 300 minutes.");

        var exists = await _workoutRepository.ExistsSameStartTimeAsync(user.Id, dateToMinute, ignoreWorkoutId: workoutId);
        if (exists)
            return Result.Fail("User cannot log more than one workout with the same start time.");

        return Result.Ok();
    }
}
