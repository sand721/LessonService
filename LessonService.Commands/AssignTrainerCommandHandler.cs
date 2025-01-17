using AutoMapper;
using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Core.Base;
using LessonService.Infrastructure.EF;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LessonService.Commands;


public class AssignTrainerCommandHandler(
    AppDbContext context,
    ILogger<AssignTrainerCommandHandler> logger,
    IMapper mapper) : IRequestHandler<AssignTrainerCommand, ApiResponse<LessonResponse>>
{
    public async Task<ApiResponse<LessonResponse>> Handle(AssignTrainerCommand command, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<LessonResponse>();
        try
        {
            var lesson = await context.Lessons.FindAsync(command.LessonId);
            if (lesson == null)
            {
                response.Message = $"Lesson with ID: {command.LessonId} not found."; 
            }
            else
            {
                lesson.AssignTrainer(command.TrainerId);
                await context.SaveChangesAsync(cancellationToken);
                response.Message = "Trainer assigned successfully.";
                response.Data = mapper.Map<LessonResponse>(lesson);
            }            
            logger.LogError(response.Message);
            return response;    
        }
        catch (Exception ex)
        {
            logger.LogError($"Error getting lesson: {ex.Message}");
            throw;
        }
    }
}