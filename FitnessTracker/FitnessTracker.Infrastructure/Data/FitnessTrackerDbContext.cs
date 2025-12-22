using FitnessTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Data;

public partial class FitnessTrackerDbContext : DbContext
{
    public FitnessTrackerDbContext()
    {
    }

    public FitnessTrackerDbContext(DbContextOptions<FitnessTrackerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<FoodItem> FoodItems { get; set; }

    public virtual DbSet<FoodLog> FoodLogs { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<MeasurementLog> MeasurementLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FitnessTracker;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exercise__3214EC0703C15951");

            entity.ToTable("Exercise");

            entity.HasIndex(e => e.Name, "UQ__Exercise__737584F614484D1A").IsUnique();

            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MuscleGroup)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FoodItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodItem__3214EC0762EF1925");

            entity.ToTable("FoodItem");

            entity.HasIndex(e => e.Name, "UQ__FoodItem__737584F61D76C7FF").IsUnique();

            entity.Property(e => e.Carbs).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Fat).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Protein).HasColumnType("decimal(6, 2)");
        });

        modelBuilder.Entity<FoodLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FoodLog__3214EC078B53DB25");

            entity.ToTable("FoodLog");

            entity.Property(e => e.LogDate).HasPrecision(0);
            entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodLogs)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodLog__FoodId__36B12243");

            entity.HasOne(d => d.User).WithMany(p => p.FoodLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodLog__UserId__35BCFE0A");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Goal__3214EC07B44C0955");

            entity.ToTable("Goal");

            entity.Property(e => e.GoalType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TargetValue).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Goals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Goal__UserId__2E1BDC42");
        });

        modelBuilder.Entity<MeasurementLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Measurem__3214EC0751ABFA45");

            entity.ToTable("MeasurementLog");

            entity.Property(e => e.Arms).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.BodyFatPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Chest).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Date).HasPrecision(0);
            entity.Property(e => e.Waist).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.MeasurementLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Measureme__UserI__3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E94AA397");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344E0F5227").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workout__3214EC07823629DF");

            entity.ToTable("Workout");

            entity.HasIndex(e => new { e.UserId, e.Date }, "UQ__Workout__60FB4B9D4605B466").IsUnique();

            entity.Property(e => e.Date).HasPrecision(0);
            entity.Property(e => e.Notes).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Workouts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Workout__UserId__44FF419A");
        });

        modelBuilder.Entity<WorkoutExercise>(entity =>
        {
            entity.ToTable("WorkoutExercise");

            entity.HasKey(e => new { e.WorkoutId, e.ExerciseId });

            entity.Property(e => e.WeightUsed).HasColumnType("decimal(6, 2)");

            entity.HasOne(we => we.Workout)
                  .WithMany(w => w.WorkoutExercises)
                  .HasForeignKey(we => we.WorkoutId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__WorkoutEx__Worko__4D94879B");

            entity.HasOne(we => we.Exercise)
                  .WithMany()
                  .HasForeignKey(we => we.ExerciseId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__WorkoutEx__Exerc__4E88ABD4");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
