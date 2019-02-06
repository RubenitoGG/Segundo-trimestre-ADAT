namespace GestorTiendaInformatica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Imagen = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Imagen = c.String(),
                        Precio = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Categoria_CategoriaId = c.Int(),
                        Proveedor_ProveedorId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductoId)
                .ForeignKey("dbo.Categorias", t => t.Categoria_CategoriaId)
                .ForeignKey("dbo.Proveedores", t => t.Proveedor_ProveedorId)
                .Index(t => t.Categoria_CategoriaId)
                .Index(t => t.Proveedor_ProveedorId);
            
            CreateTable(
                "dbo.LineaVentas",
                c => new
                    {
                        LineaVentaId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        producto_ProductoId = c.Int(),
                        venta_VentaId = c.Int(),
                    })
                .PrimaryKey(t => t.LineaVentaId)
                .ForeignKey("dbo.Productoes", t => t.producto_ProductoId)
                .ForeignKey("dbo.Ventas", t => t.venta_VentaId)
                .Index(t => t.producto_ProductoId)
                .Index(t => t.venta_VentaId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(),
                        cliente_ClienteId = c.Int(),
                        usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Clientes", t => t.cliente_ClienteId)
                .ForeignKey("dbo.Usuarios", t => t.usuario_UsuarioId)
                .Index(t => t.cliente_ClienteId)
                .Index(t => t.usuario_UsuarioId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Direccion = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        TipoCuenta = c.String(),
                        Nombre = c.String(),
                        user = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Proveedores",
                c => new
                    {
                        ProveedorId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Direccion = c.String(),
                        NombreContacto = c.String(),
                    })
                .PrimaryKey(t => t.ProveedorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productoes", "Proveedor_ProveedorId", "dbo.Proveedores");
            DropForeignKey("dbo.Ventas", "usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.LineaVentas", "venta_VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Ventas", "cliente_ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.LineaVentas", "producto_ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Productoes", "Categoria_CategoriaId", "dbo.Categorias");
            DropIndex("dbo.Ventas", new[] { "usuario_UsuarioId" });
            DropIndex("dbo.Ventas", new[] { "cliente_ClienteId" });
            DropIndex("dbo.LineaVentas", new[] { "venta_VentaId" });
            DropIndex("dbo.LineaVentas", new[] { "producto_ProductoId" });
            DropIndex("dbo.Productoes", new[] { "Proveedor_ProveedorId" });
            DropIndex("dbo.Productoes", new[] { "Categoria_CategoriaId" });
            DropTable("dbo.Proveedores");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Clientes");
            DropTable("dbo.Ventas");
            DropTable("dbo.LineaVentas");
            DropTable("dbo.Productoes");
            DropTable("dbo.Categorias");
        }
    }
}
