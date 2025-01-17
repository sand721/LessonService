using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using LessonService.Core.Base;
using MediatR;

namespace LessonService.Commands;

public record CreateLessonCommand(
    string Name,
    string Description,
    DateTime DateFrom,
    int Duration,
    TrainingLevel TrainingLevel, 
    LessonType LessonType, int MaxStudents, Guid TrainerId) : IRequest<ApiResponse<LessonResponse>>;
