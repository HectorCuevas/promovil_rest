using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class Cuentas
    {
        [JsonProperty("cuentas")]
        private EstadoCuenta[] estadoCuentas { get; set; }   
    }
}