namespace LessonService.Application.Models.Lesson;

public record UpdateLessonRequest
(
    Guid Id,
    string? Name,
    string? Description,
    int? MaxStudents,
    int? LessonType,
    int? TrainingLevel
);