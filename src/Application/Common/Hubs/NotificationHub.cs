using Application.Shared.Common.EventHandlers;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.Application.Common.Hubs;

public class NotificationHub : Hub
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHub(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotification(SerializedNotification notification)
    {
        await _hubContext.Clients.All.SendAsync("Notification", notification);
    }
}