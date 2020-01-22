using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Items
    {
        public int id { get; set; }
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public double precio { get; set; }
        public double descuento { get; set; }

        public override string ToString()
        {
            return "producto: " + String.Join(",", producto) + System.Environment.NewLine +
                "cantidad: " + cantidad + System.Environment.NewLine +
                "precio: " + precio;
        }
    }
}