using LessonService.Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonService.Infrastructure.EF.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(500);
        builder.Property(e => e.DateFrom).IsRequired();
        builder.Property(e => e.Duration).IsRequired().HasDefaultValue(60);
        builder.Property(e => e.TrainingLevel).IsRequired().HasDefaultValue(TrainingLevel.Beginner).HasSentinel(TrainingLevel.Beginner);
        builder.Property(e => e.LessonType).IsRequired().HasDefaultValue(LessonType.None).HasSentinel(LessonType.None);
        builder.Property(e => e.MaxStudents).IsRequired().HasDefaultValue(1);
        builder.Property(e => e.LessonStatus).IsRequired().HasDefaultValue(LessonStatus.Scheduled).HasSentinel(LessonStatus.Scheduled);
        builder.Property(e => e.TrainerId).IsRequired().HasDefaultValue(Guid.Empty);

        builder.HasData(
            new Lesson("Lesson 1", "Description 1"),
            new Lesson("Lesson 2", "Description 2")
        );
    }
}