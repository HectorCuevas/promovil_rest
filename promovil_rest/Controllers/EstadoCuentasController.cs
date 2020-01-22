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
    public class EstadoCuentasController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();

        // GET: api/EstadoCuentas
        public IQueryable<EstadoCuenta> GetEstadoCuentas()
        {
            return db.EstadoCuentas;
        }

        // GET: api/EstadoCuentas/5
        [ResponseType(typeof(EstadoCuenta))]
        public DataSet GetEstadoCuenta(String cliente)
        {
            DataSet ds = new DataSet("EstadoCuenta");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
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
                    adp.TableMappings.Add("Table", "EstadoCuenta");
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

        // PUT: api/EstadoCuentas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstadoCuenta(int id, EstadoCuenta estadoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estadoCuenta.id)
            {
                return BadRequest();
            }

            db.Entry(estadoCuenta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoCuentaExists(id))
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

        // POST: api/EstadoCuentas
        [ResponseType(typeof(EstadoCuenta))]
        public IHttpActionResult PostEstadoCuenta(EstadoCuenta estadoCuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstadoCuentas.Add(estadoCuenta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estadoCuenta.id }, estadoCuenta);
        }

        // DELETE: api/EstadoCuentas/5
        [ResponseType(typeof(EstadoCuenta))]
        public IHttpActionResult DeleteEstadoCuenta(int id)
        {
            EstadoCuenta estadoCuenta = db.EstadoCuentas.Find(id);
            if (estadoCuenta == null)
            {
                return NotFound();
            }

            db.EstadoCuentas.Remove(estadoCuenta);
            db.SaveChanges();

            return Ok(estadoCuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstadoCuentaExists(int id)
        {
            return db.EstadoCuentas.Count(e => e.id == id) > 0;
        }
    }
}