using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class EstadisticasCliente
    {
        public int id { get; set; }
        public String tipo_doc { get; set; }
        public String cod_cliente { get; set; }
        public String fec_emis { get; set; }
    }
}