using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Core.Base;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LessonService.Application.Services;

public class LessonServiceApp(AppDbContext context, ILogger<LessonServiceApp> logger, IMapper mapper)
    : ILessonServiceApp
{
    public async Task<IEnumerable<LessonResponse>> GetAllLessonsAsync()
    {
        try
        {
            var lessons = await context.Lessons.ToListAsync();

            return lessons.Select(lesson => new LessonResponse(lesson.Id, lesson.Name, lesson.Description,
                lesson.DateFrom, lesson.Duration, lesson.MaxStudents, lesson.LessonType, lesson.TrainingLevel,
                lesson.LessonStatus, lesson.TrainerId));
            //mapper.Map<IEnumerable<LessonResponse>>(lessons);
        }
        catch (Exception e)
        {
            logger.LogError($"Error getting list of lessons: {e.Message}");
            throw;
        }
    }
    
    public async Task<LessonResponse?> RescheduleAsync(Guid lessonId, RescheduleRequest updateRequest)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            if (lesson == null)
                return null;        
            lesson.Reschedule(updateRequest.date, updateRequest.duration);
            await context.SaveChangesAsync();
            logger.LogInformation("Lesson rescheduled successfully.");
            return mapper.Map<LessonResponse>(lesson);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error rescheduling the lesson: {ex.Message}");
            throw;
        }
    }

    public async Task<LessonResponse?> GetLessonByIdAsync(Guid lessonId)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            if (lesson == null)
                return null;
            return mapper.Map<LessonResponse>(lesson);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }
    }

    public async Task<LessonResponse?> CreateLessonAsync(CreateLessonRequest lessonInfo)
    {
        try
        {
            var lesson = new Lesson(
                Guid.Empty,
                lessonInfo.Name,
                lessonInfo.Description,
                DateTime.SpecifyKind(lessonInfo.DateFrom, DateTimeKind.Utc),
                lessonInfo.Duration,
                (TrainingLevel)lessonInfo.TrainingLevel,
                (LessonType)lessonInfo.LessonType,
                lessonInfo.MaxStudents,
                lessonInfo.TrainerId
            );
            context.Lessons.Add(lesson);
            await context.SaveChangesAsync();
            logger.LogInformation("Lesson created successfully.");
            return mapper.Map<LessonResponse>(lesson);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error creating lesson: {ex.Message}");
            throw;
        }
    }

    public async Task<LessonResponse?> UpdateLessonAsync(Guid id, UpdateLessonRequest lessonInfo)
    {
        try
        {
            var lesson = await FindLesson(id);
            if (lesson == null)
                return null;
            if (lesson.LessonStatus != LessonStatus.Scheduled)
                return null;
            if (lessonInfo.Name is not null) lesson.SetName(lessonInfo.Name);
            if (lessonInfo.Description is not null) lesson.SetDescription(lessonInfo.Description);
            if (lessonInfo.MaxStudents.HasValue)
            {
                if (lessonInfo.MaxStudents.Value < lesson.LessonGroups.Count)
                    return null;
                lesson.SetMaxStudents(lessonInfo.MaxStudents.Value);
            }
            if (lessonInfo.LessonType.HasValue) lesson.SetLessonType((LessonType)lessonInfo.LessonType);
            if (lessonInfo.TrainingLevel.HasValue) lesson.SetTrainingLevel((TrainingLevel)lessonInfo.TrainingLevel);

            await context.SaveChangesAsync();
            logger.LogInformation("Lesson updated successfully.");
            return mapper.Map<LessonResponse>(lesson);
        }
        catch (Exception e)
        {
            logger.LogError($"Error updating lesson: {e.Message}");
            throw;
        }
    }

public async Task<LessonResponse?> UpdateLessonAsync(Guid lessonId, CreateLessonRequest lessonInfo)
    {
        var lesson = await FindLesson(lessonId);
        if (lesson == null)
            return null;
        return mapper.Map<LessonResponse>(lesson);    
    }

    public async Task<bool> DeleteLessonAsync(Guid lessonId)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            if (lesson == null)
                return false;
            context.Lessons.Remove(lesson);
            await context.SaveChangesAsync();
            logger.LogInformation("Lesson deleted successfully.");
            return true;
        }
        catch (Exception e)
        {
            logger.LogError($"Error deleting books: {e.Message}");
            throw;
        }
    }
    public async Task<LessonResponse?> AssignTrainerAsync(Guid lessonId, Guid trainerId)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            if (lesson == null)
                return null;
            lesson.AssignTrainer(trainerId);
            await context.SaveChangesAsync();
            logger.LogInformation("Trainer assigned successfully.");
            return mapper.Map<LessonResponse>(lesson);    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> RemoveTrainerAsync(Guid lessonId)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            if (lesson == null)
                return false;
            lesson.RemoveTrainer();
            await context.SaveChangesAsync();
            logger.LogInformation("Trainer assigned successfully.");
            return true;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> AddStudentAsync(Guid lessonId, Guid studentId)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            if (lesson == null)
                return false;
            var group = new LessonGroup() { Lesson = lesson, StudentId = studentId, LessonId = lessonId };
            context.LessonGroups.Add(group);
            await context.SaveChangesAsync();
            logger.LogInformation("Student added successfully.");
            return true;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error adding student: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> RemoveStudentAsync(Guid lessonId, Guid studentId)
    {
        try
        {
            var group = await FindGroup(lessonId, studentId);
            if (group == null)
                return false;
            context.LessonGroups.Remove(group);
            await context.SaveChangesAsync();
            logger.LogInformation("Student removed successfully.");
            return true;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error adding student: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Guid>> GetAllStudentsOfLessonAsync(Guid lessonId)
    {
        try
        {
            var lesson = await FindLesson(lessonId);
            return await context.LessonGroups.Where(lg=>lg.LessonId==lessonId).Select(p=>p.StudentId).ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError($"Error adding student: {ex.Message}");
            throw;
        }    
    }

    private async Task<Lesson?> FindLesson(Guid lessonId)
    {
        var lesson = await context.Lessons.FindAsync(lessonId);
        if (lesson == null)
        {
            logger.LogError($"Lesson with ID: {lessonId} not found.");
        }
        return lesson;
    }
    private async Task<LessonGroup?> FindGroup(Guid lessonId, Guid studentId)
    {
        var group = await context.LessonGroups.FirstOrDefaultAsync(x => x.LessonId == lessonId && x.StudentId == studentId);
        if (group == null)
        {
            logger.LogError($"Student with ID: {studentId} not found.");
        }
        return group;
    }
    
    
}