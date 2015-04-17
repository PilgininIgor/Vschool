namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAchievements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "QuestionRun_Id", c => c.Guid());
            AddColumn("dbo.Users", "Coins", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.GameAchievements", "Message", c => c.String());
            AddColumn("dbo.GameAchievements", "AchievementAwardType", c => c.Int(nullable: false));
            AddColumn("dbo.GameAchievements", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.GameAchievements", "AdditionalParameters", c => c.String());
            CreateIndex("dbo.Answers", "QuestionRun_Id");
            AddForeignKey("dbo.Answers", "QuestionRun_Id", "dbo.QuestionRuns", "Id");
            DropColumn("dbo.Users", "EXP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "EXP", c => c.Int(nullable: false));
            DropForeignKey("dbo.Answers", "QuestionRun_Id", "dbo.QuestionRuns");
            DropIndex("dbo.Answers", new[] { "QuestionRun_Id" });
            DropColumn("dbo.GameAchievements", "AdditionalParameters");
            DropColumn("dbo.GameAchievements", "Score");
            DropColumn("dbo.GameAchievements", "AchievementAwardType");
            DropColumn("dbo.GameAchievements", "Message");
            DropColumn("dbo.Users", "Rating");
            DropColumn("dbo.Users", "Coins");
            DropColumn("dbo.Answers", "QuestionRun_Id");
        }
    }
}
