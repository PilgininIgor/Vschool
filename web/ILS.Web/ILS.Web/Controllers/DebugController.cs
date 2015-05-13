using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILS.Domain;
using ILS.Domain.GameAchievements;
using ILS.Models;
using System.Web.Routing;
using System.IO;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DebugController : Controller
    {

        ILSContext context;
		public DebugController(ILSContext context)
		{
			this.context = context;
		}

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            context.Database.Initialize(false);
            base.Initialize(requestContext);
        }

        public JsonResult CreateTestRuns(String name)
        {
            User u = context.User.FirstOrDefault(x => x.Name == name);
            if (u == null)
                return Json(new
                {
                    success = false,
                    errorMessage = "User with name " + name + " not found"
                });
            return DoCreateTestRuns(u);
        }

        public JsonResult CreateTestRuns()
        {
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.FirstOrDefault(x => x.Name == HttpContext.User.Identity.Name);
            if (u == null)
                return Json(new
                {
                    success = false,
                    errorMessage = "Authenticated user not found not found"
                });
            return DoCreateTestRuns(u);
        }

        private JsonResult DoCreateTestRuns(User u)
        {
            Course course = context.Course.First();
            if (course == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "No courses in DB"
                });
            }
            Theme theme = context.Theme.FirstOrDefault(x => x.Course_Id.Equals(course.Id));
            if (theme == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "No themes in course " + course.Name
                });
            }
            Lecture lecture = (Lecture)context.ThemeContent.FirstOrDefault(x => x.Theme_Id.Equals(theme.Id) && x is Lecture);
            if (lecture == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "No lectures in theme " + theme.Name
                });
            }
            Test test = (Test)context.ThemeContent.FirstOrDefault(x => x.Theme_Id.Equals(theme.Id) && x is Test);
            if (test == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "No tests in theme " + theme.Name
                });
            }
            Paragraph paragraph = context.Paragraph.First(x => x.Lecture_Id.Equals(lecture.Id));
            if (paragraph == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "No paragraphs in lecture " + lecture.Name
                });
            }
            Question question = context.Question.First(x => x.Test_Id.Equals(test.Id));
            if (question == null)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "No questions in test " + test.Name
                });
            }

            CourseRun courseRun = new CourseRun();
            courseRun.Progress = 50;
            courseRun.User = u;
            courseRun.TimeSpent = 100;
            courseRun.Course = course;

            ThemeRun themeRun = new ThemeRun();
            themeRun.Progress = 35;
            themeRun.Theme = theme;
            themeRun.CourseRun = courseRun;

            LectureRun lectureRun = new LectureRun();
            lectureRun.Lecture = lecture;
            lectureRun.TimeSpent = 20;
            lectureRun.ThemeRun = themeRun;

            TestRun testRun = new TestRun();
            testRun.Result = 1;
            testRun.Test = test;
            testRun.ThemeRun = themeRun;

            QuestionRun questionRun = new QuestionRun();
            questionRun.Question = question;
            questionRun.TimeSpent = 10;
            questionRun.TestRun = testRun;

            ParagraphRun paragraphRun = new ParagraphRun();
            paragraphRun.HaveSeen = true;
            paragraphRun.Paragraph = paragraph;
            paragraphRun.LectureRun = lectureRun;

            context.QuestionRun.Add(questionRun);
            context.ThemeRun.Add(themeRun);
            context.TestRun.Add(testRun);
            context.LectureRun.Add(lectureRun);
            context.ParagraphRun.Add(paragraphRun);
            context.CourseRun.Add(courseRun);

            context.SaveChanges();
            return Json(new 
            {
                success = true
            });
        }

        public JsonResult CreateGameAchievements(bool clearTest, bool createRun)
        {
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.FirstOrDefault(x => x.Name == HttpContext.User.Identity.Name);
            if (u == null)
                return Json(new
                {
                    success = false,
                    errorMessage = "Authenticated user not found not found"
                });

            int count = context.GameAchievements.Count();

            if (clearTest && context.GameAchievements.Count(x => x.Name == "Test") > 0)
            {
                context.GameAchievements.RemoveRange(context.GameAchievements.Where(x => x.Name == "Test"));
            }

            
            GameAchievement achievement = new GameAchievement();
            achievement.AchievementExecutor = "VirtualWordAchievementExecutor";
            achievement.ImagePath = "got500money.png";
            achievement.Index = count + 1;
            achievement.Priority = count + 1;
            achievement.Name = "Test";
            context.GameAchievements.Add(achievement);

            if (createRun)
            {
                GameAchievementRun run = new GameAchievementRun();
                run.GameAchievement = achievement;
                run.User = u;
                context.GameAchievementRuns.Add(run);
            }

            context.SaveChanges();
            return Json(new
            {
                success = true
            });
        }
    }
}
