using Newtonsoft.Json;
using promovil_rest.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace promovil_rest.Clases
{
    public class Email
    {
        private static String json;

        public static String datasetToJson(DataSet ds) {
            json = JsonConvert.SerializeObject(ds);
            EstadoCuenta estadoCuenta = JsonConvert.DeserializeObject<EstadoCuenta>(json);
            return json;
        }
    }
}