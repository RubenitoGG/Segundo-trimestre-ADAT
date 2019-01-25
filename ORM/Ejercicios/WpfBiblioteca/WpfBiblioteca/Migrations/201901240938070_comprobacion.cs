namespace WpfBiblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comprobacion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ejemplares", "FechaPublicacion", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Libros", "Isbn", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Libros", "Titulo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Libros", "Editorial", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Libros", "Descripcion", c => c.String(nullable: false));
            CreateIndex("dbo.Libros", "Isbn", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Libros", new[] { "Isbn" });
            AlterColumn("dbo.Libros", "Descripcion", c => c.String());
            AlterColumn("dbo.Libros", "Editorial", c => c.String());
            AlterColumn("dbo.Libros", "Titulo", c => c.String());
            AlterColumn("dbo.Libros", "Isbn", c => c.String());
            AlterColumn("dbo.Ejemplares", "FechaPublicacion", c => c.DateTime());
        }
    }
}
