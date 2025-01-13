namespace LessonService.Core.Base.Exceptions;

public class LessonCancelledException(Lesson lesson) : Exception($"Lesson {lesson.Id} is cancelled");
