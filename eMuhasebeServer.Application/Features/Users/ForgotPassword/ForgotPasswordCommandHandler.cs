using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;
    private static readonly Dictionary<string, DateTime> _lastRequestTimes = new Dictionary<string, DateTime>();

    public ForgotPasswordCommandHandler(UserManager<AppUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }
    //"Mail adresi bulunamadı"
    //"Şifre değişikliği maili gönderilmiştir." 
    //"Yeni istek yapabilmeniz için ... süre beklemelisiniz." 

    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return "Mail adresi bulunamadı";
        }

        //if (_lastRequestTimes.TryGetValue(request.Email, out DateTime lastRequestTime))
        //{
        //    var timeSinceLastRequest = DateTime.UtcNow - lastRequestTime;
        //    var waitTime = TimeSpan.FromMinutes(Math.Pow(2, _lastRequestTimes.Count(kvp => kvp.Key == request.Email)));

        //    if (timeSinceLastRequest < waitTime)
        //    {
        //        var remainingTime = waitTime - timeSinceLastRequest;
        //        return $"Yeni istek yapabilmeniz için {remainingTime.TotalMinutes:F0} dakika beklemelisiniz.";
        //    }
        //}

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Burada e-posta gönderme işlemi yapılacak
        // Örnek: await _emailSender.SendEmailAsync(user.Email, "Şifre Sıfırlama", $"Şifrenizi sıfırlamak için bu linke tıklayın: <resetlink>?token={token}");

        // ForgotPasswordEvent'i yayınla
        await _mediator.Publish(new ForgotPasswordEvent(user.Id, token), cancellationToken);
        // TODO şifre değişikliği Mailindeki Token ın işlem süresi sadece 1 gündür. 1 günden sonra Token geçersizdir.
        //await _userManager.Options.Tokens.PasswordResetTokenLifespan = TimeSpan.FromMinutes(60);

        _lastRequestTimes[request.Email] = DateTime.UtcNow;

        return "şifre değişikliği maili gönderilmiştir.";
    }
}