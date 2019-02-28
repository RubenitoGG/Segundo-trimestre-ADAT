namespace GestorTiendaInformatica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class largo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productos", "Nombre", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Productos", "Descripcion", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productos", "Descripcion", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Productos", "Nombre", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
