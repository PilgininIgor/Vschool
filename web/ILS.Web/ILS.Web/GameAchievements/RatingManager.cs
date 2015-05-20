using System;
using System.Linq;
using ILS.Domain;

namespace ILS.Web.GameAchievements
{
    public class RatingManager
    {
        private readonly ILSContext context = new ILSContext();
        private User user;

        public void CalculateRatingForUser(User user)
        {
            this.user = user;
            const int paragraphCoefficient = 10;

            foreach (var testRun in context.TestRun.Where(run => run.ThemeRun.CourseRun.User_Id == user.Id))
            {
                var testRating = CalculateTestRating(testRun);
                user.Rating = testRating + paragraphCoefficient *
                             context.ParagraphRun.Count(
                                 run => run.LectureRun.ThemeRun.CourseRun.User_Id == user.Id && run.HaveSeen);
            }
            context.SaveChanges();
        }

        public void CalculateRatingForAll()
        {
            foreach (var u in context.User)
            {
                CalculateRatingForUser(u);
            }
        }

        private int CalculateTestRating(TestRun testRun)
        {
            var rightAnswers = testRun.Result;
            var allAnswers = testRun.QuestionsRuns.Count;
            var timeForTest = testRun.QuestionsRuns.Sum(run => run.TimeSpent);
            const int testDifficulty = 1;
            const int testCoefficient = 1000;

            return (int)(testDifficulty*(testCoefficient * (allAnswers / timeForTest) - Math.Pow(allAnswers - rightAnswers, 2)));
        }
    }
}