namespace GestorTiendaInformatica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class keys : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categorias", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Categorias", "Imagen", c => c.String(nullable: false));
            AlterColumn("dbo.Categorias", "Descripcion", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Productoes", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Productoes", "Descripcion", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Productoes", "Imagen", c => c.String(nullable: false));
            AlterColumn("dbo.Clientes", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Clientes", "Telefono", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Clientes", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Clientes", "Direccion", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "TipoCuenta", c => c.String(nullable: false));
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Usuarios", "user", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Usuarios", "password", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Proveedores", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Proveedores", "Telefono", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Proveedores", "Direccion", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Proveedores", "NombreContacto", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proveedores", "NombreContacto", c => c.String());
            AlterColumn("dbo.Proveedores", "Direccion", c => c.String());
            AlterColumn("dbo.Proveedores", "Telefono", c => c.String());
            AlterColumn("dbo.Proveedores", "Nombre", c => c.String());
            AlterColumn("dbo.Usuarios", "password", c => c.String());
            AlterColumn("dbo.Usuarios", "user", c => c.String());
            AlterColumn("dbo.Usuarios", "Nombre", c => c.String());
            AlterColumn("dbo.Usuarios", "TipoCuenta", c => c.String());
            AlterColumn("dbo.Clientes", "Direccion", c => c.String());
            AlterColumn("dbo.Clientes", "Email", c => c.String());
            AlterColumn("dbo.Clientes", "Telefono", c => c.String());
            AlterColumn("dbo.Clientes", "Nombre", c => c.String());
            AlterColumn("dbo.Productoes", "Imagen", c => c.String());
            AlterColumn("dbo.Productoes", "Descripcion", c => c.String());
            AlterColumn("dbo.Productoes", "Nombre", c => c.String());
            AlterColumn("dbo.Categorias", "Descripcion", c => c.String());
            AlterColumn("dbo.Categorias", "Imagen", c => c.String());
            AlterColumn("dbo.Categorias", "Nombre", c => c.String());
        }
    }
}
