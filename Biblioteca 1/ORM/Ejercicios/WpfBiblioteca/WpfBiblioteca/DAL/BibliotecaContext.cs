using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBiblioteca.Model;

namespace WpfBiblioteca.DAL
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext() : base("name=BibliotecaEntities") { }

        public virtual DbSet<Ejemplar> Ejemplar { get; set; }
        public virtual DbSet<Libro> Libro { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<Socio> Socio { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
