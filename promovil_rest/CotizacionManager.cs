using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using promovil_rest;
using promovil_rest.Clases;
using promovil_rest.Models;

namespace promovil_rest
{
    public class CotizacionManager
    {
        public CotizacionManager(int numCotix, Pedido pedido)
        {
            GeneratePDF generatePDF = new GeneratePDF();
            SendEmail sendEmail = new SendEmail();
            string fecha = DateTime.Now.ToString("yyyy-MM-dd");

            // DataTable dt = GetCuentas("100");
            DataTable enc = GetCotizacion(numCotix.ToString());
            DataTable det = GetDetCotizacion(numCotix.ToString());

            string path = Constants.reportPath + pedido.co_cli.Trim()+ fecha + ".pdf";

            generatePDF.GeneratePDFusingReportViewer(enc, det, path);

            Email email = new Email();
            email.body = "Adjuntamos la cotización de Tecnotools  al cliente: " + pedido.nombre;
            email.subject = "Cotización de " + pedido.nombre; ;
            email.to = pedido.email;
            email.from = "norman.vicenteo@gmail.com";
            email.reportPath = path;

            sendEmail.email(email, pedido.co_cli.Trim(), pedido.nombre);

        }
        public static DataTable GetCotizacion(String numeroPedido)
        {
            DataSet ds = new DataSet("cotizacion");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_cotizacion_header", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Number", SqlDbType.VarChar).Value = numeroPedido;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "cotizacion");
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

            return ds.Tables["cotizacion"];
        }

        public static DataTable GetDetCotizacion(String numeroPedido)
        {
            DataSet ds = new DataSet("cotizacion");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_cotizacion_det", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Number", SqlDbType.VarChar).Value = numeroPedido;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "cotizacion");
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }

            return ds.Tables["cotizacion"];
        }
    }
}