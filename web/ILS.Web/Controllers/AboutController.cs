using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILS.Domain;
using ILS.Models;
using System.Web.Routing;

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

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getAuthors()
        {
            List<EDucationAuthor> list = Enumerable.ToList<EDucationAuthor>(context.EDucationAuthor);
            List<EDucationAuthor> authors = new List<EDucationAuthor>();
            putAuthor(list, authors, "Зеленко Лариса Сергеевна");
            putAuthor(list, authors, "Загуменнов Дмитрий");
            putAuthor(list, authors, "Зинченко Алексей");
            putAuthor(list, authors, "Белов Константин");
            putAuthor(list, authors, "Петрухин Иван");
            putAuthor(list, authors, "Халитов Ильдар");
            putAuthor(list, authors, "Григорьев Александр");
            putAuthor(list, authors, "Иванов Виталий");
            putAuthor(list, authors, "Конопелькин Дмитрий");
            putAuthor(list, authors, "Оськина Наталья");
            putAuthor(list, authors, "Семенов Александр");
            putAuthor(list, authors, "Пученков Евгений");
            authors.AddRange(list);
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
    }
}
