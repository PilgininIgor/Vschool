namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeSpent = c.Single(nullable: false),
                        TestRun_Id = c.Guid(nullable: false),
                        AnswerVariant_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnswerVariants", t => t.AnswerVariant_Id)
                .ForeignKey("dbo.TestRuns", t => t.TestRun_Id, cascadeDelete: true)
                .Index(t => t.TestRun_Id)
                .Index(t => t.AnswerVariant_Id);
            
            CreateTable(
                "dbo.AnswerVariants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Text = c.String(),
                        IfCorrect = c.Boolean(nullable: false),
                        Question_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Text = c.String(),
                        PicQ = c.String(),
                        IfPictured = c.Boolean(nullable: false),
                        PicA = c.String(),
                        Test_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContents", t => t.Test_Id, cascadeDelete: true)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.ThemeContents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Theme_Id = c.Guid(nullable: false),
                        MinResult = c.Int(),
                        IsComposite = c.Boolean(),
                        Text = c.String(),
                        Description = c.String(),
                        TGTestSetting_Id = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.TGTestSettings", t => t.TGTestSetting_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.TGTestSetting_Id);
            
            CreateTable(
                "dbo.LinkEditorCoordinates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        User_Id = c.Guid(nullable: false),
                        Theme_Id = c.Guid(),
                        ThemeContent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id)
                .ForeignKey("dbo.ThemeContents", t => t.ThemeContent_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Theme_Id)
                .Index(t => t.ThemeContent_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        PasswordHash = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        EXP = c.Int(nullable: false),
                        FacultyStands_Seen = c.Boolean(nullable: false),
                        FacultyStands_Finish = c.Boolean(nullable: false),
                        HistoryStand_Seen = c.Boolean(nullable: false),
                        HistoryStand_Finish = c.Boolean(nullable: false),
                        ScienceStand_Seen = c.Boolean(nullable: false),
                        ScienceStand_Finish = c.Boolean(nullable: false),
                        StaffStand_Seen = c.Boolean(nullable: false),
                        StaffStand_Finish = c.Boolean(nullable: false),
                        LogotypeJump = c.Boolean(nullable: false),
                        TableJump = c.Boolean(nullable: false),
                        TerminalJump = c.Boolean(nullable: false),
                        LadderJump_First = c.Boolean(nullable: false),
                        LadderJump_All = c.Boolean(nullable: false),
                        LetThereBeLight = c.Boolean(nullable: false),
                        PlantJump_First = c.Boolean(nullable: false),
                        PlantJump_Second = c.Boolean(nullable: false),
                        BarrelRoll = c.Boolean(nullable: false),
                        FirstVisitLecture = c.Boolean(nullable: false),
                        FirstVisitTest = c.Boolean(nullable: false),
                        Teleportations = c.Int(nullable: false),
                        ParagraphsSeen = c.Int(nullable: false),
                        TestsFinished = c.Int(nullable: false),
                        UserGroup_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id)
                .Index(t => t.UserGroup_Id);
            
            CreateTable(
                "dbo.CourseRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Progress = c.Double(nullable: false),
                        TimeSpent = c.Double(nullable: false),
                        Visisted = c.Boolean(nullable: false),
                        CompleteAll = c.Boolean(nullable: false),
                        User_Id = c.Guid(nullable: false),
                        Course_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Diagramm = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Course_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.ThemeLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentTheme_Id = c.Guid(nullable: false),
                        LinkedTheme_Id = c.Guid(),
                        Theme_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.LinkedTheme_Id)
                .ForeignKey("dbo.Themes", t => t.ParentTheme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Themes", t => t.Theme_Id)
                .Index(t => t.ParentTheme_Id)
                .Index(t => t.LinkedTheme_Id)
                .Index(t => t.Theme_Id);
            
            CreateTable(
                "dbo.PersonalThemeLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(),
                        ThemeLink_Id = c.Guid(nullable: false),
                        CourseRun_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseRuns", t => t.CourseRun_Id)
                .ForeignKey("dbo.ThemeLinks", t => t.ThemeLink_Id, cascadeDelete: true)
                .Index(t => t.ThemeLink_Id)
                .Index(t => t.CourseRun_Id);
            
            CreateTable(
                "dbo.ThemeContentLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentThemeContent_Id = c.Guid(nullable: false),
                        LinkedThemeContent_Id = c.Guid(),
                        ThemeContent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContents", t => t.LinkedThemeContent_Id)
                .ForeignKey("dbo.ThemeContents", t => t.ParentThemeContent_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeContents", t => t.ThemeContent_Id)
                .Index(t => t.ParentThemeContent_Id)
                .Index(t => t.LinkedThemeContent_Id)
                .Index(t => t.ThemeContent_Id);
            
            CreateTable(
                "dbo.PersonalThemeContentLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.String(),
                        ThemeContentLink_Id = c.Guid(nullable: false),
                        ThemeRun_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContentLinks", t => t.ThemeContentLink_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeRuns", t => t.ThemeRun_Id)
                .Index(t => t.ThemeContentLink_Id)
                .Index(t => t.ThemeRun_Id);
            
            CreateTable(
                "dbo.ThemeRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Progress = c.Double(nullable: false),
                        TimeSpent = c.Double(nullable: false),
                        TestsComplete = c.Int(nullable: false),
                        AllLectures = c.Boolean(nullable: false),
                        AllTests = c.Boolean(nullable: false),
                        AllTestsMax = c.Boolean(nullable: false),
                        CompleteAll = c.Boolean(nullable: false),
                        CourseRun_Id = c.Guid(nullable: false),
                        Theme_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseRuns", t => t.CourseRun_Id, cascadeDelete: true)
                .ForeignKey("dbo.Themes", t => t.Theme_Id)
                .Index(t => t.CourseRun_Id)
                .Index(t => t.Theme_Id);
            
            CreateTable(
                "dbo.LectureRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeSpent = c.Double(nullable: false),
                        ThemeRun_Id = c.Guid(nullable: false),
                        Lecture_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContents", t => t.Lecture_Id)
                .ForeignKey("dbo.ThemeRuns", t => t.ThemeRun_Id, cascadeDelete: true)
                .Index(t => t.ThemeRun_Id)
                .Index(t => t.Lecture_Id);
            
            CreateTable(
                "dbo.Paragraphs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Header = c.String(),
                        Text = c.String(),
                        Lecture_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContents", t => t.Lecture_Id, cascadeDelete: true)
                .Index(t => t.Lecture_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        Path = c.String(),
                        Paragraph_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Paragraphs", t => t.Paragraph_Id, cascadeDelete: true)
                .Index(t => t.Paragraph_Id);
            
            CreateTable(
                "dbo.ParagraphRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HaveSeen = c.Boolean(nullable: false),
                        LectureRun_Id = c.Guid(nullable: false),
                        Paragraph_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LectureRuns", t => t.LectureRun_Id, cascadeDelete: true)
                .ForeignKey("dbo.Paragraphs", t => t.Paragraph_Id)
                .Index(t => t.LectureRun_Id)
                .Index(t => t.Paragraph_Id);
            
            CreateTable(
                "dbo.TestRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Result = c.Int(nullable: false),
                        ThemeRun_Id = c.Guid(nullable: false),
                        Test_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContents", t => t.Test_Id)
                .ForeignKey("dbo.ThemeRuns", t => t.ThemeRun_Id, cascadeDelete: true)
                .Index(t => t.ThemeRun_Id)
                .Index(t => t.Test_Id);
            
            CreateTable(
                "dbo.QuestionRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeSpent = c.Double(nullable: false),
                        TestRun_Id = c.Guid(nullable: false),
                        Question_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .ForeignKey("dbo.TestRuns", t => t.TestRun_Id, cascadeDelete: true)
                .Index(t => t.TestRun_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.TGTestSettings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TGCountOfTaskMode_Id = c.Guid(),
                        CountOfTasks = c.Int(nullable: false),
                        TGRatingCalculationMode_Id = c.Guid(),
                        IsTimeLimitMode = c.Boolean(nullable: false),
                        TimeLimitMinutes = c.Boolean(nullable: false),
                        TimeLimitSeconds = c.Boolean(nullable: false),
                        AnswersMixMode_Id = c.Guid(),
                        TasksMixMode_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TGMixModes", t => t.AnswersMixMode_Id)
                .ForeignKey("dbo.TGMixModes", t => t.TasksMixMode_Id)
                .ForeignKey("dbo.TGCountOfTaskModes", t => t.TGCountOfTaskMode_Id)
                .ForeignKey("dbo.TGRatingCalculationModes", t => t.TGRatingCalculationMode_Id)
                .Index(t => t.TGCountOfTaskMode_Id)
                .Index(t => t.TGRatingCalculationMode_Id)
                .Index(t => t.AnswersMixMode_Id)
                .Index(t => t.TasksMixMode_Id);
            
            CreateTable(
                "dbo.TGMixModes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TGCountOfTaskModes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TGRatingCalculationModes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Desciption = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EDucationAuthors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameAchievementRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        GameAchievementId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameAchievements", t => t.GameAchievementId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GameAchievementId);
            
            CreateTable(
                "dbo.GameAchievements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ImagePath = c.String(),
                        Priority = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                        AchievementTrigger = c.Int(nullable: false),
                        AchievementExecutor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.GameAchievementRuns", "UserId", "dbo.Users");
            DropForeignKey("dbo.GameAchievementRuns", "GameAchievementId", "dbo.GameAchievements");
            DropForeignKey("dbo.Answers", "TestRun_Id", "dbo.TestRuns");
            DropForeignKey("dbo.Answers", "AnswerVariant_Id", "dbo.AnswerVariants");
            DropForeignKey("dbo.AnswerVariants", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.LinkEditorCoordinates", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.CourseRuns", "User_Id", "dbo.Users");
            DropForeignKey("dbo.CourseRuns", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ThemeContents", "TGTestSetting_Id", "dbo.TGTestSettings");
            DropForeignKey("dbo.TGTestSettings", "TGRatingCalculationMode_Id", "dbo.TGRatingCalculationModes");
            DropForeignKey("dbo.TGTestSettings", "TGCountOfTaskMode_Id", "dbo.TGCountOfTaskModes");
            DropForeignKey("dbo.TGTestSettings", "TasksMixMode_Id", "dbo.TGMixModes");
            DropForeignKey("dbo.TGTestSettings", "AnswersMixMode_Id", "dbo.TGMixModes");
            DropForeignKey("dbo.ThemeContents", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeContentLinks", "ThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.PersonalThemeContentLinks", "ThemeRun_Id", "dbo.ThemeRuns");
            DropForeignKey("dbo.ThemeRuns", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.TestRuns", "ThemeRun_Id", "dbo.ThemeRuns");
            DropForeignKey("dbo.TestRuns", "Test_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.QuestionRuns", "TestRun_Id", "dbo.TestRuns");
            DropForeignKey("dbo.QuestionRuns", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.LectureRuns", "ThemeRun_Id", "dbo.ThemeRuns");
            DropForeignKey("dbo.ParagraphRuns", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.ParagraphRuns", "LectureRun_Id", "dbo.LectureRuns");
            DropForeignKey("dbo.LectureRuns", "Lecture_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.Pictures", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.Paragraphs", "Lecture_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.ThemeRuns", "CourseRun_Id", "dbo.CourseRuns");
            DropForeignKey("dbo.PersonalThemeContentLinks", "ThemeContentLink_Id", "dbo.ThemeContentLinks");
            DropForeignKey("dbo.ThemeContentLinks", "ParentThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.ThemeContentLinks", "LinkedThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.LinkEditorCoordinates", "ThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.ThemeLinks", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.PersonalThemeLinks", "ThemeLink_Id", "dbo.ThemeLinks");
            DropForeignKey("dbo.PersonalThemeLinks", "CourseRun_Id", "dbo.CourseRuns");
            DropForeignKey("dbo.ThemeLinks", "ParentTheme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeLinks", "LinkedTheme_Id", "dbo.Themes");
            DropForeignKey("dbo.LinkEditorCoordinates", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.Themes", "Course_Id", "dbo.Courses");
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.GameAchievementRuns", new[] { "GameAchievementId" });
            DropIndex("dbo.GameAchievementRuns", new[] { "UserId" });
            DropIndex("dbo.TGTestSettings", new[] { "TasksMixMode_Id" });
            DropIndex("dbo.TGTestSettings", new[] { "AnswersMixMode_Id" });
            DropIndex("dbo.TGTestSettings", new[] { "TGRatingCalculationMode_Id" });
            DropIndex("dbo.TGTestSettings", new[] { "TGCountOfTaskMode_Id" });
            DropIndex("dbo.QuestionRuns", new[] { "Question_Id" });
            DropIndex("dbo.QuestionRuns", new[] { "TestRun_Id" });
            DropIndex("dbo.TestRuns", new[] { "Test_Id" });
            DropIndex("dbo.TestRuns", new[] { "ThemeRun_Id" });
            DropIndex("dbo.ParagraphRuns", new[] { "Paragraph_Id" });
            DropIndex("dbo.ParagraphRuns", new[] { "LectureRun_Id" });
            DropIndex("dbo.Pictures", new[] { "Paragraph_Id" });
            DropIndex("dbo.Paragraphs", new[] { "Lecture_Id" });
            DropIndex("dbo.LectureRuns", new[] { "Lecture_Id" });
            DropIndex("dbo.LectureRuns", new[] { "ThemeRun_Id" });
            DropIndex("dbo.ThemeRuns", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeRuns", new[] { "CourseRun_Id" });
            DropIndex("dbo.PersonalThemeContentLinks", new[] { "ThemeRun_Id" });
            DropIndex("dbo.PersonalThemeContentLinks", new[] { "ThemeContentLink_Id" });
            DropIndex("dbo.ThemeContentLinks", new[] { "ThemeContent_Id" });
            DropIndex("dbo.ThemeContentLinks", new[] { "LinkedThemeContent_Id" });
            DropIndex("dbo.ThemeContentLinks", new[] { "ParentThemeContent_Id" });
            DropIndex("dbo.PersonalThemeLinks", new[] { "CourseRun_Id" });
            DropIndex("dbo.PersonalThemeLinks", new[] { "ThemeLink_Id" });
            DropIndex("dbo.ThemeLinks", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeLinks", new[] { "LinkedTheme_Id" });
            DropIndex("dbo.ThemeLinks", new[] { "ParentTheme_Id" });
            DropIndex("dbo.Themes", new[] { "Course_Id" });
            DropIndex("dbo.CourseRuns", new[] { "Course_Id" });
            DropIndex("dbo.CourseRuns", new[] { "User_Id" });
            DropIndex("dbo.Users", new[] { "UserGroup_Id" });
            DropIndex("dbo.LinkEditorCoordinates", new[] { "ThemeContent_Id" });
            DropIndex("dbo.LinkEditorCoordinates", new[] { "Theme_Id" });
            DropIndex("dbo.LinkEditorCoordinates", new[] { "User_Id" });
            DropIndex("dbo.ThemeContents", new[] { "TGTestSetting_Id" });
            DropIndex("dbo.ThemeContents", new[] { "Theme_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.AnswerVariants", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "AnswerVariant_Id" });
            DropIndex("dbo.Answers", new[] { "TestRun_Id" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.UserGroups");
            DropTable("dbo.GameAchievements");
            DropTable("dbo.GameAchievementRuns");
            DropTable("dbo.EDucationAuthors");
            DropTable("dbo.Awards");
            DropTable("dbo.Roles");
            DropTable("dbo.TGRatingCalculationModes");
            DropTable("dbo.TGCountOfTaskModes");
            DropTable("dbo.TGMixModes");
            DropTable("dbo.TGTestSettings");
            DropTable("dbo.QuestionRuns");
            DropTable("dbo.TestRuns");
            DropTable("dbo.ParagraphRuns");
            DropTable("dbo.Pictures");
            DropTable("dbo.Paragraphs");
            DropTable("dbo.LectureRuns");
            DropTable("dbo.ThemeRuns");
            DropTable("dbo.PersonalThemeContentLinks");
            DropTable("dbo.ThemeContentLinks");
            DropTable("dbo.PersonalThemeLinks");
            DropTable("dbo.ThemeLinks");
            DropTable("dbo.Themes");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseRuns");
            DropTable("dbo.Users");
            DropTable("dbo.LinkEditorCoordinates");
            DropTable("dbo.ThemeContents");
            DropTable("dbo.Questions");
            DropTable("dbo.AnswerVariants");
            DropTable("dbo.Answers");
            DropTable("dbo.Achievements");
        }
    }
}
