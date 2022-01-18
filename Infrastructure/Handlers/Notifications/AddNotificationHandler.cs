using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Notifications;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Notifications;

public class AddNotificationHandler: IRequestHandler<AddNotificationCommand, Guid>
{
    private readonly INotificationsRepository repository;

    public AddNotificationHandler(INotificationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Guid> Handle(AddNotificationCommand command, CancellationToken token)
    {
        var notification = command.Notification;
        await repository.AddNotificationAsync(notification, token);
        return notification.Id;
    }
}