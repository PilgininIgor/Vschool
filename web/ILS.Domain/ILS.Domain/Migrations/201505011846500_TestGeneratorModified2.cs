namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestGeneratorModified2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TGMixModes", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.TGCountOfTaskModes", "OrderNumber", c => c.Int(nullable: false));
            AddColumn("dbo.TGRatingCalculationModes", "OrderNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TGRatingCalculationModes", "OrderNumber");
            DropColumn("dbo.TGCountOfTaskModes", "OrderNumber");
            DropColumn("dbo.TGMixModes", "OrderNumber");
        }
    }
}
