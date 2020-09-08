using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sal7lyAdmin.Services
{
    public class EmailsSettings
    {
        private static string EmailFromName = "Sala7ly Center";
        private static string EmailFromAddress = "marwaahmed1096@gmail.com";
        private static string EmailUserName = "marwaahmed1096@gmail.com";
        private static string EmailPassword= "cudcrnyjrronyiqb";
        public static bool EmailSend(string EmailToName,string EmailToAddress,string Subject, string HtmlBody , string TxtBody="")
        {
            try
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress(EmailFromName,
                EmailFromAddress);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(EmailToName,
                        EmailToAddress);
                message.To.Add(to);

                message.Subject = Subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = HtmlBody;
                bodyBuilder.TextBody = "Hello World!";
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //await emailRequest.Attachment.CopyToAsync(memoryStream);
                    var memorystream = File.OpenRead(Path.Combine("wwwroot/" + AppSession.CustomerUploads, AppSession.CustomerDefaultImage));
                    //emailRequest.Attachment.CopyToAsync(memoryStream);
                    //bodyBuilder.Attachments.Add(AppSession.CustomerDefaultImage, memoryStream.ToArray());
                }
                message.Body = bodyBuilder.ToMessageBody();
                //string fileName = Path.GetFileName(model.Attachment.FileName);
                
                //message.Attachments.Add(new Attachment(model.Attachment.OpenReadStream(), fileName));
                // message.Attachments.Add(new Attachment("Hydrangeas.jpg"));
                SmtpClient client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(EmailUserName, EmailPassword);
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch(Exception ex)
            {
                return false;
            }
           
            return true;
        }
    }
}
