using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Producto
    {
        public int id { get; set; }
        public String Empresa { get; set; }
        public String Codigo { get; set; }
        [JsonProperty(PropertyName = "Producto")]
        public String nombre { get; set; }
        public double prec_vta1 { get; set; }


        public override string ToString()
        {
            return "empresa: " + Empresa + System.Environment.NewLine +
                "Codigo: " + Codigo + System.Environment.NewLine +
                "nombre: " + nombre;
        }
    }
}