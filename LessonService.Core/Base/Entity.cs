namespace LessonService.Core.Base
{
    public abstract class Entity<TId>(TId id)
    {
        public TId Id { get; set; } = id;

    }
}
