namespace GestorTiendaInformatica.Migrations
{
    using GestorTiendaInformatica.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GestorTiendaInformatica.DAL.TiendaInformaticaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GestorTiendaInformatica.DAL.TiendaInformaticaContext context)
        {
            Usuario admin = new Usuario();
            admin.Nombre = "Rubén";
            admin.password = "abc123.";
            admin.user = "admin";
            admin.TipoCuenta = "Administrador";
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
