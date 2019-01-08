namespace WpfBiblioteca.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Prestamos");
            AddColumn("dbo.Prestamos", "NumeroEjemp", c => c.Short(nullable: false));
            AddPrimaryKey("dbo.Prestamos", new[] { "LibroId", "NumeroEjemp", "SocioId" });
            DropColumn("dbo.Prestamos", "NumeroEjemplar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prestamos", "NumeroEjemplar", c => c.Short(nullable: false));
            DropPrimaryKey("dbo.Prestamos");
            DropColumn("dbo.Prestamos", "NumeroEjemp");
            AddPrimaryKey("dbo.Prestamos", new[] { "LibroId", "NumeroEjemplar", "SocioId" });
        }
    }
}
