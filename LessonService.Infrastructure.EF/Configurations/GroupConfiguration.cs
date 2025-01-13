using LessonService.Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonService.Infrastructure.EF.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<LessonGroup>
{
    public void Configure(EntityTypeBuilder<LessonGroup> builder)
    {
        builder.ToTable("LessonGroups");
        builder.HasKey(e => new { e.StudentId, e.LessonId });
        builder.HasOne(e => e.Lesson)
            .WithMany(l => l.LessonGroups)
            .HasForeignKey(e => e.LessonId);
        builder.Property(e => e.StudentId).IsRequired();
        builder.Property(e => e.LessonId).IsRequired();
    }
}