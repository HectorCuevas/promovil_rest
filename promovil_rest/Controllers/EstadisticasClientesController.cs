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
    public class EstadisticasClientesController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();

        // GET: api/EstadisticasClientes
        public IQueryable<EstadisticasCliente> GetEstadisticasClientes()
        {
            return db.EstadisticasClientes;
        }

        // GET: api/EstadisticasClientes/5
        [Route("api/EstadisticasClientes/{tipoDoc}/{factNum}/{coCliente}")]
        public DataSet GetEstadisticasCliente(String tipoDoc, String factNum, String cocliente)
        {
            DataSet ds = new DataSet("clientes");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_estadisticasXCliente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tipo_doc", SqlDbType.VarChar).Value = tipoDoc;
                    cmd.Parameters.Add("@fact_num", SqlDbType.VarChar).Value = factNum;
                    cmd.Parameters.Add("@co_cli", SqlDbType.VarChar).Value = cocliente;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "clientes");
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

        // PUT: api/EstadisticasClientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstadisticasCliente(int id, EstadisticasCliente estadisticasCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estadisticasCliente.id)
            {
                return BadRequest();
            }

            db.Entry(estadisticasCliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadisticasClienteExists(id))
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

        // POST: api/EstadisticasClientes
        [ResponseType(typeof(EstadisticasCliente))]
        public IHttpActionResult PostEstadisticasCliente(EstadisticasCliente estadisticasCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstadisticasClientes.Add(estadisticasCliente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estadisticasCliente.id }, estadisticasCliente);
        }

        // DELETE: api/EstadisticasClientes/5
        [ResponseType(typeof(EstadisticasCliente))]
        public IHttpActionResult DeleteEstadisticasCliente(int id)
        {
            EstadisticasCliente estadisticasCliente = db.EstadisticasClientes.Find(id);
            if (estadisticasCliente == null)
            {
                return NotFound();
            }

            db.EstadisticasClientes.Remove(estadisticasCliente);
            db.SaveChanges();

            return Ok(estadisticasCliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstadisticasClienteExists(int id)
        {
            return db.EstadisticasClientes.Count(e => e.id == id) > 0;
        }
    }
}