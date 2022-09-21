using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace promovil_rest.Clases
{
   
    public class SendEmail
    {
        private MailMessage mail = new MailMessage();
        private SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        public void email(Email email, string cliente, string nombreCliente)
        {
            try
            {
                mail.From = new MailAddress("norman.vicenteo@gmail.com");
                mail.To.Add(email.to);
                mail.Subject = email.subject;
                mail.Body = email.body;


                Attachment attachment;
                attachment = new Attachment(email.reportPath);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("norman.vicenteo@gmail.com", "kiirriukkmrxishq");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                attachment.Dispose();
                attachment = null;
                mail.Attachments.Clear();
                mail.Attachments.Dispose();
                mail.Dispose();
                mail = null;
            }
            catch (Exception ex)
            {
                mail.Attachments.Clear();
                mail.Attachments.Dispose();
                mail.Dispose();
                SmtpServer.Dispose();

            }

        }

    }
}