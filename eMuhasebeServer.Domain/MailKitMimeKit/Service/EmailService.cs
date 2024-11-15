using eMuhasebeServer.Domain.MailKitMimeKit.Helper;
using MailKit.Security;
using MimeKit;

namespace eMuhasebeServer.Domain.MailKitMimeKit.Service
{
    public class EmailService : IEmailService
    {
        public async Task<EmailResult> SendEmailAsync(MailRequest mailRequest, EmailSettings emailSettings)
        {
            var result = new EmailResult();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSettings.Email);

            foreach (var toEmail in mailRequest.ToEmails)
            {
                email.To.Add(MailboxAddress.Parse(toEmail));
            }

            foreach (var toCC in mailRequest.ToCCs)
            {
                email.Cc.Add(MailboxAddress.Parse(toCC));
            }

            foreach (var toBCC in mailRequest.ToBCCs)
            {
                email.Bcc.Add(MailboxAddress.Parse(toBCC));
            }

            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();

            //string attachmentPath = @"C:\Users\Lenovo\Documents\solutions\eMuhasebe\ders13\eMuhasebeServerMultiMail-Zipper-Timer\eMuhasebeServer.Domain\Attachment\dummy.pdf";

            //string ekAdi = "maildekiEk.pdf";

           //byte[] fileBytes;
           // if (System.IO.File.Exists(mailRequest.AttachmentPath))
           // {
           //     FileStream file = new FileStream(mailRequest.AttachmentPath, FileMode.Open, FileAccess.Read);
           //     using (var ms = new MemoryStream())
           //     {
           //         file.CopyTo(ms);
           //         fileBytes = ms.ToArray();
           //     }
           //     builder.Attachments.Add(mailRequest.AttachmentName, fileBytes, ContentType.Parse("application/octet-stream"));
           // }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            try
            {
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailSettings.Email, emailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                result.SuccessfulEmails.AddRange(mailRequest.ToEmails);
                result.SuccessfulEmails.AddRange(mailRequest.ToCCs);
                result.SuccessfulEmails.AddRange(mailRequest.ToBCCs);
            }
            catch (Exception)
            {
                result.FailedEmails.AddRange(mailRequest.ToEmails);
                result.FailedEmails.AddRange(mailRequest.ToCCs);
                result.FailedEmails.AddRange(mailRequest.ToBCCs);
            }

            return result;
        }

        public string GetHtmlContent(HtmlMail1Content content)
        {
            return $@"
            <div  class=""headergroup"" style=""float:left;width:100%;background-image:linear-gradient(154deg,#ffe18a,#f5b716);text-align:center;border-radius:6px 6px;"">
                <h1 class=""headerh1"" style=""margin-block-end: 2px;"">{content.Aciklama1H1}</h1>
                <img class=""companylogo""  src=""{content.ImageUrlSirketLogo}"" />
                <h2 class=""headerh2"" style=""margin-block-start: 2px;"">{content.Aciklama3H1}</h2>
        
                    <a href=""{content.LinkUrlBtn1}{content.DataBtn1}"" class=""ahrefsitelink"" target=""""_blank"""" style=""text-decoration: none; width: fit-content; margin: 10px auto; display: block;"">
                <button class=""buton1"" style=""width: fit-content; margin: 10px auto; display: block;  cursor: pointer;  padding: 10px 20px; "" >
                <span style=""color: blue; font-size: 20px;"" >{content.Aciklama4Btn1}</span>
                </button>
                    </a>


                <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"">
                  <tr>
                    <td width=""15%"" style=""font-size: 0; line-height: 0;"">&nbsp;</td>
                    <td width=""70%"" align=""center"">
                      <img class=""companylogogif"" src=""https://www.tammerkez.com/assets2/media/picture/mailhtml/htmlcontenttammerkezgif2.gif"" alt=""Company Logo"" style=""display: block; max-width: 100%; height: auto; max-height: 70px;"" />
                    </td>
                    <td width=""15%"" style=""font-size: 0; line-height: 0;"">&nbsp;</td>
                  </tr>
                </table>
              <br>
              <br>

                <a class=""ahrefunscribe"" href=""{content.YaziyaTiklayincaGitLinkUrl}"" target=""_blank"">Linke tıklayarak abonelikten ayrılabilirsiniz.</a>
                <div><h1> İletişim : {content.Aciklama2H1}</h1></div>
            </div>


            ";
        }
    }
}





        //<div class=""animationframe"" style=""background-color: #252839; width: 100%; padding: 0px; box-sizing: border-box;"">
        //    <img class=""companylogogif""  src=""http://localhost:4202/assets2/media/picture/mailhtml/htmlcontenttammerkezgif2.gif"" />
        //</div>

//< div class= ""animationframe"" style=""background-color: red; width: fit - content; height: fit - content; "" >
//  < iframe class= ""iframe"" src=""http://localhost:4202/mail-content-carousel"" style=""text-decoration: none; width: fit-content; margin: 10px auto; display: block;"" title=""Mail Content Carousel""></iframe>
//</ div >