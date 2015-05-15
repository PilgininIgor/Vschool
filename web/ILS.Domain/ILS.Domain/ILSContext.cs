using System.Data.Entity;
using ILS.Domain.TestGenerator;
using ILS.Domain.TestGenerator.Settings;
using ILS.Domain.GameAchievements;

namespace ILS.Domain
{
    public class ILSContext : DbContext
    {
        //определяем содержимое базы, создаем модель
        //все дальнейшие операции с ней будем проводить через этот класс
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }

        public DbSet<EDucationAuthor> EDucationAuthor { get; set; }
        public DbSet<Award> Award { get; set; }
        public DbSet<Achievement> Achievement { get; set; }

        public DbSet<Course> Course { get; set; }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<ThemeContent> ThemeContent { get; set; }
        public DbSet<Paragraph> Paragraph { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<AnswerVariant> AnswerVariant { get; set; }

        public DbSet<CourseRun> CourseRun { get; set; }
        public DbSet<ThemeRun> ThemeRun { get; set; }
        public DbSet<TestRun> TestRun { get; set; }
        public DbSet<LectureRun> LectureRun { get; set; }
        public DbSet<ParagraphRun> ParagraphRun { get; set; }
        public DbSet<QuestionRun> QuestionRun { get; set; }
        public DbSet<Answer> Answer { get; set; }

        public DbSet<ThemeLink> ThemeLink { get; set; }
        public DbSet<PersonalThemeLink> PersonalThemeLink { get; set; }
        public DbSet<ThemeContentLink> ThemeContentLink { get; set; }
        public DbSet<PersonalThemeContentLink> PersonalThemeContentLink { get; set; }

        public DbSet<LinkEditorCoordinates> LinkEditorCoordinates { get; set; }

        public DbSet<GameAchievement> GameAchievements { get; set; }
        public DbSet<GameAchievementRun> GameAchievementRuns { get; set; }

        #region ГЕНЕРАТОР ТЕСТОВ
        public DbSet<TGTest> TGTest { get; set; }
        public DbSet<TGTaskTemplate> TGTaskTemplate { get; set; }
        public DbSet<TGTestSetting> TGTestSetting { get; set; }
        public DbSet<TGCountOfTaskMode> TGCountOfTaskMode { get; set; }
        public DbSet<TGMixMode> TGMixMode { get; set; }
        public DbSet<TGRatingCalculationMode> TGRatingCalculationMode { get; set; }
        #endregion

        //имя базы по умолчанию: ILS.Domain.ILSContext. Если Entity Framework не обнаружит ее в СУБД, то попытается создать
        //но masterhost не даст нам программно создать новую базу - у нас есть только одна существующая под названием u273630
        //поэтому мы ее и переименовываем. При локальной разработке и отладке это неважно, но перед загрузкой на хостинг должно быть
        public ILSContext()
            : base("u273630")
        {
            Database.SetInitializer<ILSContext>(new CreateDatabaseIfNotExists<ILSContext>());
        }
    }
}
