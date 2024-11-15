using eMuhasebeServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace eMuhasebeServer.Application.Features.Users.ResetPasswordUsername;

public class ResetPasswordUsernameCommandHandler : IRequestHandler<ResetPasswordUsernameCommand, Result<string>>
{
    private readonly UserManager<AppUser> _userManager;

    public ResetPasswordUsernameCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<Result<string>> Handle(ResetPasswordUsernameCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!); 
        if (user == null)
        {
            return "Kullanıcı bulunamadı.";
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Token!, request.Password!); 
        if (result.Succeeded)
        {
            return "Şifre başarıyla değiştirildi.";
        }
        else
        {
            if (result.Errors.Any(e => e.Code == "InvalidToken"))
            {
                return "İşlem süresi zaman aşımına uğradı. Lütfen şifre sıfırlama işlemini tekrar başlatın.";
            }

            return string.Join(", ", result.Errors.Select(e => e.Description));
        }
    }
}