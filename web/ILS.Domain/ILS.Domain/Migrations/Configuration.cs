namespace ILS.Domain.Migrations
{
    using GameAchievements;
    using System;
    using System.Data.Entity.Migrations;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class Configuration : DbMigrationsConfiguration<ILS.Domain.ILSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        static string CalculateSHA1(string text)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(text);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash;
        }

        protected override void Seed(ILSContext context)
        {
            context.Database.ExecuteSqlCommand(FillDatabaseHelper.ScriptFillTables);   
            
            #region GameAchievements
            var achievementIndex = 1;
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("cc241156-555b-45d2-9c75-74d7fb66f078"),
                Index = achievementIndex++,
                Name = "Это только начало",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 100,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Education,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.CourseProgressAchievementExecutor",
                AdditionalParameters = "courseProgress=25",
                ImagePath = "just_the_beginning.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("9b907c24-65b0-4d5a-8c14-42e83dd3c522"),
                Index = achievementIndex++,
                Name = "Назад дороги нет",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 200,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Education,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.CourseProgressAchievementExecutor",
                AdditionalParameters = "courseProgress=50",
                ImagePath = "no_way_back.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("acaa86e7-4018-40bd-88b9-2790775bff0f"),
                Index = achievementIndex++,
                Name = "Дело сделано",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 500,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Education,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.CourseProgressAchievementExecutor",
                AdditionalParameters = "courseProgress=100",
                ImagePath = "remember_you_are_here_forever.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("84df1662-8803-4183-a0eb-4a17e93201ef"),
                Index = achievementIndex++,
                Name = "Вместе веселее",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 100,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Multiplayer,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldAchievementExecutor",
                ImagePath = "friends_forever.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("5c06dcd1-7e35-4a2a-9256-189ab3e4bf52"),
                Index = achievementIndex++,
                Name = "Отличник",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 500,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Education,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.TopRatingAchievementExecutor",
                AdditionalParameters = "ratingThreshold=20",
                ImagePath = "neeerd.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("54e2f924-443c-4075-931a-7d373ee81cf7"),
                Index = achievementIndex++,
                Name = "Один из лучших",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 750,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Education,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.TopRatingAchievementExecutor",
                AdditionalParameters = "ratingThreshold=10",
                ImagePath = "best_of_us.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("dc016a1e-715c-42e6-97c9-ccd21cd85a82"),
                Index = achievementIndex++,
                Name = "Чемпион",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 1000,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Education,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.TopRatingAchievementExecutor",
                AdditionalParameters = "ratingThreshold=0",
                ImagePath = "special_olympics.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("aaee71d1-7a49-4037-93dd-2b15e21528ef"),
                Index = achievementIndex++,
                Name = "Торопыга",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 100,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Paragraph,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldAchievementExecutor",
                ImagePath = "premature_ejaculator.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("0e9a9e2f-fc60-4046-99dd-02b239b9dd26"),
                Index = achievementIndex++,
                Name = "Идеальное прохождение I",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 250,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Test,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.TestWithoutMistakesAchievementExecutor",
                AdditionalParameters = "testNumber=1",
                ImagePath = "flawless_victory_1.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("69ee7582-e2e1-44c9-ae51-3d5da172b25b"),
                Index = achievementIndex++,
                Name = "Идеальное прохождение II",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 500,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Test,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.TestWithoutMistakesAchievementExecutor",
                AdditionalParameters = "testNumber=5",
                ImagePath = "flawless_victory_2.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("3d71f2ef-2d51-45a8-831c-1dc1437d7677"),
                Index = achievementIndex++,
                Name = "Идеальное прохождение III",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 1000,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Test,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.TestWithoutMistakesAchievementExecutor",
                AdditionalParameters = "testNumber=10",
                ImagePath = "flawless_victory_3.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("519cd50b-3b8a-4608-ae9c-eb323ff4f715"),
                Index = achievementIndex++,
                Name = "Терпеливый слушатель",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 100,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Guide,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldAchievementExecutor",
                ImagePath = "my_dreams_make_more_sense_than_this_anyway.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("fa0a14c0-9cbb-429b-a781-78a376117671"),
                Index = achievementIndex++,
                Name = "Исследователь I",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 100,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Stend,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldNumberAchievementExecutor",
                AdditionalParameters = "number=1",
                ImagePath = "dora_the_explorer_1.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("0b2cc31f-5341-41fe-91e5-8f745067d7c6"),
                Index = achievementIndex++,
                Name = "Исследователь II",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 200,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Stend,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldNumberAchievementExecutor",
                AdditionalParameters = "number=5",
                ImagePath = "dora_the_explorer_2.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("eede382f-93e1-463a-b972-65e11dc4d121"),
                Index = achievementIndex++,
                Name = "Исследователь III",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 300,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Stend,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldNumberAchievementExecutor",
                AdditionalParameters = "number=10",
                ImagePath = "dora_the_explorer_3.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("0946e04d-1009-4f3d-b258-bb87e58d0e00"),
                Index = achievementIndex++,
                Name = "Путешественник I",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 100,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Teleport,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldNumberAchievementExecutor",
                AdditionalParameters = "number=2",
                ImagePath = "forward_to_the_past_1.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("b3bf8e8a-1b6b-4d6c-8938-17c8be00c5f1"),
                Index = achievementIndex++,
                Name = "Путешественник II",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 200,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Teleport,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldNumberAchievementExecutor",
                AdditionalParameters = "number=50",
                ImagePath = "forward_to_the_past_2.png"
            });
            context.GameAchievements.Add(new GameAchievement
            {
                Id = new Guid("053b3b33-1283-4e1f-a011-129c3d50ce56"),
                Index = achievementIndex++,
                Name = "Путешественник III",
                AchievementAwardType = AchievementAwardType.Coins,
                Score = 300,
                Priority = 10,
                AchievementTrigger = AchievementTrigger.Teleport,
                AchievementExecutor = "ILS.Web.GameAchievements.AchievementsExecutors.VirtualWorldNumberAchievementExecutor",
                AdditionalParameters = "number=100",
                ImagePath = "forward_to_the_past_3.png"
            });
            #endregion

            context.Course.Remove(context.Course.Find(new Guid("61137184-8eda-4d36-9cb5-21aba2c911e0"))); //Математика
            context.Course.Remove(context.Course.Find(new Guid("3d0f6b8b-7c10-49c3-bdfe-788a73d839b5"))); //Связанная информатика
            context.Course.Find(new Guid("51b98495-2c83-43d5-aa3f-eb81f8cef8f3")).Name = "Подготовка к ЕГЭ";
        }
    }
}
