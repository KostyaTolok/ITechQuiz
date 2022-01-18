using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Notifications;
using Application.Interfaces.Services;
using Application.Queries.Notifications;
using Domain.Entities.Auth;
using Domain.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class NotificationsService : INotificationsService
{
    private readonly ILogger<INotificationsService> logger;
    private readonly IMediator mediator;

    public NotificationsService(IMediator mediator, ILoggerFactory factory)
    {
        this.mediator = mediator;
        logger = factory.CreateLogger<INotificationsService>();
    }

    public async Task<Guid> AddNotificationAsync(string userEmail, CancellationToken token)
    {
        if (string.IsNullOrEmpty(userEmail))
        {
            logger.LogError("{ExString}",
                AnswerServiceStrings.AddAnswerQuestionIdException);
            throw new ArgumentException(AnswerServiceStrings.AddAnswerQuestionIdException);
        }

        return await mediator.Send(new AddNotificationCommand(new Notification {UserEmail = userEmail}), token);
    }
    
    public async Task DeleteNotificationsAsync(IEnumerable<Notification> notifications, CancellationToken token)
    {
        await mediator.Send(new DeleteNotificationsCommand(notifications), token);
    }
    
    public async Task<IEnumerable<Notification>> GetNotificationsAsync(string userEmail, CancellationToken token)
    {
        if (string.IsNullOrEmpty(userEmail))
        {
            logger.LogError("{ExString}",
                AnswerServiceStrings.AddAnswerQuestionIdException);
            throw new ArgumentException(AnswerServiceStrings.AddAnswerQuestionIdException);
        }

        return await mediator.Send(new GetNotificationsQuery(userEmail), token);
    }
}