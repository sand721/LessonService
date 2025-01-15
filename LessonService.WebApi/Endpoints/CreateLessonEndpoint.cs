using LessonService.Commands;
using MediatR;
 
namespace LessonService.WebApi.Endpoints;

public static class CreateLessonEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        HelperEndpoint.ConfigureEndpoint(app.MapPost(HelperEndpoint.baseUrl,
                async (CreateLessonCommand command, IMediator mediator) =>
                {
                    var result = await mediator.Send(command);
                    return Results.Created($"{HelperEndpoint.baseUrl}/{result.Id}", result);
                }), "Create a lesson", "Endpoint to create lesson")
            .WithName("CreateLesson");
    }    
}