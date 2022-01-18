using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication.Hubs;

public class NotificationHub : Hub
{
    private static ConcurrentDictionary<string, string> connections = new ();

    private readonly IUsersService usersService;
    private readonly INotificationsService notificationsService;

    public NotificationHub(IUsersService usersService, INotificationsService notificationsService)
    {
        this.usersService = usersService;
        this.notificationsService = notificationsService;
    }

    [AllowAnonymous]
    public async Task Notify(Guid? surveyId)
    {
        if (surveyId.HasValue)
        {
            var userEmail = await usersService.GetUserEmailBySurveyId(surveyId.Value, CancellationToken.None);
            if (connections.ContainsKey(userEmail))
            {
                await Clients.Client(connections[userEmail]).SendAsync("Receive", true);
            }
            else
            {
                await notificationsService.AddNotificationAsync(userEmail, CancellationToken.None);
            }
        }
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override async Task OnConnectedAsync()
    {
        var userEmail = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userEmail == null)
            return;
        
        if (connections.ContainsKey(userEmail))
            connections.TryUpdate(userEmail, Context.ConnectionId, connections[userEmail]);
        else
            connections.TryAdd(userEmail, Context.ConnectionId);

        var notifications = await notificationsService.GetNotificationsAsync(userEmail, CancellationToken.None);
        var notificationList = notifications.ToList();
        if (notificationList.Any())
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Receive", true);
            await notificationsService.DeleteNotificationsAsync(notificationList, CancellationToken.None);
        }
        
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userEmail = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userEmail != null)
        {
            connections.TryRemove(userEmail, out _);
        }
        return base.OnDisconnectedAsync(exception);
    }

}