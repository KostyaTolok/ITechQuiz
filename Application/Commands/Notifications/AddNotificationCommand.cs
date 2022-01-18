using System;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Commands.Notifications;

public record AddNotificationCommand(Notification Notification) : IRequest<Guid>;