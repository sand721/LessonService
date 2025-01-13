using LessonService.Application.Models.Lesson;
using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class AssignTrainerEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to assign a trainer to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/trainer",
                async (TrainerRequest request, ILessonServiceApp lessonServiceApp) =>
                {
                    var result = await lessonServiceApp.AssignTrainerAsync(request.LessonId, request.TrainerId);
                    return result is not null ? Results.Created($"{HelperEndpoint.baseUrl}/{result.Id}", result) : Results.NotFound();
                }), "Assign a trainer to the lesson", "Endpoint to assign a trainer to the lesson")
            .WithName("AssignTrainer");
    }
}