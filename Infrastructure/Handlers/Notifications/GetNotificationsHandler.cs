using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Notifications;
using Domain.Entities.Auth;
using MediatR;

namespace Infrastructure.Handlers.Notifications;

public class GetNotificationsHandler: IRequestHandler<GetNotificationsQuery, IEnumerable<Notification>>
{
    private readonly INotificationsRepository repository;

    public GetNotificationsHandler(INotificationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Notification>> Handle(GetNotificationsQuery request, CancellationToken token)
    {
        return await repository.GetNotificationsAsync(request.UserEmail, token);
    }
}