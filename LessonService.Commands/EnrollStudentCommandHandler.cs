using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Core.Base;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class EnrollStudentCommandHandler(
    AppDbContext context,
    ILogger<AssignTrainerCommandHandler> logger) : IRequestHandler<EnrollStudentCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(EnrollStudentCommand command,
        CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>();
        response.Data = false;
        try
        {
            var lesson = await context.Lessons.FindAsync(command.LessonId);
            if (lesson == null)
            {
                response.Message = $"Lesson with ID: {command.LessonId} not found."; 
            }
            else
            {
                var group = new LessonGroup() { Lesson = lesson, StudentId = command.StudentId, LessonId = command.LessonId };
                context.LessonGroups.Add(group);
                await context.SaveChangesAsync(cancellationToken);
                response.Message = "Student added successfully.";
                logger.LogInformation("Student added successfully.");
                response.Data = true;
            }
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error adding student: {ex.Message}");
            throw;
        }
        
    }
}