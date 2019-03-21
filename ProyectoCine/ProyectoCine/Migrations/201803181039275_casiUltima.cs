namespace ProyectoCine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class casiUltima : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tarifa", "diaEspectador", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tarifa", "formatoOtros");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tarifa", "formatoOtros", c => c.String());
            DropColumn("dbo.Tarifa", "diaEspectador");
        }
    }
}
