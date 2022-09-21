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
        public string body { get; set; }
        public string subject { get; set; }
        public string reportPath { get; set; }
        public string to { get; set; }
        public string from { get; set; }

    }
}