namespace ProyectoCine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoriaPelicula",
                c => new
                    {
                        CodigoCategoriaPelicula = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CodigoCategoriaPelicula);
            
            CreateTable(
                "dbo.Pelicula",
                c => new
                    {
                        CodigoPelicula = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false),
                        duracion = c.String(nullable: false),
                        fechaEstreno = c.String(nullable: false),
                        imagen = c.String(nullable: false),
                        CodigoProductora = c.String(nullable: false, maxLength: 128),
                        CodigoCategoriaPelicula = c.String(nullable: false, maxLength: 128),
                        enCartelera = c.Boolean(nullable: false),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CodigoPelicula)
                .ForeignKey("dbo.CategoriaPelicula", t => t.CodigoCategoriaPelicula, cascadeDelete: true)
                .ForeignKey("dbo.Productora", t => t.CodigoProductora, cascadeDelete: true)
                .Index(t => t.CodigoProductora)
                .Index(t => t.CodigoCategoriaPelicula);
            
            CreateTable(
                "dbo.Funcion",
                c => new
                    {
                        CodigoFuncion = c.String(nullable: false, maxLength: 128),
                        formato = c.String(nullable: false),
                        CodigoSala = c.String(nullable: false, maxLength: 128),
                        CodigoPelicula = c.String(nullable: false, maxLength: 128),
                        fechaInicio = c.String(nullable: false),
                        fechaFin = c.String(nullable: false),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CodigoFuncion)
                .ForeignKey("dbo.Pelicula", t => t.CodigoPelicula, cascadeDelete: true)
                .ForeignKey("dbo.Sala", t => t.CodigoSala, cascadeDelete: true)
                .Index(t => t.CodigoSala)
                .Index(t => t.CodigoPelicula);
            
            CreateTable(
                "dbo.Horario",
                c => new
                    {
                        DiaSemana = c.String(nullable: false, maxLength: 128),
                        CodigoFuncion = c.String(nullable: false, maxLength: 128),
                        horaInicio = c.String(nullable: false),
                        horaFin = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.DiaSemana, t.CodigoFuncion })
                .ForeignKey("dbo.Funcion", t => t.CodigoFuncion, cascadeDelete: true)
                .Index(t => t.CodigoFuncion);
            
            CreateTable(
                "dbo.Sala",
                c => new
                    {
                        CodigoSala = c.String(nullable: false, maxLength: 128),
                        numero = c.Int(nullable: false),
                        numFilas = c.Int(nullable: false),
                        numColumnas = c.Int(nullable: false),
                        formato = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CodigoSala);
            
            CreateTable(
                "dbo.Productora",
                c => new
                    {
                        CodigoProductora = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false),
                        pais = c.String(),
                    })
                .PrimaryKey(t => t.CodigoProductora);
            
            CreateTable(
                "dbo.CategoriaProducto",
                c => new
                    {
                        CodigoCategoriaProducto = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false),
                        imagen = c.String(nullable: false),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CodigoCategoriaProducto);
            
            CreateTable(
                "dbo.Producto",
                c => new
                    {
                        CodigoProducto = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false),
                        precio = c.Double(nullable: false),
                        imagen = c.String(nullable: false),
                        stock = c.Int(nullable: false),
                        CodigoCategoriaProducto = c.String(nullable: false, maxLength: 128),
                        CodigoProveedor = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CodigoProducto)
                .ForeignKey("dbo.CategoriaProducto", t => t.CodigoCategoriaProducto, cascadeDelete: true)
                .ForeignKey("dbo.Proveedor", t => t.CodigoProveedor, cascadeDelete: true)
                .Index(t => t.CodigoCategoriaProducto)
                .Index(t => t.CodigoProveedor);
            
            CreateTable(
                "dbo.LineaVenta",
                c => new
                    {
                        CodigoLineaVenta = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        CodigoProducto = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CodigoLineaVenta, t.VentaId })
                .ForeignKey("dbo.Producto", t => t.CodigoProducto, cascadeDelete: true)
                .ForeignKey("dbo.Venta", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.CodigoProducto);
            
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        fecha = c.DateTime(nullable: false),
                        CodigoEmpleado = c.String(nullable: false, maxLength: 128),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId)
                .ForeignKey("dbo.Empleado", t => t.CodigoEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .Index(t => t.CodigoEmpleado)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        tipo = c.String(nullable: false),
                        nombre = c.String(nullable: false),
                        apellidos = c.String(nullable: false),
                        nif = c.String(nullable: false, maxLength: 9),
                        correo = c.String(nullable: false),
                        telefono = c.String(nullable: false, maxLength: 9),
                        direccion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Entrada",
                c => new
                    {
                        EntradaId = c.Int(nullable: false, identity: true),
                        butaca = c.String(nullable: false),
                        CodigoEmpleado = c.String(nullable: false, maxLength: 128),
                        nombreEmpleado = c.String(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        CodigoTarifa = c.String(nullable: false, maxLength: 128),
                        CodigoFuncion = c.String(nullable: false, maxLength: 128),
                        hora = c.String(nullable: false),
                        fecha = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EntradaId)
                .ForeignKey("dbo.Cliente", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Empleado", t => t.CodigoEmpleado, cascadeDelete: true)
                .ForeignKey("dbo.Funcion", t => t.CodigoFuncion, cascadeDelete: true)
                .ForeignKey("dbo.Tarifa", t => t.CodigoTarifa, cascadeDelete: true)
                .Index(t => t.CodigoEmpleado)
                .Index(t => t.ClienteId)
                .Index(t => t.CodigoTarifa)
                .Index(t => t.CodigoFuncion);
            
            CreateTable(
                "dbo.Empleado",
                c => new
                    {
                        CodigoEmpleado = c.String(nullable: false, maxLength: 128),
                        rango = c.String(nullable: false),
                        nombre = c.String(nullable: false),
                        apellidos = c.String(nullable: false),
                        usuario = c.String(nullable: false),
                        contrasena = c.String(nullable: false, maxLength: 16),
                        nif = c.String(nullable: false, maxLength: 9),
                        telefono = c.String(nullable: false, maxLength: 9),
                        correo = c.String(nullable: false),
                        direccion = c.String(nullable: false),
                        fechaAlta = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CodigoEmpleado);
            
            CreateTable(
                "dbo.Tarifa",
                c => new
                    {
                        CodigoTarifa = c.String(nullable: false, maxLength: 128),
                        formatoSala = c.String(nullable: false),
                        formatoFuncion = c.String(nullable: false),
                        formatoCliente = c.String(nullable: false),
                        diaEspectador = c.Boolean(nullable: false),
                        precio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CodigoTarifa);
            
            CreateTable(
                "dbo.Proveedor",
                c => new
                    {
                        CodigoProveedor = c.String(nullable: false, maxLength: 128),
                        tipo = c.String(nullable: false),
                        nombre = c.String(nullable: false),
                        apellidos = c.String(nullable: false),
                        nif = c.String(nullable: false, maxLength: 9),
                        telefono = c.String(nullable: false, maxLength: 9),
                        correo = c.String(nullable: false),
                        direccion = c.String(nullable: false),
                        personaContacto = c.String(),
                    })
                .PrimaryKey(t => t.CodigoProveedor);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producto", "CodigoProveedor", "dbo.Proveedor");
            DropForeignKey("dbo.LineaVenta", "VentaId", "dbo.Venta");
            DropForeignKey("dbo.Venta", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Entrada", "CodigoTarifa", "dbo.Tarifa");
            DropForeignKey("dbo.Entrada", "CodigoFuncion", "dbo.Funcion");
            DropForeignKey("dbo.Venta", "CodigoEmpleado", "dbo.Empleado");
            DropForeignKey("dbo.Entrada", "CodigoEmpleado", "dbo.Empleado");
            DropForeignKey("dbo.Entrada", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.LineaVenta", "CodigoProducto", "dbo.Producto");
            DropForeignKey("dbo.Producto", "CodigoCategoriaProducto", "dbo.CategoriaProducto");
            DropForeignKey("dbo.Pelicula", "CodigoProductora", "dbo.Productora");
            DropForeignKey("dbo.Funcion", "CodigoSala", "dbo.Sala");
            DropForeignKey("dbo.Funcion", "CodigoPelicula", "dbo.Pelicula");
            DropForeignKey("dbo.Horario", "CodigoFuncion", "dbo.Funcion");
            DropForeignKey("dbo.Pelicula", "CodigoCategoriaPelicula", "dbo.CategoriaPelicula");
            DropIndex("dbo.Entrada", new[] { "CodigoFuncion" });
            DropIndex("dbo.Entrada", new[] { "CodigoTarifa" });
            DropIndex("dbo.Entrada", new[] { "ClienteId" });
            DropIndex("dbo.Entrada", new[] { "CodigoEmpleado" });
            DropIndex("dbo.Venta", new[] { "ClienteId" });
            DropIndex("dbo.Venta", new[] { "CodigoEmpleado" });
            DropIndex("dbo.LineaVenta", new[] { "CodigoProducto" });
            DropIndex("dbo.LineaVenta", new[] { "VentaId" });
            DropIndex("dbo.Producto", new[] { "CodigoProveedor" });
            DropIndex("dbo.Producto", new[] { "CodigoCategoriaProducto" });
            DropIndex("dbo.Horario", new[] { "CodigoFuncion" });
            DropIndex("dbo.Funcion", new[] { "CodigoPelicula" });
            DropIndex("dbo.Funcion", new[] { "CodigoSala" });
            DropIndex("dbo.Pelicula", new[] { "CodigoCategoriaPelicula" });
            DropIndex("dbo.Pelicula", new[] { "CodigoProductora" });
            DropTable("dbo.Proveedor");
            DropTable("dbo.Tarifa");
            DropTable("dbo.Empleado");
            DropTable("dbo.Entrada");
            DropTable("dbo.Cliente");
            DropTable("dbo.Venta");
            DropTable("dbo.LineaVenta");
            DropTable("dbo.Producto");
            DropTable("dbo.CategoriaProducto");
            DropTable("dbo.Productora");
            DropTable("dbo.Sala");
            DropTable("dbo.Horario");
            DropTable("dbo.Funcion");
            DropTable("dbo.Pelicula");
            DropTable("dbo.CategoriaPelicula");
        }
    }
}
