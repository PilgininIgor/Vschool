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

        public JsonResult ReadCourses(int page, int start, int limit)
        {
            return Json(new
            {
                success = true,
                courses = context.Course
                .OrderBy(x => x.Name)
                .Skip(start)
                .Take(limit)
                .Select(x => new { name = x.Name, id = x.Id }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadThemes(int page, int start, int limit, string courseId)
        {
            Guid courseIdGuid = (String.IsNullOrEmpty(courseId))? Guid.Empty : new Guid(courseId);
            return Json(new
            {
                success = true,
                courses = context.Theme
                .OrderBy(x => x.OrderNumber)
                .Skip(start)
                .Take(limit)
                .Where(x => courseIdGuid.Equals(Guid.Empty) || x.Course_Id.Equals(courseIdGuid))
                .Select(x => new { name = x.Name, id = x.Id }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourse(Guid id)
        {
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            var c = context.Course.Find(id);
            return Json(new
            {
                name = c.Name,
                childs = c.Themes.OrderBy(x => x.OrderNumber).Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    outputLinks = x.OutputThemeLinks.OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        parentId = y.ParentTheme_Id,
                        linkedId = y.LinkedTheme_Id,
                    }).ToList(),
                    coordinates = x.LinkEditorCoordinates.Where(y => y.User.Equals(u)).OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        x = y.X,
                        y = y.Y,
                    }).ToList()
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTheme(Guid id)
        {
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            var c = context.Theme.Find(id);
            return Json(new
            {
                name = c.Name,
                childs = c.ThemeContents.OrderBy(x => x.OrderNumber).Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    outputLinks = x.OutputThemeContentLinks.OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        parentId = y.ParentThemeContent_Id,
                        linkedId = y.LinkedThemeContent_Id,
                    }),
                    coordinates = x.LinkEditorCoordinates.Where(y => y.User.Equals(u)).OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        x = y.X,
                        y = y.Y,
                    })
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public void SaveCourse(string connections, string coordinates, Guid id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var conns = jss.Deserialize<dynamic>(connections);

            var recievedThemeLinks = new List<ThemeLink>();
            var themeLinksByCourse =
                context.ThemeLink.Where(x => x.ParentTheme.Course_Id.Equals(id));

            if (conns != null)
            {
                for (int i = 0; i < conns.Length; i++)
                {
                    var parentTheme = context.Theme.Find(new Guid(conns[i]["parentLink"]));
                    var linkedTheme = context.Theme.Find(new Guid(conns[i]["linkedLink"]));
                    recievedThemeLinks.Add(new ThemeLink
                    {
                        ParentTheme_Id = parentTheme.Id,
                        LinkedTheme_Id = linkedTheme.Id,
                        ParentTheme = parentTheme,
                        LinkedTheme = linkedTheme
                    });

                    var curThemeLink = recievedThemeLinks[i];
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

            SaveCourseCoordinates(coordinates);

            context.SaveChanges();
        }

        public void SaveTheme(string connections, string coordinates, Guid id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var conns = jss.Deserialize<dynamic>(connections);

            var recievedThemeContentLinks = new List<ThemeContentLink>();
            var themeContentLinksByTheme =
                context.ThemeContentLink.Where(x => x.ParentThemeContent.Theme_Id.Equals(id));

            if (conns != null)
            {
                for (int i = 0; i < conns.Length; i++)
                {
                    var parentThemeContent = context.ThemeContent.Find(new Guid(conns[i]["parentLink"]));
                    var linkedThemeContent = context.ThemeContent.Find(new Guid(conns[i]["linkedLink"]));
                    recievedThemeContentLinks.Add(new ThemeContentLink
                    {
                        ParentThemeContent_Id = parentThemeContent.Id,
                        LinkedThemeContent_Id = linkedThemeContent.Id,
                        ParentThemeContent = parentThemeContent,
                        LinkedThemeContent = linkedThemeContent
                    });

                    var curThemeContentLink = recievedThemeContentLinks[i];
                    if (!themeContentLinksByTheme.Any(x => x.ParentThemeContent_Id == curThemeContentLink.ParentThemeContent_Id
                                                    && x.LinkedThemeContent_Id == curThemeContentLink.LinkedThemeContent_Id))
                    {
                        context.ThemeContentLink.Add(curThemeContentLink);
                        curThemeContentLink.ParentThemeContent.OutputThemeContentLinks.Add(curThemeContentLink);
                    }
                }
            }

            foreach (var themeContentLink in themeContentLinksByTheme)
            {
                if (!recievedThemeContentLinks.Any(x => x.ParentThemeContent_Id == themeContentLink.ParentThemeContent_Id
                                                && x.LinkedThemeContent_Id == themeContentLink.LinkedThemeContent_Id))
                {
                    context.ThemeContentLink.Remove(themeContentLink);
                }
            }

            SaveThemeCoordinates(coordinates);

            context.SaveChanges();
        }

        private void SaveCourseCoordinates(string coordinates)
        {
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            JavaScriptSerializer jss = new JavaScriptSerializer();            
            var coords = jss.Deserialize<dynamic>(coordinates);
            if (coords == null)
            {
                return;
            }
            for (int i = 0; i < coords.Length; i++)
            {
                var theme = context.Theme.Find(new Guid(coords[i]["id"]));
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

        private void SaveThemeCoordinates(string coordinates)
        {
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var coords = jss.Deserialize<dynamic>(coordinates);
            if (coords == null)
            {
                return;
            }
            for (int i = 0; i < coords.Length; i++)
            {
                var themeContent = context.ThemeContent.Find(new Guid(coords[i]["id"]));
                if (!themeContent.LinkEditorCoordinates.Any(x => x.User.Equals(u)))
                {
                    var linkEditorCoordinates = new LinkEditorCoordinates
                    {
                        X = coords[i]["x"],
                        Y = coords[i]["y"],
                        User = u
                    };
                    context.LinkEditorCoordinates.Add(linkEditorCoordinates);
                    themeContent.LinkEditorCoordinates.Add(linkEditorCoordinates);
                }
                else
                {
                    LinkEditorCoordinates currentCoordinates =
                        themeContent.LinkEditorCoordinates.First(x => x.User.Equals(u));
                    currentCoordinates.X = coords[i]["x"];
                    currentCoordinates.Y = coords[i]["y"];
                }
            }
        }
    }
}
