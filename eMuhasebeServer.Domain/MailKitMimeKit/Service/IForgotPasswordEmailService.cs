using eMuhasebeServer.Domain.MailKitMimeKit.Helper;

namespace eMuhasebeServer.Domain.MailKitMimeKit.Service;

public interface IForgotPasswordEmailService
{
    Task<EmailResult> SendEmailAsync(MailRequest mailRequest, EmailSettings emailSettings);
    string GetHtmlContent(HtmlMail1Content content);
}




