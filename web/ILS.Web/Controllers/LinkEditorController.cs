using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ILS.Domain;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class LinkEditorController : Controller
    {
		ILSContext context;
        public LinkEditorController(ILSContext context)
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
                .Select(x => new { name = x.Name, id = x.Id })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourse(Guid id)
        {
            var c = context.Course.Find(id);
            return Json(new
            {
                name = c.Name,
                themes = c.Themes.OrderBy(x => x.OrderNumber).Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    outputThemeLinks = x.OutputThemeLinks.OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        parentThemeId = y.ParentTheme_Id,
                        linkedThemeId = y.LinkedTheme_Id,
                    })
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public void SaveCourse(String connections)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var conns = jss.Deserialize<dynamic>(connections);
            List<ThemeLink> themeLinks = new List<ThemeLink>();
            if (connections != "[0]")
            {
                for (int i = 0; i < conns.Length; i++)
                {
                    Theme parentTheme = context.Theme.Find(new Guid(conns[i]["parentThemeLink"]));
                    Theme linkedTheme = context.Theme.Find(new Guid(conns[i]["linkedThemeLink"]));
                    themeLinks.Add(new ThemeLink
                    {
                        ParentTheme_Id = parentTheme.Id,
                        LinkedTheme_Id = linkedTheme.Id,
                        ParentTheme = parentTheme,
                        LinkedTheme = linkedTheme
                    });

                    ThemeLink curThemeLink = themeLinks[i];
                    if (!context.ThemeLink.Any(x => x.ParentTheme_Id == curThemeLink.ParentTheme_Id
                                                    && x.LinkedTheme_Id == curThemeLink.LinkedTheme_Id))
                    {
                        context.ThemeLink.Add(curThemeLink);
                        curThemeLink.ParentTheme.OutputThemeLinks.Add(curThemeLink);
                    }
                }
                context.SaveChanges();
            }

            foreach (var themeLink in context.ThemeLink)
            {
                if (!themeLinks.Any(x => x.ParentTheme_Id == themeLink.ParentTheme_Id
                                                && x.LinkedTheme_Id == themeLink.LinkedTheme_Id))
                {
                    context.ThemeLink.Remove(themeLink);
                }
            }
            context.SaveChanges();
        }
    }
}
