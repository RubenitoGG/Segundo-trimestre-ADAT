namespace ProyectoCine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstnewmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "telefono", c => c.String(nullable: false, maxLength: 9));
            AddColumn("dbo.Empleado", "telefono", c => c.String(nullable: false, maxLength: 9));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empleado", "telefono");
            DropColumn("dbo.Cliente", "telefono");
        }
    }
}
