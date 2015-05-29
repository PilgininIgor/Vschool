namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TGRatingCalculationModes", "Description", c => c.String());
            DropColumn("dbo.TGRatingCalculationModes", "Desciption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TGRatingCalculationModes", "Desciption", c => c.String());
            DropColumn("dbo.TGRatingCalculationModes", "Description");
        }
    }
}
