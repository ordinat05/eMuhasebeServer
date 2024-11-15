using Microsoft.AspNetCore.SignalR;

namespace eMuhasebeServer.WebAPI.SignalRHub.ConnectionHub;

public class ChatHub : Hub
{

    //private readonly ILocationService _locationService;

    //public ChatHub(ILocationService locationService)
    //{
    //    _locationService = locationService;
    //}

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    public class NotificationModels
    {
        public int Id { get; set; }

        public EntityNameEnum type { get; set; }

        public string? UserId { get; set; }
    }

    public enum EntityNameEnum
    {
        Message = 1,
        UploadFile = 2,
        DownloadFile = 3
    }
}
