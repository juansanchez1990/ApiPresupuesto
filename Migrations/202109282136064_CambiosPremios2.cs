namespace ApiPresupuesto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosPremios2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Premios", "Id_Categoria_Presupuesto", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Premios", "Id_Categoria_Presupuesto");
        }
    }
}
