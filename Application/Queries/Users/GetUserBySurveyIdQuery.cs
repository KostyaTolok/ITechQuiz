using System;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.Users;

public record GetUserBySurveyIdQuery(Guid? SurveyId) : IRequest<User>;