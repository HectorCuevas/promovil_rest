using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class DetallePedido
    {
        public string tipo_doc { get; set; }
        public int fact_num { get; set; }
        public string  comentario_renglon { get; set; }
        public string co_art { get; set; }
        public decimal prec_vta { get; set; }
        public decimal total_art { get; set; }
        public string descuento { get; set; }
        public decimal reng_neto { get; set; }
        public decimal aux1 { get; set; }
        public string aux2 { get; set; }
    }
}