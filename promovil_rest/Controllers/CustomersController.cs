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
using promovil_rest.Models.Responses;

namespace promovil_rest.Controllers
{
    public class CustomersController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();

        // GET: api/Customers
        public IQueryable<Clientes> GetClientes()
        {
            return db.Clientes;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Clientes))]
        public DataSet GetClientes(String id)
        {
            DataSet ds = new DataSet("customers");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_clientes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_vendedor", SqlDbType.VarChar).Value = id.ToString();
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "Customers");
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

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClientes(int id, Clientes clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clientes.id)
            {
                return BadRequest();
            }

            db.Entry(clientes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Clientes))]
        public IHttpActionResult PostClientes(Clientes clientes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(clientes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = clientes.id }, clientes);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Clientes))]
        public IHttpActionResult DeleteClientes(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(clientes);
            db.SaveChanges();

            return Ok(clientes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientesExists(int id)
        {
            return db.Clientes.Count(e => e.id == id) > 0;
        }
    }
}