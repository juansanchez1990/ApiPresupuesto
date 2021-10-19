namespace ApiPresupuesto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldSupermercado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Premios", "Supermercado", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Premios", "Supermercado");
        }
    }
}
