using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");



                mail.From = new MailAddress("norman.vicenteo@gmail.com");
                mail.To.Add("mguzman@prosisco.com.gt");
                mail.Subject = "Estado de cuenta ";
                mail.Body = "ESTE ES UN MENSAJE";

               

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("norman.vicenteo@gmail.com", "0");
                SmtpServer.EnableSsl = true;


                SmtpServer.Send(mail);
                Console.Write("enviado");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            Console.ReadKey();
        }
    }
}
