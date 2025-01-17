﻿using LessonService.Application.Models.Lesson;
using LessonService.Application.Models.System;
using MediatR;

namespace LessonService.Commands;

public record AssignTrainerCommand(Guid LessonId, Guid TrainerId):  IRequest<ApiResponse<LessonResponse>>;