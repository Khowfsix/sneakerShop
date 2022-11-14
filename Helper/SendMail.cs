using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace WebApplication1.Helper
{
    public class SendMail
    {
        public static bool sendMail(string to, string subject, string body, string attachFile)
        {
            try
            {
                MailMessage msg = new MailMessage(ConstantHelper.emailSender, to, subject, attachFile);
                using (var client = new SmtpClient(ConstantHelper.hostEmail, ConstantHelper.portEmail))
                {
                    client.EnableSsl = true;

                    if (!string.IsNullOrEmpty(attachFile))
                    {
                        Attachment attachment = new Attachment(attachFile);
                        msg.Attachments.Add(attachment);
                    }

                    NetworkCredential credential = new NetworkCredential(ConstantHelper.emailSender, ConstantHelper.passwordSender);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    client.Send(msg);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}