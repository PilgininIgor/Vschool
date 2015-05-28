using System.Collections.Generic;
using System.Linq;
using ILS.Domain;
using ILS.Domain.GameAchievements;
using ILS.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class RenderTest : BaseTest
    {
        private readonly User user;
        private readonly Course course;
        private readonly List<GameAchievement> achievements;

        public RenderTest()
        {
            user = new User
            {
                Name = "tester",
                FirstName = "Test",
                LastName = "Testerov",
                Email = "test@test.com",
                Coins = 50
            };
            var lecture = new Lecture
            {
                Name = "First Lecture",
                Text = "some text",
                Paragraphs = new List<Paragraph>
                {
                    new Paragraph
                    {
                        Header = "header",
                        OrderNumber = 1,
                        Text = "some another text"
                    }
                }
            };
            course = new Course 
            {
                Name = "Information",
                Themes = new List<Theme>
                {
                    new Theme
                    {
                        Name = "First",
                        OrderNumber = 1,
                        ThemeContents = new List<ThemeContent>
                        {
                            lecture
                        }
                    }
                }
            };
            achievements = new List<GameAchievement>
            {
                new GameAchievement
                {
                    Name = "First",
                    AchievementAwardType = AchievementAwardType.Coins,
                    Message = "Msg1",
                    Score = 10
                },
                new GameAchievement
                {
                    Name = "Second",
                    AchievementAwardType = AchievementAwardType.Coins,
                    Message = "Msg2",
                    Score = 25
                }
            };
        }
        protected override void AddMockData(ILSContext context)
        {
            context.GameAchievements.AddRange(achievements);
            context.Course.Add(course);
            context.User.Add(user);
        }

        [TestMethod]
        public void TestGetProfile()
        {
            var controller = CreateController<RenderController>();
            dynamic data = controller.GetProfile(user.Name).Data;
            var model = data.model;
            Assert.AreEqual(user.FirstName + " " + user.LastName, model.Name);
            Assert.AreEqual(user.Email, model.Email);
            Assert.AreEqual(user.Coins, model.Money);
        }

        [TestMethod]
        public void TestGetGameAchievementsForUnity()
        {
            var controller = CreateController<RenderController>();
            dynamic data = controller.GetGameAchievementsForUnity().Data;
            for (int i = 0; i < achievements.Count; i++)
            {
                Assert.AreEqual(achievements[i].Name, data[i].name);
            }
        }

        [TestMethod]
        public void TestUnityList()
        {
            var controller = CreateController<RenderController>();
            dynamic data = controller.UnityList().Data;
            Assert.AreEqual(course.Name, data.coursesNames[0].name);
        }
    }
}
