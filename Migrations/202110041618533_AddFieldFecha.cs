namespace ApiPresupuesto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldFecha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Premios", "FechaEntrega", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Premios", "FechaEntrega");
        }
    }
}
