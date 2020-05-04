using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using promovil_rest.Clases;
using promovil_rest.Models;

namespace promovil_rest.Controllers
{
    public class ChartsController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();
        private String json;
        private List<Items> pedidos;
        private BodegaPDF pdf;
        private MailMessage mail = new MailMessage();
        private SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        // GET: api/Charts/5

        public IHttpActionResult GetChart(int id)
        {
            Chart chart = db.Charts.Find(id);
            if (chart == null)
            {
                return NotFound();
            }

            return Ok(chart);
        }


        // POST: api/Charts
        [HttpPost]
        [ResponseType(typeof(Chart))]
        public String PostChart(Chart carrito)
        {
            String res = "exito";
            try
            {
                String fecha = DateTime.Now.ToString("yyyy-MM-dd");
                String cliente = carrito.nombre_cliente;
                String body = "Pedido " + carrito.nombre_cliente + System.Environment.NewLine +
                             "Nombre cliente: " + carrito.nombre_cliente + System.Environment.NewLine +
                             "Codigo cliente: " + carrito.codigo_cliente + System.Environment.NewLine +
                             "Codigo vendedor: " + carrito.cod_vendedor + System.Environment.NewLine +
                             "Fecha: " + fecha;
                String to = "bodega.sl@corsenesa.com";
                 string path = "C:\\Pedidos\\" + cliente + fecha + ".pdf";
                //string path = "C:\\Users\\Norman\\Documents\\Cotizaciones\\" + cliente + fecha + ".pdf";
                pdf = new BodegaPDF();
                pedidos = carrito.items;
                pdf.generatePDF(path, carrito, pedidos);
                pedidos = carrito.items;
                

                email(body, path, to, carrito.nombre_cliente, fecha);
                res = "exito";
            }
            catch (Exception ex) {
                res = ex.ToString();
            }
            return res;
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChartExists(int id)
        {
            return db.Charts.Count(e => e.id == id) > 0;
        }

        private List<Items> jsonToClases(String ds)
        {
            json = JsonConvert.SerializeObject(ds);
            JObject cuentas = JObject.Parse(json);
            List<JToken> results = cuentas["Items"].Children().ToList();
            List<Items> searchResults = new List<Items>();
            foreach (JToken result in results)
            {
                Items searchResult = result.ToObject<Items>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
        private void email(String body, String path, string email, string cliente, String fecha)
        {
            try
            {
                mail.From = new MailAddress("info@corsenesa.com");
                mail.To.Add(email);
                mail.Subject = "Pedido  " + cliente + " fecha: " + fecha;
                mail.Body = body;


                Attachment attachment;
                attachment = new Attachment(path);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info@corsenesa.com", "prueba2019");
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