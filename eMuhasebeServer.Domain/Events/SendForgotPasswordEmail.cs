using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.MailKitMimeKit.Helper;
using eMuhasebeServer.Domain.MailKitMimeKit.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace eMuhasebeServer.Domain.Events
{
    public sealed class SendForgotPasswordEmail : INotificationHandler<ForgotPasswordEvent>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IForgotPasswordEmailService _forgotPasswordEmailService;

        public SendForgotPasswordEmail(UserManager<AppUser> userManager, IForgotPasswordEmailService forgotPasswordEmailService)
        {
            _userManager = userManager;
            _forgotPasswordEmailService = forgotPasswordEmailService;
        }

        public async Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
        {
            AppUser? appUser = await _userManager.FindByIdAsync(notification.UserId.ToString());
            if (appUser is not null)
            {
                var emailSettings = new EmailSettings
                {
                    Email = "sebkucuk05@gmail.com",
                    Password = "ueglhwihzsmurtne",
                    Host = "smtp.gmail.com",
                    DisplayName = "Göndericinin Görünen ismi tammerkez.com",
                    Port = 587
                };

                // var confirmationLink = $"http://localhost:4202/change-password/{HttpUtility.UrlEncode(appUser.Email)}";
                var confirmationLink = $"http://localhost:4202/change-password?email={HttpUtility.UrlEncode(appUser.Email)}&token={HttpUtility.UrlEncode(notification.Token)}";
                // http://localhost:4202   Yerel sunucuda test yapıyorsan
                // https://www.tammerkez.com Proje canlıya alınmışsa, Canlı Frontend Proje adresi olacak.
                // http://localhost:4202/change-password/sebkucuk05@gmail.com
                // 

                var htmlContent = new HtmlMail1Content
                {
                    Aciklama1H1 = "Şifre Değişikliği",
                    ImageUrlSirketLogo = "https://www.tammerkez.com/assets2/dist/img/tammerkezlogo33.png",
                    YaziyaTiklayincaGitLinkUrl = "https://www.tammerkez.com/bulten",
                    Aciklama2H1 = "iletisim@tammerkez.com",
                    Aciklama3H1 = "Şifrenizi Değiştirmek İçin Aşağıdaki Butona Tıklayınız",
                    Aciklama4Btn1 = "Şifremi Değiştirmek İstiyorum",
                    LinkUrlBtn1 = confirmationLink,
                    DataBtn1 = "" 
                };

                var mailRequest = new MailRequest
                {
                    ToEmails = appUser.Email != null ? new List<string> { appUser.Email } : new List<string>(),
                    ToCCs = new List<string>(),
                    ToBCCs = new List<string>(),
                    Subject = "Şifre Değişikliği",
                    Body = _forgotPasswordEmailService.GetHtmlContent(htmlContent)
                };

                var result = await _forgotPasswordEmailService.SendEmailAsync(mailRequest, emailSettings);

                if (result.FailedEmails.Any())
                {
                    Console.WriteLine($"E-posta gönderimi başarısız: {string.Join(", ", result.FailedEmails)}");
                }
            }
        }
    }
}