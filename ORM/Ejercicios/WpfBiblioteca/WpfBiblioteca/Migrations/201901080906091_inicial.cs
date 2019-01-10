namespace WpfBiblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ejemplares",
                c => new
                    {
                        LibroId = c.Int(nullable: false),
                        NumeroEjemplar = c.Int(nullable: false),
                        FechaPublicacion = c.DateTime(),
                        Estado = c.String(),
                    })
                .PrimaryKey(t => new { t.LibroId, t.NumeroEjemplar })
                .ForeignKey("dbo.Libros", t => t.LibroId, cascadeDelete: true)
                .Index(t => t.LibroId);
            
            CreateTable(
                "dbo.Libros",
                c => new
                    {
                        LibroId = c.Int(nullable: false, identity: true),
                        Isbn = c.String(),
                        Titulo = c.String(),
                        Editorial = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.LibroId);
            
            CreateTable(
                "dbo.Prestamos",
                c => new
                    {
                        LibroId = c.Int(nullable: false),
                        NumeroEjemplar = c.Short(nullable: false),
                        SocioId = c.Int(nullable: false),
                        FechaPrestamo = c.DateTime(nullable: false),
                        FechaDevolucion = c.DateTime(),
                        Ejemplar_LibroId = c.Int(),
                        Ejemplar_NumeroEjemplar = c.Int(),
                    })
                .PrimaryKey(t => new { t.LibroId, t.NumeroEjemplar, t.SocioId })
                .ForeignKey("dbo.Ejemplares", t => new { t.Ejemplar_LibroId, t.Ejemplar_NumeroEjemplar })
                .ForeignKey("dbo.Socios", t => t.SocioId, cascadeDelete: true)
                .Index(t => t.SocioId)
                .Index(t => new { t.Ejemplar_LibroId, t.Ejemplar_NumeroEjemplar });
            
            CreateTable(
                "dbo.Socios",
                c => new
                    {
                        SocioId = c.Int(nullable: false, identity: true),
                        Dni = c.String(),
                        Nombre = c.String(),
                        Apellidos = c.String(),
                        Direccion = c.String(),
                        Correo = c.String(),
                        Telefono = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.SocioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prestamos", "SocioId", "dbo.Socios");
            DropForeignKey("dbo.Prestamos", new[] { "Ejemplar_LibroId", "Ejemplar_NumeroEjemplar" }, "dbo.Ejemplares");
            DropForeignKey("dbo.Ejemplares", "LibroId", "dbo.Libros");
            DropIndex("dbo.Prestamos", new[] { "Ejemplar_LibroId", "Ejemplar_NumeroEjemplar" });
            DropIndex("dbo.Prestamos", new[] { "SocioId" });
            DropIndex("dbo.Ejemplares", new[] { "LibroId" });
            DropTable("dbo.Socios");
            DropTable("dbo.Prestamos");
            DropTable("dbo.Libros");
            DropTable("dbo.Ejemplares");
        }
    }
}
