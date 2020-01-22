using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Documentos
    {
        public int id { get; set; }
        public String empresa { get; set; }
        public String tipo { get; set; }
        public String factura { get; set; }
        public String cliente { get; set; }
        public decimal monto { get; set; }
        public int MyProperty { get; set; }
    }
}