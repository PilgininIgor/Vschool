using System;
using System.Collections.Generic;
using ILS.Domain;
using ILS.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LinkEditorTest : BaseTest
    {
        private readonly User user;
        private readonly Course course;

        public LinkEditorTest()
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
            var test = new Test
            {
                Name = "First Test",
                Questions = new List<Question>
                {
                    new Question
                    {
                        OrderNumber = 1,
                        Text = "some another text",                       
                    }
                }
            };
            course = new Course 
            {
                Id = Guid.NewGuid(),
                Name = "Information",
                Themes = new List<Theme>
                {
                    new Theme
                    {
                        Name = "First",
                        OrderNumber = 1,
                        ThemeContents = new List<ThemeContent>
                        {
                            lecture,
                            test
                        }
                    },
                    new Theme
                    {
                        Name = "Second",
                        OrderNumber = 2
                    }
                }
            };
         
        }
        protected override void AddMockData(ILSContext context)
        {
            context.Course.Add(course);
            context.User.Add(user);
        }

        [TestMethod]
        public void TestReadCourses()
        {
            var controller = CreateController<LinkEditorController>();
            dynamic data = controller.ReadCourses(1, 0, 10).Data;
            Assert.IsTrue(data.success);
            Assert.AreEqual(course.Name, data.courses[0].name);
        }

        [TestMethod]
        public void TestReadThemes()
        {
            var controller = CreateController<LinkEditorController>();
            dynamic data = controller.ReadThemes(1, 0, 10, course.Id.ToString()).Data;
            Assert.IsTrue(data.success);
            var i = 0;
            foreach (var theme in course.Themes)
            {
                Assert.AreEqual(theme.Name, data.courses[i++].name);
            }
        }
    }
}
