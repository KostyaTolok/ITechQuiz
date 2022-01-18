using System.Collections.Generic;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.Notifications;

public record GetNotificationsQuery(string UserEmail) : IRequest<IEnumerable<Notification>>;