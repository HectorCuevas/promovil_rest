using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Pedidos
    {
       
        [JsonProperty(PropertyName = "pedidos")]
        public List<DetallePedido> pedidos { get; set; }
    }
} 