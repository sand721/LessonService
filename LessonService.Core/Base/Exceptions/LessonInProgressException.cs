namespace LessonService.Core.Base.Exceptions;

public class LessonInProgressException(Lesson lesson) : Exception($"Lesson {lesson.Id} is in progress");