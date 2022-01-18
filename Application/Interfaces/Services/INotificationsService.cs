using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Auth;

namespace Application.Interfaces.Services;

public interface INotificationsService
{
    Task<Guid> AddNotificationAsync(string userEmail, CancellationToken token);

    Task DeleteNotificationsAsync(IEnumerable<Notification> notifications, CancellationToken token);

    Task<IEnumerable<Notification>> GetNotificationsAsync(string userEmail, CancellationToken token);

}