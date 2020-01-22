using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Ubicacion
    {
        public int id { get; set; }       
        public Double latitud { get; set; }
        public Double longitud { get; set; }
        public String fecha { get; set; }
        public String nombre_cliente { get; set; }
        public String codigo_cliente { get; set; }
        public String codigo_vendedor { get; set; }
        public String motivo { get; set; }

        public override string ToString()
        {
            return "codigo_vendedor: " + codigo_vendedor + System.Environment.NewLine +
                "latitud: " + latitud.ToString() + System.Environment.NewLine +
                "longitud: " + longitud.ToString() + System.Environment.NewLine +
                "fecha: " + fecha + System.Environment.NewLine +
                "nombre_cliente: " + nombre_cliente + System.Environment.NewLine +
                "codigo_cliente: " + codigo_cliente;
        }

    }
}