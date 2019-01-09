using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using _02___ModeloPrueba.Model;

namespace _02___ModeloPrueba.DAL
{
    public class PruebaContext : DbContext
    {
        public PruebaContext() : base("name=Prueba02Entities") { }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<LineaVenta> LineaVenta { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
