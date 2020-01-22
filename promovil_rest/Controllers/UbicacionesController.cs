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
    public class UbicacionesController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();

        // GET: api/Ubicaciones
        public IQueryable<Ubicaciones> GetUbicaciones()
        {
            return db.Ubicaciones;
        }

        // GET: api/Ubicaciones/5
        [Route("api/Ubicaciones/{id}")]
        public DataSet GetUbicaciones(String id)
        {
            DataSet ds = new DataSet("Ubicaciones");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_visitas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_cli", SqlDbType.VarChar).Value = id;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "Ubicaciones");
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

        // PUT: api/Ubicaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUbicaciones(int id, Ubicaciones ubicaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ubicaciones.id)
            {
                return BadRequest();
            }

            db.Entry(ubicaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbicacionesExists(id))
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

        // POST: api/Ubicaciones
        [HttpPost]
        [ResponseType(typeof(Ubicaciones))]
        public int PostUbicaciones(Ubicaciones ubicaciones)
        {
            int retRecord = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                foreach (Ubicacion ubicacion in ubicaciones.ubicaciones) {
                    using (SqlCommand cmd = new SqlCommand("sp_insert_ubicacion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("latitud", SqlDbType.VarChar).Value = ubicacion.latitud;
                        cmd.Parameters.Add("longitud", SqlDbType.VarChar).Value = ubicacion.longitud;
                        cmd.Parameters.Add("fecha", SqlDbType.VarChar).Value = ubicacion.fecha;
                        cmd.Parameters.Add("nombre_cliente", SqlDbType.VarChar).Value = ubicacion.nombre_cliente;
                        cmd.Parameters.Add("codigo_cliente", SqlDbType.VarChar).Value = ubicacion.codigo_cliente;
                        cmd.Parameters.Add("codigo_vendedor", SqlDbType.VarChar).Value = ubicacion.codigo_vendedor;
                        cmd.Parameters.Add("motivo", SqlDbType.VarChar).Value = ubicacion.motivo;
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        retRecord = cmd.ExecuteNonQuery();
                    }
                }
               
            }
            return retRecord;
        }

        // DELETE: api/Ubicaciones/5
        [ResponseType(typeof(Ubicaciones))]
        public IHttpActionResult DeleteUbicaciones(int id)
        {
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return NotFound();
            }

            db.Ubicaciones.Remove(ubicaciones);
            db.SaveChanges();

            return Ok(ubicaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UbicacionesExists(int id)
        {
            return db.Ubicaciones.Count(e => e.id == id) > 0;
        }
    }
}