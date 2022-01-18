using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class EFNotificationsRepository : INotificationsRepository
{
    private readonly QuizDbContext context;

    public EFNotificationsRepository(QuizDbContext context)
    {
        this.context = context;
    }

    public async Task AddNotificationAsync(Notification notification, CancellationToken token)
    {
        context.Entry(notification).State = EntityState.Added;

        await context.SaveChangesAsync(token);
    }

    public async Task DeleteNotificationsAsync(IEnumerable<Notification> notifications, CancellationToken token)
    {
        context.Notifications.RemoveRange(notifications);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAsync(string userEmail, CancellationToken token)
    {
        return await context.Notifications
            .Where(n => n.UserEmail == userEmail)
            .ToListAsync(token);
    }
}