using LessonService.Application.Models.Lesson;
using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class RescheduleEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to update data of the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost("/lessons/reschedule/{id:guid}",
                async (Guid id, RescheduleRequest updateRequest, ILessonServiceApp lessonServiceApp) =>
                {
                    var result = await lessonServiceApp.RescheduleAsync(id, updateRequest);
                    return result is not null ? Results.Created($"{HelperEndpoint.baseUrl}/{result.Id}", result) : Results.NotFound();
                }), "Reschedule the lesson", "Endpoint to reschedule the lesson")
            .WithName("UpdateLesson");
    }
}