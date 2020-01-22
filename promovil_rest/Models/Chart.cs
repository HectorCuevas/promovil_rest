using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Chart
    {
        public int id { get; set; }
        [JsonProperty(PropertyName = "Items")]
        public List<Items> items { get; set; }
        public String nombre_cliente { get; set; }
        public String codigo_cliente { get; set; }
        public String cod_vendedor { get; set; }

        public override string ToString()
        {
            return "items: " + String.Join(",", items) + System.Environment.NewLine;
        }
    }
}