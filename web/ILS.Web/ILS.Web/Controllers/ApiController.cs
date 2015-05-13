namespace ILS.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Domain;

    public class ApiController : Controller
    {
        readonly ILSContext context;

        public ApiController(ILSContext context)
        {
            this.context = context;
        }

        public JsonResult GetUsersTestsByDate(int year, int month, int day)
        {
            var userTestRunsByDate = context.TestRun.Where(run => run.TestDateTime.Day 
                == day && run.TestDateTime.Month == month && run.TestDateTime.Year == year).OrderBy(run => run.TestDateTime);

            return Json(userTestRunsByDate.Select(run => new
            {
                UserName = run.ThemeRun.CourseRun.User.Name,
                Date = run.TestDateTime,
                CorrectAnswers = run.Result,
                AllAnswers = run.QuestionsRuns.Count
            }), JsonRequestBehavior.AllowGet);
        }
    }
}
