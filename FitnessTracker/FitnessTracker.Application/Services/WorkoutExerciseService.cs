//using AutoMapper;
//using FitnessTracker.Application.DTOs.WorkoutExercise;
//using FitnessTracker.Application.Interfaces;
//using FitnessTracker.Application.Results;
//using FitnessTracker.Application.Services.Validators;
//using FitnessTracker.Domain.Entities;

//namespace FitnessTracker.Application.Services;

//public class WorkoutExerciseService : IWorkoutExerciseService
//{
//    private readonly IWorkoutExerciseRepository _repo;
//    private readonly IMapper _mapper;
//    private readonly IWorkoutExerciseServiceValidator _validator;

//    public WorkoutExerciseService(
//        IWorkoutExerciseRepository repo,
//        IMapper mapper,
//        IWorkoutExerciseServiceValidator validator)
//    {
//        _repo = repo;
//        _mapper = mapper;
//        _validator = validator;
//    }

//    public async Task<Result<IEnumerable<WorkoutExerciseDto>>> GetAllAsync()
//    {
//        var items = await _repo.GetAllAsync();
//        return Result<IEnumerable<WorkoutExerciseDto>>.Ok(_mapper.Map<IEnumerable<WorkoutExerciseDto>>(items));
//    }

//    public async Task<Result<WorkoutExerciseDto>> GetByIdAsync(int workoutId, int exerciseId)
//    {
//        var entity = await _repo.GetByIdAsync(workoutId, exerciseId);
//        if (entity == null)
//            return Result<WorkoutExerciseDto>.Fail("WorkoutExercise not found.");

//        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(entity));
//    }

//    public async Task<Result<WorkoutExerciseDto>> CreateAsync(CreateWorkoutExerciseDto dto)
//    {
//        var validation = await _validator.ValidateCreateAsync(dto);
//        if (!validation.Success)
//            return Result<WorkoutExerciseDto>.Fail(validation.Error);

//        var entity = _mapper.Map<WorkoutExercise>(dto);
//        await _repo.CreateAsync(entity);

//        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(entity));
//    }

//    public async Task<Result<WorkoutExerciseDto>> UpdateAsync(int workoutId, int exerciseId, UpdateWorkoutExerciseDto dto)
//    {
//        var existing = await _repo.GetByIdAsync(workoutId, exerciseId);
//        if (existing == null)
//            return Result<WorkoutExerciseDto>.Fail("WorkoutExercise not found.");

//        _mapper.Map(dto, existing);
//        await _repo.UpdateAsync(existing);

//        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(existing));
//    }

//    public async Task<Result<WorkoutExerciseDto>> DeleteAsync(int workoutId, int exerciseId)
//    {
//        var existing = await _repo.GetByIdAsync(workoutId, exerciseId);
//        if (existing == null)
//            return Result<WorkoutExerciseDto>.Fail("WorkoutExercise not found.");

//        await _repo.DeleteAsync(workoutId, exerciseId);
//        return Result<WorkoutExerciseDto>.Ok(_mapper.Map<WorkoutExerciseDto>(existing));
//    }
//}
