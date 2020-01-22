using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Ubicaciones
    {
        public int id { get; set; }
        [JsonProperty(PropertyName = "ubicaciones")]
        public List<Ubicacion> ubicaciones { get; set; }

        public override string ToString()
        {
            return "ubicaciones: " + String.Join(",", ubicaciones) + System.Environment.NewLine;
        }
    }
}