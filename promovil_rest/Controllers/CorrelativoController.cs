using promovil_rest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace promovil_rest.Controllers
{
    [RoutePrefix("api/Correlativo")]
    public class CorrelativoController : ApiController
    {
        [ResponseType(typeof(Correlativo))]
        public int PostComentario(Correlativo correlativo)
        {
            int res = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TPOS_CORRELATIVO", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("pTIPO_DOC", SqlDbType.VarChar).Value = correlativo.tipo_doc;
                    cmd.Parameters.Add("pCO_SUCU", SqlDbType.VarChar).Value = correlativo.co_sucu;
                    cmd.Parameters.Add("pAccion", SqlDbType.Char).Value = correlativo.accion;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);

                    }
                }
            }
            return res;
        }
    }
}
