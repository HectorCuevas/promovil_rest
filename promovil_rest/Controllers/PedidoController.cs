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
    public class PedidoController : ApiController
    {
        [Route("api/Pedido")]
        [ResponseType(typeof(Pedido))]
        public int PostEncabezado(Pedido pedido)
        {
            int retRecord = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TPOS_INSERTA ENCABEZADO", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@pTIPO_DOC", SqlDbType.VarChar).Value = pedido.tipo_doc;
                    cmd.Parameters.Add("@pDEVICE", SqlDbType.VarChar).Value = pedido.device;
                    cmd.Parameters.Add("@pFACT_NUM", SqlDbType.Int).Value = pedido.fact_num;
                    cmd.Parameters.Add("@pCO_CLI", SqlDbType.VarChar).Value = pedido.co_cli;
                    cmd.Parameters.Add("@pCO_VEN", SqlDbType.VarChar).Value = pedido.co_ven;
                    cmd.Parameters.Add("@pNIT", SqlDbType.VarChar).Value = pedido.nit;
                    cmd.Parameters.Add("@pDPI", SqlDbType.VarChar).Value = pedido.dpi;
                    cmd.Parameters.Add("@pNOMBRE", SqlDbType.VarChar).Value = pedido.nombre;
                    cmd.Parameters.Add("@PDIRECCION", SqlDbType.VarChar).Value = pedido.direccion;
                    cmd.Parameters.Add("@pFORMA_PAG", SqlDbType.VarChar).Value = pedido.forma_pag;
                    cmd.Parameters.Add("@pTOTAL", SqlDbType.Decimal  ).Value = pedido.total;
                    cmd.Parameters.Add("@pCOBRO", SqlDbType.Decimal).Value = pedido.cobro;
                    cmd.Parameters.Add("@pCOMENTARIO", SqlDbType.VarChar).Value = pedido.comentario;
                    cmd.Parameters.Add("@pFE_US_IN", SqlDbType.DateTime).Value = pedido.fe_us_in;
                    cmd.Parameters.Add("@pNRO_DOC", SqlDbType.Decimal).Value = pedido.nro_doc;
                    cmd.Parameters.Add("@pLATITUD", SqlDbType.Decimal).Value = pedido.latitud;
                    cmd.Parameters.Add("@pLONGITUD", SqlDbType.Decimal).Value = pedido.longitud;
                    cmd.Parameters.Add("@pDEPATAMENTO", SqlDbType.VarChar).Value = pedido.departamento;
                    cmd.Parameters.Add("@pMUNICIPIO", SqlDbType.VarChar).Value = pedido.municipio;
                    cmd.Parameters.Add("@pZONA", SqlDbType.TinyInt).Value = pedido.zona;
                    cmd.Parameters.Add("@pSUCU", SqlDbType.VarChar).Value = pedido.sucursal;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open(); 
                    }
                    retRecord = cmd.ExecuteNonQuery();
                    if(retRecord == 1)
                    {
                        retRecord = PostDetalles(pedido.detalles);
                    }
                    con.Close();
                }
            }
            return retRecord;
        }

        private int PostDetalles(List<DetallePedido> detalles)
        {
            int retRecord = 0, renglon=1;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["promovil_rest.Properties.Settings.Conexion"].ConnectionString))
            {
                foreach (DetallePedido item in detalles)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_TPOS_INSERTA DETALLE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pTIPO_DOC", SqlDbType.VarChar).Value = item.tipo_doc;
                        cmd.Parameters.Add("@pFACT_NUM", SqlDbType.Int).Value = item.fact_num;
                        cmd.Parameters.Add("@pCOMENTARIO_RENGLON", SqlDbType.VarChar).Value = item.comentario_renglon;
                        cmd.Parameters.Add("@pCO_ART", SqlDbType.VarChar).Value = item.co_art;
                        cmd.Parameters.Add("@pRENG_NUM", SqlDbType.Int).Value = renglon;
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
                        renglon=renglon+1;
                        con.Close();
                    }

                }
            }
            return retRecord;
        }
    }
}
