using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Auth;

namespace Application.Interfaces.Repositories;

public interface INotificationsRepository
{
    Task AddNotificationAsync(Notification notification, CancellationToken token);
        
    Task DeleteNotificationsAsync(IEnumerable<Notification> notifications, CancellationToken token);

    Task<IEnumerable<Notification>> GetNotificationsAsync(string userEmail, CancellationToken token);
}