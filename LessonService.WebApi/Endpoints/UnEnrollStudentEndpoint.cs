using LessonService.Application.Models.Lesson;
using LessonService.Interfaces;

namespace LessonService.WebApi.Endpoints;

public static class UnEnrollStudentEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        // Endpoint to un-enroll a student from the lesson
        HelperEndpoint.ConfigureEndpoint(app.MapPut($"{HelperEndpoint.baseUrl}/student",
                async (StudentRequest request, ILessonServiceApp lessonServiceApp) =>
                {
                    var result = await lessonServiceApp.RemoveStudentAsync(request.LessonId, request.StudentId);
                    return result ? Results.NoContent() : Results.NotFound();
                }), "Un-enroll a student from the lesson", "Endpoint to un-enroll a student from the lesson")
            .WithName("UnEnrollStudent");
    }
}