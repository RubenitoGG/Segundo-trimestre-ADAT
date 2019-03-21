namespace ProyectoCine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ultima : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CodigoCategoria = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false),
                        descripcion = c.String(),
                    })
                .PrimaryKey(t => t.CodigoCategoria);
            
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
                        CodigoCategoria = c.String(nullable: false, maxLength: 128),
                        enCartelera = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CodigoPelicula)
                .ForeignKey("dbo.Categoria", t => t.CodigoCategoria, cascadeDelete: true)
                .ForeignKey("dbo.Productora", t => t.CodigoProductora, cascadeDelete: true)
                .Index(t => t.CodigoProductora)
                .Index(t => t.CodigoCategoria);
            
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
                        numero = c.String(nullable: false),
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
                "dbo.Cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        tipo = c.String(nullable: false),
                        nombre = c.String(nullable: false),
                        apellidos = c.String(nullable: false),
                        nif = c.String(nullable: false, maxLength: 9),
                        correo = c.String(nullable: false),
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
                        formatoOtros = c.String(),
                        precio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CodigoTarifa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entrada", "CodigoTarifa", "dbo.Tarifa");
            DropForeignKey("dbo.Entrada", "CodigoFuncion", "dbo.Funcion");
            DropForeignKey("dbo.Entrada", "CodigoEmpleado", "dbo.Empleado");
            DropForeignKey("dbo.Entrada", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Pelicula", "CodigoProductora", "dbo.Productora");
            DropForeignKey("dbo.Funcion", "CodigoSala", "dbo.Sala");
            DropForeignKey("dbo.Funcion", "CodigoPelicula", "dbo.Pelicula");
            DropForeignKey("dbo.Horario", "CodigoFuncion", "dbo.Funcion");
            DropForeignKey("dbo.Pelicula", "CodigoCategoria", "dbo.Categoria");
            DropIndex("dbo.Entrada", new[] { "CodigoFuncion" });
            DropIndex("dbo.Entrada", new[] { "CodigoTarifa" });
            DropIndex("dbo.Entrada", new[] { "ClienteId" });
            DropIndex("dbo.Entrada", new[] { "CodigoEmpleado" });
            DropIndex("dbo.Horario", new[] { "CodigoFuncion" });
            DropIndex("dbo.Funcion", new[] { "CodigoPelicula" });
            DropIndex("dbo.Funcion", new[] { "CodigoSala" });
            DropIndex("dbo.Pelicula", new[] { "CodigoCategoria" });
            DropIndex("dbo.Pelicula", new[] { "CodigoProductora" });
            DropTable("dbo.Tarifa");
            DropTable("dbo.Empleado");
            DropTable("dbo.Entrada");
            DropTable("dbo.Cliente");
            DropTable("dbo.Productora");
            DropTable("dbo.Sala");
            DropTable("dbo.Horario");
            DropTable("dbo.Funcion");
            DropTable("dbo.Pelicula");
            DropTable("dbo.Categoria");
        }
    }
}
