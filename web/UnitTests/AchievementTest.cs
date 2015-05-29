using System;
using System.Collections.Generic;
using System.Linq;
using ILS.Domain;
using ILS.Domain.GameAchievements;
using ILS.Web.Controllers;
using ILS.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class AchievementTest : BaseTest
    {
        private readonly List<GameAchievement> achievements;

        public AchievementTest()
        {
            achievements = new List<GameAchievement>
            {
                new GameAchievement
                {
                    Index = 0,
                    Name = "First",
                    AchievementAwardType = AchievementAwardType.Coins,
                    Score = 100,
                    Priority = 10,
                    AchievementTrigger = AchievementTrigger.Theme,
                    AchievementExecutor = "STUB",
                    AdditionalParameters = "STUB",
                    ImagePath = "STUB"
                },
                new GameAchievement
                {
                    Index = 1,
                    Name = "Second",
                    AchievementAwardType = AchievementAwardType.Coins,
                    Score = 200,
                    Priority = 20,
                    AchievementTrigger = AchievementTrigger.Test,
                    AchievementExecutor = "STUB2",
                    AdditionalParameters = "STUB2",
                    ImagePath = "STUB2"
                }
            };       
        }
        protected override void AddMockData(ILSContext context)
        {
            context.GameAchievements.AddRange(achievements);
        }

        [TestMethod]
        public void TestCreateGameAchievement()
        {
            var controller = CreateController<AchievementsController>();
            var gameAchievementModel = new GameAchievementModel
            {
                Index = 2,
                Name = "Unique Text 2",
                AchievementAwardType = "2",
                Score = 300,
                Priority = 30,
                AchievementTrigger = "3",
                AchievementExecutor = "STUB4",
                AdditionalParameters = "STUB4",
                ImagePath = "STU4"
            };
            controller.CreateGameAchievement(gameAchievementModel);
            Assert.IsTrue(context.GameAchievements.Any(achievemnt =>
                achievemnt.Name == gameAchievementModel.Name
                && achievemnt.Score == gameAchievementModel.Score
                && achievemnt.AchievementExecutor == gameAchievementModel.AchievementExecutor
                && achievemnt.AdditionalParameters == gameAchievementModel.AdditionalParameters
                && achievemnt.ImagePath == gameAchievementModel.ImagePath));
        }

        [TestMethod]
        public void TestUpdateGameAchievement()
        {
            var controller = CreateController<AchievementsController>();
            var gameAchievementModel = new GameAchievementModel
            {
                GameAchievementId =achievements[0].Id,
                Index = 2,
                Name = "Unique Test 3",
                AchievementAwardType = "1",
                Score = 300,
                Priority = 30,
                AchievementTrigger = "2",
                AchievementExecutor = "STUB3",
                AdditionalParameters = "STUB3",
                ImagePath = "STUB3"
            };
            controller.UpdateGameAchievement(gameAchievementModel);
            Assert.IsTrue(context.GameAchievements.Any(achievemnt =>
                achievemnt.Name == gameAchievementModel.Name
                && achievemnt.Score == gameAchievementModel.Score
                && achievemnt.AchievementExecutor == gameAchievementModel.AchievementExecutor
                && achievemnt.AdditionalParameters == gameAchievementModel.AdditionalParameters
                && achievemnt.ImagePath == gameAchievementModel.ImagePath));
        }
    }
}
