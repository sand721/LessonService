namespace LessonService.Application.Models.Lesson;

public record TrainerRequest(Guid LessonId, Guid TrainerId);