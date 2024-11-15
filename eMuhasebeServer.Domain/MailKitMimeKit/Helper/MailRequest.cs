namespace eMuhasebeServer.Domain.MailKitMimeKit.Helper
{
    public class MailRequest
    {
        //public string ToEmail { get; set; }
        public List<string> ToEmails { get; set; } = new List<string>();
        public List<string> ToCCs { get; set; } = new List<string>();
        //Mail Alıcıları birbir adreslerini görebilirler
        public List<string> ToBCCs { get; set; } = new List<string>();
        //Mail Alıcıları birbir adreslerini göremezler
        public string? Subject { get; set; }
        public string? Body { get; set; }

        public string? AttachmentPath { get; set; } 
        public string? AttachmentName { get; set; }
    }
}
