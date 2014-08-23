namespace ILS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Baseline : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
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
                "dbo.ThemeContentLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentThemeContent_Id = c.Guid(nullable: false),
                        LinkedThemeContent_Id = c.Guid(),
                        ThemeContent_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeContents", t => t.ParentThemeContent_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeContents", t => t.LinkedThemeContent_Id)
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
                "dbo.TestRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Result = c.Int(nullable: false),
                        ThemeRun_Id = c.Guid(nullable: false),
                        Test_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeRuns", t => t.ThemeRun_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeContents", t => t.Test_Id)
                .Index(t => t.ThemeRun_Id)
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id);
            
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
                "dbo.QuestionRuns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeSpent = c.Double(nullable: false),
                        TestRun_Id = c.Guid(nullable: false),
                        Question_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestRuns", t => t.TestRun_Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.TestRun_Id)
                .Index(t => t.Question_Id);
            
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
                .ForeignKey("dbo.TestRuns", t => t.TestRun_Id, cascadeDelete: true)
                .ForeignKey("dbo.AnswerVariants", t => t.AnswerVariant_Id)
                .Index(t => t.TestRun_Id)
                .Index(t => t.AnswerVariant_Id);
            
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
                .ForeignKey("dbo.ThemeRuns", t => t.ThemeRun_Id, cascadeDelete: true)
                .ForeignKey("dbo.ThemeContents", t => t.Lecture_Id)
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
                "dbo.ThemeLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentTheme_Id = c.Guid(nullable: false),
                        LinkedTheme_Id = c.Guid(),
                        Theme_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Themes", t => t.ParentTheme_Id, cascadeDelete: true)
                .ForeignKey("dbo.Themes", t => t.LinkedTheme_Id)
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
                .ForeignKey("dbo.ThemeLinks", t => t.ThemeLink_Id, cascadeDelete: true)
                .ForeignKey("dbo.CourseRuns", t => t.CourseRun_Id)
                .Index(t => t.ThemeLink_Id)
                .Index(t => t.CourseRun_Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfQuestions",
                c => new
                    {
                        TypeOfQuestionIdentifier = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TypeOfQuestionIdentifier);
            
            CreateTable(
                "dbo.QuestionTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        ClassName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ParameterIdentifier = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        QuestionTemplate_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ParameterIdentifier)
                .ForeignKey("dbo.QuestionTemplates", t => t.QuestionTemplate_Id, cascadeDelete: true)
                .Index(t => t.QuestionTemplate_Id);
            
            CreateTable(
                "dbo.Constraints",
                c => new
                    {
                        ParameterIdentifier = c.String(nullable: false, maxLength: 128),
                        TypeOfConstraint = c.String(nullable: false, maxLength: 128),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ParameterIdentifier, t.TypeOfConstraint, t.Value })
                .ForeignKey("dbo.Parameters", t => t.ParameterIdentifier, cascadeDelete: true)
                .Index(t => t.ParameterIdentifier);
            
            CreateTable(
                "dbo.ParameterValues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParameterIdentifier = c.String(nullable: false, maxLength: 128),
                        Level_Id = c.Guid(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.ParameterIdentifier, t.Level_Id })
                .ForeignKey("dbo.Parameters", t => t.ParameterIdentifier, cascadeDelete: true)
                .ForeignKey("dbo.Levels", t => t.Level_Id, cascadeDelete: true)
                .Index(t => t.ParameterIdentifier)
                .Index(t => t.Level_Id);
            
            CreateTable(
                "dbo.GeneratedTests",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Paragraph_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Paragraphs", t => t.Paragraph_Id, cascadeDelete: true)
                .Index(t => t.Paragraph_Id);
            
            CreateTable(
                "dbo.InstanceOfQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionTemplate_Id = c.Guid(nullable: false),
                        TypeOfQuestionIdentifier = c.String(maxLength: 128),
                        Level_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionTemplates", t => t.QuestionTemplate_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfQuestions", t => t.TypeOfQuestionIdentifier)
                .ForeignKey("dbo.Levels", t => t.Level_Id, cascadeDelete: true)
                .Index(t => t.QuestionTemplate_Id)
                .Index(t => t.TypeOfQuestionIdentifier)
                .Index(t => t.Level_Id);
            
            CreateTable(
                "dbo.TestContents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        GeneratedTest_Id = c.Guid(nullable: false),
                        InstanceOfQuestion_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeneratedTests", t => t.GeneratedTest_Id, cascadeDelete: true)
                .ForeignKey("dbo.InstanceOfQuestions", t => t.InstanceOfQuestion_Id, cascadeDelete: true)
                .Index(t => t.GeneratedTest_Id)
                .Index(t => t.InstanceOfQuestion_Id);
            
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
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.TestContents", new[] { "InstanceOfQuestion_Id" });
            DropIndex("dbo.TestContents", new[] { "GeneratedTest_Id" });
            DropIndex("dbo.InstanceOfQuestions", new[] { "Level_Id" });
            DropIndex("dbo.InstanceOfQuestions", new[] { "TypeOfQuestionIdentifier" });
            DropIndex("dbo.InstanceOfQuestions", new[] { "QuestionTemplate_Id" });
            DropIndex("dbo.GeneratedTests", new[] { "Paragraph_Id" });
            DropIndex("dbo.ParameterValues", new[] { "Level_Id" });
            DropIndex("dbo.ParameterValues", new[] { "ParameterIdentifier" });
            DropIndex("dbo.Constraints", new[] { "ParameterIdentifier" });
            DropIndex("dbo.Parameters", new[] { "QuestionTemplate_Id" });
            DropIndex("dbo.PersonalThemeLinks", new[] { "CourseRun_Id" });
            DropIndex("dbo.PersonalThemeLinks", new[] { "ThemeLink_Id" });
            DropIndex("dbo.ThemeLinks", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeLinks", new[] { "LinkedTheme_Id" });
            DropIndex("dbo.ThemeLinks", new[] { "ParentTheme_Id" });
            DropIndex("dbo.ParagraphRuns", new[] { "Paragraph_Id" });
            DropIndex("dbo.ParagraphRuns", new[] { "LectureRun_Id" });
            DropIndex("dbo.Pictures", new[] { "Paragraph_Id" });
            DropIndex("dbo.Paragraphs", new[] { "Lecture_Id" });
            DropIndex("dbo.LectureRuns", new[] { "Lecture_Id" });
            DropIndex("dbo.LectureRuns", new[] { "ThemeRun_Id" });
            DropIndex("dbo.Answers", new[] { "AnswerVariant_Id" });
            DropIndex("dbo.Answers", new[] { "TestRun_Id" });
            DropIndex("dbo.QuestionRuns", new[] { "Question_Id" });
            DropIndex("dbo.QuestionRuns", new[] { "TestRun_Id" });
            DropIndex("dbo.AnswerVariants", new[] { "Question_Id" });
            DropIndex("dbo.Questions", new[] { "Test_Id" });
            DropIndex("dbo.ThemeContents", new[] { "Theme_Id" });
            DropIndex("dbo.TestRuns", new[] { "Test_Id" });
            DropIndex("dbo.TestRuns", new[] { "ThemeRun_Id" });
            DropIndex("dbo.ThemeRuns", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeRuns", new[] { "CourseRun_Id" });
            DropIndex("dbo.PersonalThemeContentLinks", new[] { "ThemeRun_Id" });
            DropIndex("dbo.PersonalThemeContentLinks", new[] { "ThemeContentLink_Id" });
            DropIndex("dbo.ThemeContentLinks", new[] { "ThemeContent_Id" });
            DropIndex("dbo.ThemeContentLinks", new[] { "LinkedThemeContent_Id" });
            DropIndex("dbo.ThemeContentLinks", new[] { "ParentThemeContent_Id" });
            DropIndex("dbo.Themes", new[] { "Course_Id" });
            DropIndex("dbo.CourseRuns", new[] { "Course_Id" });
            DropIndex("dbo.CourseRuns", new[] { "User_Id" });
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.TestContents", "InstanceOfQuestion_Id", "dbo.InstanceOfQuestions");
            DropForeignKey("dbo.TestContents", "GeneratedTest_Id", "dbo.GeneratedTests");
            DropForeignKey("dbo.InstanceOfQuestions", "Level_Id", "dbo.Levels");
            DropForeignKey("dbo.InstanceOfQuestions", "TypeOfQuestionIdentifier", "dbo.TypeOfQuestions");
            DropForeignKey("dbo.InstanceOfQuestions", "QuestionTemplate_Id", "dbo.QuestionTemplates");
            DropForeignKey("dbo.GeneratedTests", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.ParameterValues", "Level_Id", "dbo.Levels");
            DropForeignKey("dbo.ParameterValues", "ParameterIdentifier", "dbo.Parameters");
            DropForeignKey("dbo.Constraints", "ParameterIdentifier", "dbo.Parameters");
            DropForeignKey("dbo.Parameters", "QuestionTemplate_Id", "dbo.QuestionTemplates");
            DropForeignKey("dbo.PersonalThemeLinks", "CourseRun_Id", "dbo.CourseRuns");
            DropForeignKey("dbo.PersonalThemeLinks", "ThemeLink_Id", "dbo.ThemeLinks");
            DropForeignKey("dbo.ThemeLinks", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeLinks", "LinkedTheme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeLinks", "ParentTheme_Id", "dbo.Themes");
            DropForeignKey("dbo.ParagraphRuns", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.ParagraphRuns", "LectureRun_Id", "dbo.LectureRuns");
            DropForeignKey("dbo.Pictures", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.Paragraphs", "Lecture_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.LectureRuns", "Lecture_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.LectureRuns", "ThemeRun_Id", "dbo.ThemeRuns");
            DropForeignKey("dbo.Answers", "AnswerVariant_Id", "dbo.AnswerVariants");
            DropForeignKey("dbo.Answers", "TestRun_Id", "dbo.TestRuns");
            DropForeignKey("dbo.QuestionRuns", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.QuestionRuns", "TestRun_Id", "dbo.TestRuns");
            DropForeignKey("dbo.AnswerVariants", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Test_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.ThemeContents", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.TestRuns", "Test_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.TestRuns", "ThemeRun_Id", "dbo.ThemeRuns");
            DropForeignKey("dbo.ThemeRuns", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeRuns", "CourseRun_Id", "dbo.CourseRuns");
            DropForeignKey("dbo.PersonalThemeContentLinks", "ThemeRun_Id", "dbo.ThemeRuns");
            DropForeignKey("dbo.PersonalThemeContentLinks", "ThemeContentLink_Id", "dbo.ThemeContentLinks");
            DropForeignKey("dbo.ThemeContentLinks", "ThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.ThemeContentLinks", "LinkedThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.ThemeContentLinks", "ParentThemeContent_Id", "dbo.ThemeContents");
            DropForeignKey("dbo.Themes", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseRuns", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseRuns", "User_Id", "dbo.Users");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.TestContents");
            DropTable("dbo.InstanceOfQuestions");
            DropTable("dbo.GeneratedTests");
            DropTable("dbo.ParameterValues");
            DropTable("dbo.Constraints");
            DropTable("dbo.Parameters");
            DropTable("dbo.QuestionTemplates");
            DropTable("dbo.TypeOfQuestions");
            DropTable("dbo.Levels");
            DropTable("dbo.PersonalThemeLinks");
            DropTable("dbo.ThemeLinks");
            DropTable("dbo.ParagraphRuns");
            DropTable("dbo.Pictures");
            DropTable("dbo.Paragraphs");
            DropTable("dbo.LectureRuns");
            DropTable("dbo.Answers");
            DropTable("dbo.QuestionRuns");
            DropTable("dbo.AnswerVariants");
            DropTable("dbo.Questions");
            DropTable("dbo.ThemeContents");
            DropTable("dbo.TestRuns");
            DropTable("dbo.ThemeRuns");
            DropTable("dbo.PersonalThemeContentLinks");
            DropTable("dbo.ThemeContentLinks");
            DropTable("dbo.Themes");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseRuns");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
        }
    }
}
