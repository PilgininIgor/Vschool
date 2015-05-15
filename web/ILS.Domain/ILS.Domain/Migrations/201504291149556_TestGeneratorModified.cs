namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestGeneratorModified : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ThemeContents", name: "TGTestSetting_Id", newName: "TestSetting_Id");
            RenameColumn(table: "dbo.TGTestSettings", name: "TGCountOfTaskMode_Id", newName: "CountOfTaskMode_Id");
            RenameColumn(table: "dbo.TGTestSettings", name: "TGRatingCalculationMode_Id", newName: "RatingCalculationMode_Id");
            RenameIndex(table: "dbo.ThemeContents", name: "IX_TGTestSetting_Id", newName: "IX_TestSetting_Id");
            RenameIndex(table: "dbo.TGTestSettings", name: "IX_TGCountOfTaskMode_Id", newName: "IX_CountOfTaskMode_Id");
            RenameIndex(table: "dbo.TGTestSettings", name: "IX_TGRatingCalculationMode_Id", newName: "IX_RatingCalculationMode_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TGTestSettings", name: "IX_RatingCalculationMode_Id", newName: "IX_TGRatingCalculationMode_Id");
            RenameIndex(table: "dbo.TGTestSettings", name: "IX_CountOfTaskMode_Id", newName: "IX_TGCountOfTaskMode_Id");
            RenameIndex(table: "dbo.ThemeContents", name: "IX_TestSetting_Id", newName: "IX_TGTestSetting_Id");
            RenameColumn(table: "dbo.TGTestSettings", name: "RatingCalculationMode_Id", newName: "TGRatingCalculationMode_Id");
            RenameColumn(table: "dbo.TGTestSettings", name: "CountOfTaskMode_Id", newName: "TGCountOfTaskMode_Id");
            RenameColumn(table: "dbo.ThemeContents", name: "TestSetting_Id", newName: "TGTestSetting_Id");
        }
    }
}
