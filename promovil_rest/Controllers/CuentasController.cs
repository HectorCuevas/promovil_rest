using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using promovil_rest.Clases;
using promovil_rest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;

namespace promovil_rest.Controllers
{
    public class CuentasController : ApiController
    {
        private List<EstadoCuenta> list;
        private String json;
        private DataSet ds = new DataSet("cuentas");
        private GeneratePDF pdf;
        private string BODY = "Se envía estado de cuenta del periodo: ";
        private MailMessage mail = new MailMessage();
        private SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        private promovil_restContext db = new promovil_restContext();

        // GET: api/Cuentas
        public IQueryable<Cuenta> GetCuentas()
        {
            return db.Cuentas;
        }

        // GET: api/Cuentas/5
        [Route("api/Cuentas/{id}/{id2}/{id3}/{id4}")]
        [ResponseType(typeof(Cuenta))]
        public int GetClientes(String id, String id2, string id3, string id4)
        {
            //id=email
            //id2=cliente
            //id3=es email?
            //id4=sucursal


            int res = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_estado_cuenta", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cliente", SqlDbType.VarChar).Value = id2.ToString();
                    cmd.Parameters.Add("@sucursal", SqlDbType.VarChar).Value = id4.ToString();
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

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (id3 == "1")
                {
                
                    string fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    list = datasetToJson(ds);
                    string cliente = list[0].Cliente.ToString();
                    pdf = new GeneratePDF();
                
                   string path = "C:\\Estados de cuenta\\" + cliente + fecha + ".pdf";
                   // string path = "C:\\Users\\Norman\\Documents\\Cotizaciones\\" + cliente + fecha + ".pdf";

                    pdf.ManipulatePdf(path, list);                      
                    email(BODY + fecha, path, id, cliente);
                }
                res = 1;

            }
            return res;
        }

        [Route("api/Cuentas/{id}/{id2}")]
        [ResponseType(typeof(Cuenta))]
        public DataSet GetCuentas(String id, string id2)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_estado_cuenta", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cliente", SqlDbType.VarChar).Value = id.ToString();
                    cmd.Parameters.Add("@sucursal", SqlDbType.VarChar).Value = id2.ToString();
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
            
            return ds;
        }

        // PUT: api/Cuentas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCuenta(int id, Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuenta.id)
            {
                return BadRequest();
            }

            db.Entry(cuenta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cuentas
        [ResponseType(typeof(Cuenta))]
        public IHttpActionResult PostCuenta(Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cuentas.Add(cuenta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cuenta.id }, cuenta);
        }

        // DELETE: api/Cuentas/5
        [ResponseType(typeof(Cuenta))]
        public IHttpActionResult DeleteCuenta(int id)
        {
            Cuenta cuenta = db.Cuentas.Find(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            db.Cuentas.Remove(cuenta);
            db.SaveChanges();

            return Ok(cuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CuentaExists(int id)
        {
            return db.Cuentas.Count(e => e.id == id) > 0;
        }
        private void email(String body, String path, string email, string cliente)
        {           
            try {
                mail.From = new MailAddress("info@corsenesa.com");
                mail.To.Add(email);
                mail.Subject = "Estado de cuenta "+ cliente;
                mail.Body = body;


                Attachment attachment;
                attachment = new Attachment(path);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info@corsenesa.com", "laboratorio");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                attachment.Dispose();
                attachment = null;
                mail.Attachments.Clear();
                mail.Attachments.Dispose();
                mail.Dispose();    
                mail = null;
            }
            catch(Exception ex)
            {
                mail.Attachments.Clear();
                mail.Attachments.Dispose();
                mail.Dispose();
                SmtpServer.Dispose();

            }

        }
        public List<EstadoCuenta> datasetToJson(DataSet ds)
        {
            json = JsonConvert.SerializeObject(ds);
            JObject cuentas = JObject.Parse(json);
            List<JToken> results = cuentas["cuentas"].Children().ToList();
            List<EstadoCuenta> searchResults = new List<EstadoCuenta>();
            foreach (JToken result in results)
            {
                EstadoCuenta searchResult = result.ToObject<EstadoCuenta>();
                searchResults.Add(searchResult);
            }
            return searchResults;
        }
    }
}