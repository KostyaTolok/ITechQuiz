using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Notifications;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Notifications;

public class DeleteNotificationsHandler: IRequestHandler<DeleteNotificationsCommand, Unit>
{
    private readonly INotificationsRepository repository;

    public DeleteNotificationsHandler(INotificationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Unit> Handle(DeleteNotificationsCommand command, CancellationToken token)
    {
        await repository.DeleteNotificationsAsync(command.Notifications, token);
        return Unit.Value;
    }
}