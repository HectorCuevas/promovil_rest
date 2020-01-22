using Newtonsoft.Json;
using System;

namespace promovil_rest.Models
{
    public class EstadoCuenta
    {
        public int id { get; set; }
        [JsonProperty(PropertyName = "Tienda")]
        public String Tienda { get; set; }
        [JsonProperty(PropertyName = "Cliente")]
        public int Cliente { get; set; }
        [JsonProperty(PropertyName = "NombreCliente")]
        public String NombreCliente { get; set; }
        [JsonProperty(PropertyName = "Paciente")]
        public String Paciente { get; set; }
        [JsonProperty(PropertyName = "TipoDocumento")]
        public String TipoDocumento { get; set; }
        [JsonProperty(PropertyName = "NumeroDocumentos")]
        public int NumeroDocumentos { get; set; }
        [JsonProperty(PropertyName = "Saldo")]
        public Double SaldoTotal { get; set; }
        [JsonProperty(PropertyName = "porvencer")]
        public Decimal PORVENCER { get; set; }
        [JsonProperty(PropertyName = "credA")]
        public Decimal V030 { get; set; }
        [JsonProperty(PropertyName = "credB")]
        public Decimal V3160 { get; set; }
        [JsonProperty(PropertyName = "credC")]
        public Decimal V6190 { get; set; }
        [JsonProperty(PropertyName = "credD")]
        public Decimal M90 { get; set; }
        [JsonProperty(PropertyName = "FechaEmision")]
        public DateTime FechaEmision { get; set; }
        [JsonProperty(PropertyName = "FechaVencimiento")]
        public DateTime FechaVencimiento { get; set; }
        [JsonProperty(PropertyName = "Vencido")]
        public Decimal Vencido { get; set; }


        public override string ToString()
        {
            return "resultado: " + Tienda + System.Environment.NewLine +
                "descripcion: " + NombreCliente + System.Environment.NewLine +
                "archivo: " + Paciente;
        }
    }

   

}