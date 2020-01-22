using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Comentario
    {
        public int id { get; set; }
        public String titulo { get; set; }
        public String descripcion { get; set; }     
        public String co_cli { get; set; }
        public String co_ven { get; set; }
        public int tipo_comentario { get; set; }
        public string estado { get; set; }
        public string resolucion { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_resolucion { get; set; }
    }
}