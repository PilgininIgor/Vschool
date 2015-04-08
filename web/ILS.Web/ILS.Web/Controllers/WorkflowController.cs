using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILS.Domain;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class WorkflowController : Controller
    {
		ILSContext context;
        public WorkflowController (ILSContext context)
		{ 
			this.context = context; 
		}

        public ActionResult Index(ILSContext context)
        {
            return View();
        }

		public ActionResult ReadCourses(int page, int start, int limit)
		{
			return Json(new
			{
				success = true,
				courses = context.Course
				.OrderBy(x => x.Id)
				.Skip(start)
				.Take(limit)
				.Select(x => new { name = x.Name, id = x.Id})
			}, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetCourse(Guid id)
		{
			return Content(context.Course.Single(x => x.Id == id).Diagramm);
		}

		public ActionResult SetDiagramm(Guid id, string diagramm)
		{
			var course = context.Course.Single(x => x.Id == id);

			course.Diagramm = diagramm;

			context.SaveChanges();

			return new EmptyResult();
		}
    }
}
