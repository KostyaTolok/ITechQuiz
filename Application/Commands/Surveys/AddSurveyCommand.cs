using Domain.Entities.Surveys;
using MediatR;
using System;

namespace Application.Commands.Surveys
{
    public record AddSurveyCommand(Survey Survey) : IRequest<Guid>;
}
