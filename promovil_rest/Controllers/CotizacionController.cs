using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using promovil_rest.Clases;
using promovil_rest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace promovil_rest.Controllers
{
    public class CotizacionController : ApiController
    {

        private List<EstadoCuenta> list;
        private DataSet ds = new DataSet("cuentas");
        private String json;



        [Route("api/Cotizacion/{numeroPedido}")]
        //[ResponseType(typeof(Cotizacion))]
        public DataSet GetCotizacion(String numeroPedido)
        {
            int res = 0;
            DataSet ds = new DataSet("cotizacion");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.ConexionTT"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_cotizacion_header", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Value = numeroPedido;
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
            if (ds.Tables[0].Rows.Count > 0)
            {
              
                    //string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    //list = datasetToJson(ds);
                    //string nombreCliente = list[0].NombreCliente.ToString();
                    //string cliente = list[0].Cliente.ToString();
                    //pdf = new GeneratePDF();

                    ////string path = "C:\\Estados de cuenta\\reporte.pdf";
                    //string path = "C:\\Estados de cuenta\\" + cliente + fecha + ".pdf";
                    //// string path = "C:\\Users\\Norman\\Documents\\Cotizaciones\\" + cliente + fecha + ".pdf";

                    ////  pdf.ManipulatePdf(path, list);

                    //pdf.GeneratePDFusingReportViewer(ds.Tables["cuentas"], path);

                    ////string body = "Adjuntamos estado de cuenta de " + empresa + " al cliente: " + nombreCliente;
                    //string body = "Adjuntamos estado de cuenta de Tecnotools  al cliente: " + nombreCliente;


                    //email(body, path, id, cliente, nombreCliente);
                
                    //res = 1;

            }
            return ds;
        }

        [Route("api/Cotizacion/Detalle/{numeroPedido}")]
        public DataSet GetCotizacionDetalle(String numeroPedido)
        {
            DataSet ds = new DataSet("detalle");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_cotizacion_det", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Number", SqlDbType.Int).Value = numeroPedido;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "detalle");
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return ds;
        }

        public CotizacionHeader datasetToJson(DataSet ds)
        {
            json = JsonConvert.SerializeObject(ds);
            JObject cuentas = JObject.Parse(json);
            List<JToken> results = cuentas["cotizacion"].Children().ToList();
            CotizacionHeader searchResult = results[0].ToObject<CotizacionHeader>();
            return searchResult;
        }
    }
}