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
    public class DetallePedidoController : ApiController
    {
        [ResponseType(typeof(Pedidos))]
        public int PostDetalle(Pedidos pedido)
        {
            int retRecord = 0;
            int renglon = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                foreach(DetallePedido item in pedido.pedidos)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_TPOS_INSERTA DETALLE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pTIPO_DOC", SqlDbType.VarChar).Value = item.tipo_doc;
                        cmd.Parameters.Add("@pFACT_NUM", SqlDbType.Int).Value = item.fact_num;
                        cmd.Parameters.Add("@pCOMENTARIO_RENGLON", SqlDbType.VarChar).Value = item.comentario_renglon;
                        cmd.Parameters.Add("@pCO_ART", SqlDbType.VarChar).Value = item.co_art;
                        cmd.Parameters.Add("@pRENG_NUM", SqlDbType.VarChar).Value = renglon;
                        cmd.Parameters.Add("@pPREC_VTA", SqlDbType.VarChar).Value = item.prec_vta;
                        cmd.Parameters.Add("@pTOTAL_ART", SqlDbType.VarChar).Value = item.total_art;
                        cmd.Parameters.Add("@pDESCUENTO", SqlDbType.VarChar).Value = item.descuento;
                        cmd.Parameters.Add("@pRENG_NETO", SqlDbType.Decimal).Value = item.reng_neto;
                        cmd.Parameters.Add("@paux01", SqlDbType.Decimal).Value = item.aux1;
                        cmd.Parameters.Add("@pAUX02", SqlDbType.VarChar).Value = item.aux2;
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        retRecord = cmd.ExecuteNonQuery();
                        renglon++;
                        con.Close();
                    }
                    
                }
            }
            return retRecord;
        }
    }
}
