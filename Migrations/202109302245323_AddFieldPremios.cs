namespace ApiPresupuesto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldPremios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Premios", "Entregados", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Premios", "Entregados");
        }
    }
}
