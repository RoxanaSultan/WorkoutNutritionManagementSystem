using FitnessTracker.Application.Results;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Interfaces.ServiceValidators;

public interface IMeasurementLogServiceValidator
{
    Task<Result> ValidateCreateAsync(MeasurementLog? previous, MeasurementLog current);
    Task<Result> ValidateUpdateAsync(MeasurementLog? previous, MeasurementLog current);
}
