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

        [HttpPost]
        [ResponseType(typeof(CustomersFilter))]
        public DataSet PostClientes(CustomersFilter productosFilter)
        {
            DataSet ds = new DataSet("customers");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[sp_select_clientes]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_vendedor", SqlDbType.VarChar).Value = productosFilter.co_ven;
                    cmd.Parameters.Add("@filtro", SqlDbType.VarChar).Value = productosFilter.filtro;
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