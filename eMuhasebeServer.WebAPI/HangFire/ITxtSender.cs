namespace eMuhasebeServer.WebAPI.HangFire
{
    public interface ITxtSender
    {
        Task Sender(string msg);
    }
}
