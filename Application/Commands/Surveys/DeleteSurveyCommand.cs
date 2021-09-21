using MediatR;
using System;

namespace Application.Commands.Surveys
{
    public record DeleteSurveyCommand(Guid Id) : IRequest<bool>;
}
