using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILS.Domain;
using ILS.Models;
using System.Web.Routing;
using System.IO;

namespace ILS.Web.Controllers
{
    public class AboutController : Controller
    {

        ILSContext context;
		public AboutController(ILSContext context)
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

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getAuthors()
        {
            List<EDucationAuthor> authors = 
                Enumerable.OrderBy<EDucationAuthor, int>(context.EDucationAuthor, x => x.Priority).ToList();
            return Json(new
            {
                authors
            }, JsonRequestBehavior.AllowGet);
        }

        private void putAuthor(List<EDucationAuthor> src, List<EDucationAuthor> list, String name)
        {
            list.Add(src.Single<EDucationAuthor>(x => x.Name.Equals(name)));
            src.RemoveAll(x => x.Name.Equals(name));
        }

        public JsonResult getAwards()
        {
            List<Award> awards = Enumerable.ToList<Award>(context.Award);
            return Json(new
            {
                awards
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAchievements()
        {
            List<Achievement> achievements = Enumerable.ToList<Achievement>(context.Achievement);
            return Json(new
            {
                achievements
            }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getScreenshots()
        {
            List<String> list = Directory.GetFiles(Server.MapPath(Url.Content("~/Content/Sprites/screens/full"))).ToList<String>();
            List<String> screenshots = new List<string>();
            foreach (String item in list)
            {
                screenshots.Add(item.Substring(item.IndexOf("Content")).Replace("\\", "/").Replace("full/", ""));
            }
            return Json(new
            {
                screenshots
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
