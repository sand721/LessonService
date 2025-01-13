using LessonService.Application.Models.Lesson;
using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class CreateLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to create lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost(HelperEndpoint.baseUrl,
                async (CreateLessonRequest createLessonRequest, ILessonServiceApp lessonServiceApp) =>
                {
                    var result = await lessonServiceApp.CreateLessonAsync(createLessonRequest);
                    return result is not null ? Results.Created($"{HelperEndpoint.baseUrl}/{result.Id}", result) : Results.NotFound();
                }), "Create a lesson", "Endpoint to create lesson")
            .WithName("CreateLesson");
    }    
}