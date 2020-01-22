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
using System.Web.Http;
using System.Web.Http.Description;
using promovil_rest.Models;

namespace promovil_rest.Controllers
{
    public class DocumentosController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();

        // GET: api/Documentos
        public IQueryable<Documentos> GetDocumentos()
        {
            return db.Documentos;
        }

        // GET: api/Documentos/5
        // [Route("api/Documentos/id/id2")]
        [Route("api/Documentos/{id}/{id2}")]
        public DataSet GetDocumentos(String id, String id2)
        {
            DataSet ds = new DataSet("documentos");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_documentos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_ven", SqlDbType.VarChar).Value = id.ToString();
                    cmd.Parameters.Add("@fec_emis", SqlDbType.VarChar).Value = id2.ToString();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "documentos");
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

        // PUT: api/Documentos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDocumentos(int id, Documentos documentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != documentos.id)
            {
                return BadRequest();
            }

            db.Entry(documentos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentosExists(id))
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

        // POST: api/Documentos
        [ResponseType(typeof(Documentos))]
        public IHttpActionResult PostDocumentos(Documentos documentos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Documentos.Add(documentos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = documentos.id }, documentos);
        }

        // DELETE: api/Documentos/5
        [ResponseType(typeof(Documentos))]
        public IHttpActionResult DeleteDocumentos(int id)
        {
            Documentos documentos = db.Documentos.Find(id);
            if (documentos == null)
            {
                return NotFound();
            }

            db.Documentos.Remove(documentos);
            db.SaveChanges();

            return Ok(documentos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocumentosExists(int id)
        {
            return db.Documentos.Count(e => e.id == id) > 0;
        }
    }
}