using eMuhasebeServer.Domain.MailKitMimeKit.Helper;

namespace eMuhasebeServer.Domain.MailKitMimeKit.Service
{
    public interface IEmailService
    {
        Task<EmailResult> SendEmailAsync(MailRequest mailRequest, EmailSettings emailSettings);
        string GetHtmlContent(HtmlMail1Content content);

    }
}
