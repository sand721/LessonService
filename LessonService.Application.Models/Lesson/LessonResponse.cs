using LessonService.Core.Base;

namespace LessonService.Application.Models.Lesson;

public record LessonResponse
(
    Guid Id,
    string Name,
    string Description,
    DateTime DateFrom,
    int Duration,
    int MaxStudents,
    LessonType LessonType,
    TrainingLevel TrainingLevel,
    LessonStatus LessonStatus,
    Guid TrainerId
);