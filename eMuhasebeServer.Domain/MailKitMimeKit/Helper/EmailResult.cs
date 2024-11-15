namespace eMuhasebeServer.Domain.MailKitMimeKit.Helper
{
    public class EmailResult
    {
        public List<string> SuccessfulEmails { get; set; } = new List<string>();
        public List<string> FailedEmails { get; set; } = new List<string>();
    }
}
