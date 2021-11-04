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
    public class ProductosController : ApiController
    {
        private promovil_restContext db = new promovil_restContext();


        // GET: api/Productos/5
        [Route("api/Productos/{id}/{id2}")]
        public DataSet GetProductos(String id, String id2)
        {

            DataSet ds = new DataSet("productos");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_select_productos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_ven", SqlDbType.VarChar).Value = "";
                    cmd.Parameters.Add("@fec_emis", SqlDbType.VarChar).Value = "";
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "productos");
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
        [ResponseType(typeof(ProductosFilter))]
        public DataSet PostUbicaciones(ProductosFilter productosFilter)
        {
            DataSet ds = new DataSet("productos");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[sp_select_productos2]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@co_ven", SqlDbType.VarChar).Value = productosFilter.co_ven;
                    cmd.Parameters.Add("@filtro", SqlDbType.VarChar).Value = productosFilter.filtro;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.TableMappings.Add("Table", "productos");
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

        /* // GET: api/Productos/5
         [Route("api/EstadoCuenta/{id}")]
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
                     //       adp.TableMappings.Add("Table", "EstadoCuenta");
                     adp.SelectCommand = cmd;
                     adp.Fill(ds);

                     if (con.State == ConnectionState.Open)
                     {
                         con.Close();
                     }
                 }
             }
              return ds;
         }*/

       
    }
}