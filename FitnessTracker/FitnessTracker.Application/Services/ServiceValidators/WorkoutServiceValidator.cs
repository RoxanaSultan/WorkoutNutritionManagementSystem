using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.Workout;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Services.ServiceValidators;

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

        if (dateToMinute > DateTime.Now)
            return Result.Fail(ErrorMessages.WorkoutDateNotInFuture);

        if (dateToMinute < user.RegistrationDate)
            return Result.Fail(ErrorMessages.WorkoutDateBeforeUserRegistration);

        var exists = await _workoutRepository.ExistsSameStartTimeAsync(dto.UserId, dateToMinute);
        if (exists)
            return Result.Fail(ErrorMessages.WorkoutSameStartTimeNotAllowed);

        return Result.Ok();
    }

    public async Task<Result> ValidateUpdateAsync(User user, UpdateWorkoutDto dto, int workoutId)
    {
        var dateToMinute = TruncateToMinute(dto.Date);

        if (dateToMinute > DateTime.Now)
            return Result.Fail(ErrorMessages.WorkoutDateNotInFuture);

        if (dateToMinute < user.RegistrationDate)
            return Result.Fail(ErrorMessages.WorkoutDateBeforeUserRegistration);

        var exists = await _workoutRepository.ExistsSameStartTimeAsync(
            user.Id, dateToMinute, ignoreWorkoutId: workoutId);

        if (exists)
            return Result.Fail(ErrorMessages.WorkoutSameStartTimeNotAllowed);

        return Result.Ok();
    }
}
