namespace WpfBiblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comprobaciones : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Socios", "Dni", c => c.String(nullable: false, maxLength: 9));
            AlterColumn("dbo.Socios", "Nombre", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Socios", "Apellidos", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Socios", "Direccion", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Socios", "Correo", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Socios", "Telefono", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Socios", "Password", c => c.String(nullable: false, maxLength: 3));
            CreateIndex("dbo.Socios", "Dni", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Socios", new[] { "Dni" });
            AlterColumn("dbo.Socios", "Password", c => c.String());
            AlterColumn("dbo.Socios", "Telefono", c => c.String());
            AlterColumn("dbo.Socios", "Correo", c => c.String());
            AlterColumn("dbo.Socios", "Direccion", c => c.String());
            AlterColumn("dbo.Socios", "Apellidos", c => c.String());
            AlterColumn("dbo.Socios", "Nombre", c => c.String());
            AlterColumn("dbo.Socios", "Dni", c => c.String());
        }
    }
}
