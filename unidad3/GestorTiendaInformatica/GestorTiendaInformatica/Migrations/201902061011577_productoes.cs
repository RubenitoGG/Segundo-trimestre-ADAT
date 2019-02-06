namespace GestorTiendaInformatica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productoes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Productoes", newName: "Productos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Productos", newName: "Productoes");
        }
    }
}
