using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace promovil_rest.Models
{
    public class Carrito
    {
        private int id { get; set; }
        [JsonProperty(PropertyName = "Items")]
        public List<Items> items { get; set; }
        public String nombre_cliente { get; set; }
        public String codigo_cliente { get; set; }
        public String cod_vendedor { get; set; }


        public override string ToString()
        {
            return "items: " + items + System.Environment.NewLine;
        }
    }

}