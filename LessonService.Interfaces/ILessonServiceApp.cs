using LessonService.Application.Models.Lesson;

namespace LessonService.Interfaces;

public interface ILessonServiceApp
{
    Task<IEnumerable<LessonResponse>> GetAllLessonsAsync();
    Task<LessonResponse?> GetLessonByIdAsync(Guid id);
    Task<LessonResponse?> CreateLessonAsync(CreateLessonRequest lessonInfo);
    Task<LessonResponse?> UpdateLessonAsync(Guid id, UpdateLessonRequest lessonInfo);
    Task<bool> DeleteLessonAsync(Guid id);
    Task<LessonResponse?> AssignTrainerAsync(Guid lessonId, Guid trainerId);
    Task<bool> RemoveTrainerAsync(Guid lessonId);
    Task<bool> AddStudentAsync(Guid lessonId, Guid studentId);
    Task<bool> RemoveStudentAsync(Guid lessonId, Guid studentId);
    Task<List<Guid>> GetAllStudentsOfLessonAsync(Guid lessonId);
    Task<LessonResponse?> RescheduleAsync(Guid id, RescheduleRequest updateRequest);
}