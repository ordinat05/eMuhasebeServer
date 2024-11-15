using MediatR;

namespace eMuhasebeServer.Domain.Events;

public sealed class ForgotPasswordEvent : INotification
{
    public Guid UserId { get; }
    public string Token { get; }

    public ForgotPasswordEvent(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
    }
}




