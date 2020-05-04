using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace promovil_rest.Models
{
    public class promovil_restContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public promovil_restContext() : base("name=promovil_rest.Properties.Settings.Conexion")
        {
        }

        public System.Data.Entity.DbSet<promovil_rest.Models.Clientes> Clientes { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Documentos> Documentos { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Productos> Productos { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.EstadisticasCliente> EstadisticasClientes { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Comentario> Comentarios { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Login> Logins { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.EstadoCuenta> EstadoCuentas { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Cuenta> Cuentas { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Chart> Charts { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Items> Items { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Ubicaciones> Ubicaciones { get; set; }

        public System.Data.Entity.DbSet<promovil_rest.Models.Correlativo> Correlativoes { get; set; }
    }
}
