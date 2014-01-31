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
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
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
                    }),
                    coordinates = x.LinkEditorCoordinates.Where(y => y.User.Equals(u)).OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        x = y.X,
                        y = y.Y,
                    })
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public void SaveCourse(String connections, String coordinates, Guid courseId)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var conns = jss.Deserialize<dynamic>(connections);

            List<ThemeLink> recievedThemeLinks = new List<ThemeLink>();
            IEnumerable<ThemeLink> themeLinksByCourse =
                context.ThemeLink.Where(x => x.ParentTheme.Course_Id.Equals(courseId));

            if (conns != null)
            {
                for (int i = 0; i < conns.Length; i++)
                {
                    Theme parentTheme = context.Theme.Find(new Guid(conns[i]["parentThemeLink"]));
                    Theme linkedTheme = context.Theme.Find(new Guid(conns[i]["linkedThemeLink"]));
                    recievedThemeLinks.Add(new ThemeLink
                    {
                        ParentTheme_Id = parentTheme.Id,
                        LinkedTheme_Id = linkedTheme.Id,
                        ParentTheme = parentTheme,
                        LinkedTheme = linkedTheme
                    });

                    ThemeLink curThemeLink = recievedThemeLinks[i];
                    if (!themeLinksByCourse.Any(x => x.ParentTheme_Id == curThemeLink.ParentTheme_Id
                                                    && x.LinkedTheme_Id == curThemeLink.LinkedTheme_Id))
                    {
                        context.ThemeLink.Add(curThemeLink);
                        curThemeLink.ParentTheme.OutputThemeLinks.Add(curThemeLink);
                    }
                }
            }

            foreach (var themeLink in themeLinksByCourse)
            {
                if (!recievedThemeLinks.Any(x => x.ParentTheme_Id == themeLink.ParentTheme_Id
                                                && x.LinkedTheme_Id == themeLink.LinkedTheme_Id))
                {
                    context.ThemeLink.Remove(themeLink);
                }
            }

            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            var coords = jss.Deserialize<dynamic>(coordinates);
            if (coords != null)
            {
                for (int i = 0; i < coords.Length; i++)
                {
                    Theme theme = context.Theme.Find(new Guid(coords[i]["id"]));
                    if (!theme.LinkEditorCoordinates.Any(x => x.User.Equals(u)))
                    {
                        var linkEditorCoordinates = new LinkEditorCoordinates
                        {
                            X = coords[i]["x"],
                            Y = coords[i]["y"],
                            User = u
                        };
                        context.LinkEditorCoordinates.Add(linkEditorCoordinates);
                        theme.LinkEditorCoordinates.Add(linkEditorCoordinates);
                    }
                    else
                    {
                        LinkEditorCoordinates currentCoordinates =
                            theme.LinkEditorCoordinates.First(x => x.User.Equals(u));
                        currentCoordinates.X = coords[i]["x"];
                        currentCoordinates.Y = coords[i]["y"];
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
