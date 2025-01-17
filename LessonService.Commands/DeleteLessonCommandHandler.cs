using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;

public class DeleteLessonCommandHandler(
    AppDbContext context,
    ILogger<AssignTrainerCommandHandler> logger) : IRequestHandler<DeleteLessonCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<bool>
        {
            Data = false
        };
        try
        {
            var lesson = await context.Lessons.FindAsync(command.LessonId);
            if (lesson == null)
            {
                response.Message = $"Lesson with ID: {command.LessonId} not found."; 
            }
            else
            {
                context.Lessons.Remove(lesson);
                await context.SaveChangesAsync(cancellationToken);
                response.Message = "Lesson deleted successfully.";
                response.Data = true;
            }

            return response;
        }
        catch (Exception e)
        {
            logger.LogError($"Error deleting books: {e.Message}");
            throw;
        }
    }
}