namespace LessonService.Core.Base
{
    public class LessonGroup
    {
        public Guid LessonId { get; init; }
        public Guid StudentId { get; init; }
        public Lesson Lesson { get; init; } = new();
        // Parameterless constructor required by EF Core
    }
}
