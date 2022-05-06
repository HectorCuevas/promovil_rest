using System;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using promovil_rest;
using promovil_rest.Clases;

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

                GeneratePDF generatePDF = new GeneratePDF();

                DataTable dt = GetCuentas("100");

                generatePDF.GeneratePDFusingReportViewer(dt, "C:\\Estados de cuenta\\reporte.pdf");



                mail.From = new MailAddress("norman.vicenteo@gmail.com");
                //mail.From = new MailAddress("info@corsenesa.com");
                 mail.To.Add("nvicente@prosisco.com.gt");
               // mail.To.Add("norman.vicenteo@gmail.com");
                mail.Subject = "Estado de cuenta (PRUEBA) ";
                mail.Body = "ESTE ES UN MENSAJE DE PRUEBA";

               

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("norman.vicenteo@gmail.com", "kiirriukkmrxishq");
             //   SmtpServer.Credentials = new System.Net.NetworkCredential("info@corsenesa.com", "laboratorio");
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

        public static DataTable GetCuentas(String cliente)
        {
            DataSet ds = new DataSet("cuentas");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConsoleApp1.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_estado_cuenta", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cliente", SqlDbType.VarChar).Value = cliente.ToString();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "cuentas");
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

            return ds.Tables["cuentas"];
        }
    }
}
