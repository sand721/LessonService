using LessonService.Application.Models.Lesson;
using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class EnrollStudentEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to enroll a student to the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPost($"{HelperEndpoint.baseUrl}/student",
                async (StudentRequest request, ILessonServiceApp lessonServiceApp) =>
                {
                    var result = await lessonServiceApp.AddStudentAsync(request.LessonId, request.StudentId);
                    return result ? Results.NoContent() : Results.NotFound();
                }), "Enroll a student to the lesson", "Endpoint to enroll a student to the lesson")
            .WithName("EnrollStudent");
        
    }
}