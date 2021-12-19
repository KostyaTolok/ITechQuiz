using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

public class NotificationHub : Hub
{
    public async Task Notify(string clientEmail)
    {
        await Clients.User(clientEmail).SendAsync("Receive");
    }
}