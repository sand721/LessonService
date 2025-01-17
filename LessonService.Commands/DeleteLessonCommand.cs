using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using MediatR;

namespace LessonService.Commands;

public record DeleteLessonCommand(Guid LessonId) : IRequest<ApiResponse<bool>>;