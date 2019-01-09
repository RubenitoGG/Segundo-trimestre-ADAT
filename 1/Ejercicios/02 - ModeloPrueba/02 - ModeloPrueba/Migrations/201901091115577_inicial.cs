namespace _02___ModeloPrueba.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Telefono = c.String(),
                        Movil = c.String(),
                        Direccion = c.String(),
                        Email = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        FechaVentana = c.DateTime(),
                        EmpleadoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.LineaVentas",
                c => new
                    {
                        LineaVentaId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LineaVentaId)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.ProductoId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Precio = c.Double(nullable: false),
                        NombreProducto = c.String(),
                        NombreMarca = c.String(),
                        AnimalDirigido = c.String(),
                        Peso = c.Double(nullable: false),
                        Tamaño = c.Double(nullable: false),
                        Imagen = c.String(),
                        Stock = c.Int(nullable: false),
                        Categoria = c.String(),
                        ProveedorId = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Proveedors", t => t.ProveedorId, cascadeDelete: true)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Proveedors",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        Telefono = c.String(),
                        Movil = c.String(),
                        Direccion = c.String(),
                        Email = c.String(),
                        Habilitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineaVentas", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Productoes", "ProveedorId", "dbo.Proveedors");
            DropForeignKey("dbo.LineaVentas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Ventas", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Productoes", new[] { "ProveedorId" });
            DropIndex("dbo.LineaVentas", new[] { "VentaId" });
            DropIndex("dbo.LineaVentas", new[] { "ProductoId" });
            DropIndex("dbo.Ventas", new[] { "ClienteId" });
            DropTable("dbo.Proveedors");
            DropTable("dbo.Productoes");
            DropTable("dbo.LineaVentas");
            DropTable("dbo.Ventas");
            DropTable("dbo.Clientes");
        }
    }
}
