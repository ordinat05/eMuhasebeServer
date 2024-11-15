using eMuhasebeServer.Domain.Entities;
using eMuhasebeServer.Domain.MailKitMimeKit.Helper;
using eMuhasebeServer.Domain.MailKitMimeKit.Service;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace eMuhasebeServer.Domain.Events
{
    public sealed class SendConfirmEmailEvent2 : INotificationHandler<AppUserEvent>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public SendConfirmEmailEvent2(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task Handle(AppUserEvent notification, CancellationToken cancellationToken)
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

                var confirmationLink = $"https://www.tammerkez.com/confirm-email/{HttpUtility.UrlEncode(appUser.Email)}";
                // http://localhost:4202   Yerel sunucuda test yapıyorsan
                // https://www.tammerkez.com Proje canlıya alınmışsa, Canlı Frontend Proje adresi olacak.
                // http://localhost:4202/confirm-email/sebkucuk05@gmail.com
              

                var htmlContent = new HtmlMail1Content
                {
                    Aciklama1H1 = "E-posta Adresinizi Onaylayın",
                    ImageUrlSirketLogo = "https://www.tammerkez.com/assets2/dist/img/tammerkezlogo33.png",
                    YaziyaTiklayincaGitLinkUrl = "https://www.tammerkez.com/bulten",
                    Aciklama2H1 = "iletisim@tammerkez.com",
                    Aciklama3H1 = "Mail Adresinizi Onaylamak İçin Aşağıdaki Butona Tıklayınız",
                    Aciklama4Btn1 = "Mail Adresimi Onayla",
                    LinkUrlBtn1 = confirmationLink,
                    DataBtn1 = "" // Bu alanı boş bırakıyoruz çünkü link zaten tam olarak oluşturuldu
                };

                var mailRequest = new MailRequest
                {
                    ToEmails = appUser.Email != null ? new List<string> { appUser.Email } : new List<string>(),
                    ToCCs = new List<string>(),
                    ToBCCs = new List<string>(),
                    Subject = "E-posta Onayı",
                    Body = _emailService.GetHtmlContent(htmlContent)
                };

                var result = await _emailService.SendEmailAsync(mailRequest, emailSettings);

                if (result.FailedEmails.Any())
                {
                    Console.WriteLine($"E-posta gönderimi başarısız: {string.Join(", ", result.FailedEmails)}");
                }
            }
        }
    }
}