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
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");



                mail.From = new MailAddress("normandaniel.galileo@outlook.com");
                mail.To.Add("norman.vicenteo@gmail.com");
                mail.Subject = "Cotización";
                mail.Body = "ESTE ES UN MENSAJE";

               

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("normandaniel.galileo@outlook.com", "G3n3sys2017");
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
