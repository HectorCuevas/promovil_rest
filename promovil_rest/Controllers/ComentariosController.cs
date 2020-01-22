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
    public class ComentariosController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();
        

        // GET: api/Comentarios
        public IQueryable<Comentario> GetComentarios()
        {
            return db.Comentarios;
        }

        // GET: api/Comentarios/5
        [Route("api/Comentarios/{id}")]
        public DataSet GetComentario(String id)
        {
            DataSet ds = new DataSet("comentarios");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_comentarios", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_cli", SqlDbType.VarChar).Value = id;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "comentarios");
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

        // PUT: api/Comentarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComentario(int id, Comentario comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comentario.id)
            {
                return BadRequest();
            }

            db.Entry(comentario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // POST: api/Comentarios
        [ResponseType(typeof(Comentario))]
        public int PostComentario(Comentario comentario)
        {
            int retRecord = 0;
            string fechaActual = "";
            fechaActual = DateTime.Now.ToString("yyyy.MM.dd");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insert_comentario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                   // cmd.Parameters.Add("titulo", SqlDbType.VarChar).Value = comentario.titulo;
                    cmd.Parameters.Add("descripcion", SqlDbType.VarChar).Value = comentario.descripcion;
                    cmd.Parameters.Add("co_cli", SqlDbType.VarChar).Value = comentario.co_cli;
                    cmd.Parameters.Add("co_ven", SqlDbType.VarChar).Value = comentario.co_ven;
                    cmd.Parameters.Add("id_tipo_comentario", SqlDbType.Int).Value = comentario.tipo_comentario;
                    cmd.Parameters.Add("titulo", SqlDbType.VarChar).Value = comentario.titulo;
                    cmd.Parameters.Add("estado", SqlDbType.VarChar).Value = comentario.estado;
                    cmd.Parameters.Add("fecha_ingreso", SqlDbType.DateTime).Value = fechaActual;
                    cmd.Parameters.Add("resolucion", SqlDbType.VarChar).Value = comentario.resolucion;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    retRecord = cmd.ExecuteNonQuery();
                }
            }
            return retRecord;
        }

        // DELETE: api/Comentarios/5
        [ResponseType(typeof(Comentario))]
        public IHttpActionResult DeleteComentario(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return NotFound();
            }

            db.Comentarios.Remove(comentario);
            db.SaveChanges();

            return Ok(comentario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComentarioExists(int id)
        {
            return db.Comentarios.Count(e => e.id == id) > 0;
        }
    }
}