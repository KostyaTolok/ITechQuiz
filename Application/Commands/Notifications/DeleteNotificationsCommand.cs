using System.Collections.Generic;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Commands.Notifications;

public record DeleteNotificationsCommand(IEnumerable<Notification> Notifications) : IRequest<Unit>;