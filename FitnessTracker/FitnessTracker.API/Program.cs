using FitnessTracker.API.Middleware;
using FitnessTracker.Application.Configuration;
using FitnessTracker.Application.Interfaces.Repositories;
using FitnessTracker.Application.Interfaces.Services;
using FitnessTracker.Application.Interfaces.ServiceValidators;
using FitnessTracker.Application.Services;
using FitnessTracker.Application.Services.ServiceValidators;
using FitnessTracker.Application.Validators.User;
using FitnessTracker.Infrastructure;
using FitnessTracker.Infrastructure.Data;
using FitnessTracker.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FitnessTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitnessTrackerDbConnectionString"));

    options.LogTo(Console.WriteLine, LogLevel.Information);

    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

builder.Services.AddValidatorsFromAssembly(typeof(CreateUserDtoValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();

builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

builder.Services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
builder.Services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();

builder.Services.AddScoped<IFoodLogRepository, FoodLogRepository>();
builder.Services.AddScoped<IFoodLogService, FoodLogService>();

builder.Services.AddScoped<IMeasurementLogRepository, MeasurementLogRepository>();
builder.Services.AddScoped<IMeasurementLogService, MeasurementLogService>();

builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IGoalService, GoalService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IUserServiceValidator, UserServiceValidator>();
builder.Services.AddScoped<IGoalServiceValidator, GoalServiceValidator>();
builder.Services.AddScoped<IWorkoutServiceValidator, WorkoutServiceValidator>();
builder.Services.AddScoped<IWorkoutExerciseServiceValidator, WorkoutExerciseServiceValidator>();
builder.Services.AddScoped<IExerciseServiceValidator, ExerciseServiceValidator>();
builder.Services.AddScoped<IFoodItemServiceValidator, FoodItemServiceValidator>();
builder.Services.AddScoped<IFoodLogServiceValidator, FoodLogServiceValidator>();
builder.Services.AddScoped<IMeasurementLogServiceValidator, MeasurementLogServiceValidator>();

builder.Services.AddAutoMapper(cfg => { }, typeof(UserProfile).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
        .CreateLogger("RequestLogger");

    logger.LogInformation("HTTP {Method} {Path} started",
        context.Request.Method, context.Request.Path);

    await next();

    logger.LogInformation("HTTP {Method} {Path} finished with {StatusCode}",
        context.Request.Method, context.Request.Path, context.Response.StatusCode);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();



app.MapControllers();

app.Run();
