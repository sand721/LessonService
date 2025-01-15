using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Core.Base;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class CreateLessonCommandHandler(AppDbContext context, ILogger<CreateLessonCommandHandler> logger, IMapper mapper) : IRequestHandler<CreateLessonCommand, LessonResponse>
{
    public async Task<LessonResponse> Handle(CreateLessonCommand lessonInfo, CancellationToken cancellationToken)
    {
        try
        {
            var lesson = new Lesson(
                Guid.Empty,
                lessonInfo.Name,
                lessonInfo.Description,
                DateTime.SpecifyKind(lessonInfo.DateFrom, DateTimeKind.Utc),
                lessonInfo.Duration,
                lessonInfo.TrainingLevel,
                lessonInfo.LessonType,
                lessonInfo.MaxStudents,
                lessonInfo.TrainerId
            );
            context.Lessons.Add(lesson);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Lesson created successfully.");
            return mapper.Map<LessonResponse>(lesson);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error creating lesson: {ex.Message}");
            throw;
        }
    }
}