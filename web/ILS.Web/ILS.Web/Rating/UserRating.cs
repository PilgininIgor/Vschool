namespace ILS.Web.Rating
{
    using Domain;
    using System;
    using System.Linq;

    public class UserRating
    {
        private const double TestMagicConstant = 100;
        private const double ParagraphMagicConstant = 10; 

        private ILSContext context;
        private User user;

        public UserRating(ILSContext context, Guid userId)
        {
            this.context = context;
            user = context.User.Find(userId);
        }

        public int CalculateRating()
        {
            var testRatingSum = 0.0;
            foreach (var test in context.ThemeContent.Where(tc => tc is Test))
            {
                testRatingSum += CalculateRatingForTest(test as Test);
            }

            var rating = (int) (testRatingSum +
                         ParagraphMagicConstant*
                         context.ParagraphRun.Count(run => run.LectureRun.ThemeRun.CourseRun.User_Id == user.Id && run.HaveSeen));

            user.Rating = rating;
            context.SaveChanges();
            return rating;
        }

        private double CalculateRatingForTest(Test test)
        {
            var testRun = context.TestRun.FirstOrDefault(run => run.Test_Id == test.Id && run.ThemeRun.CourseRun.User_Id == user.Id);
            if (testRun == null)
            {
                return 0;
            }
            return 0;
            /*Пришлось закомментить, так как вылетала ошибка из-за того что QuestionRun не используются*/
            /*TestMagicConstant*test.TestDifficulty*
            context.QuestionRun.Where(run => run.TestRun_Id == testRun.Id).Sum(run => run.TimeSpent)/
            test.MaxMinutes*Math.Pow(testRun.Result/(double) test.Questions.Count, 2);*/
        }
    }
}