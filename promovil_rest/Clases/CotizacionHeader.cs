using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promovil_rest.Clases
{
    public class CotizacionHeader
    {
        public int id { get; set; }
        [JsonProperty("Numero")]
        public int Numero { get; }

        [JsonProperty("moneda")]
        public string Moneda { get; }

        [JsonProperty("FechaEmision")]
        public string FechaEmision { get; }

        [JsonProperty("NIT")]
        public string NIT { get; }

        [JsonProperty("nombre")]
        public string Nombre { get; }

        [JsonProperty("direccion")]
        public string Direccion { get; }

        [JsonProperty("vendedor")]
        public string Vendedor { get; }

        [JsonProperty("condicionpago")]
        public string Condicionpago { get; }

        [JsonProperty("subtotal")]
        public string Subtotal { get; }

        [JsonProperty("porcentaje_descuento")]
        public string PorcentajeDescuento { get; }

        [JsonProperty("descuento")]
        public double Descuento { get; }

        [JsonProperty("total_letras")]
        public string TotalLetras { get; }

        [JsonProperty("Total")]
        public string Total { get; }
    }
}