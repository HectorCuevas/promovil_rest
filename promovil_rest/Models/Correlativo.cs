using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Correlativo
    {
        public int id { get; set; }
        public string tipo_doc { get; set; }
        public string co_sucu { get; set; }
        public char accion { get; set; }

    }
}   