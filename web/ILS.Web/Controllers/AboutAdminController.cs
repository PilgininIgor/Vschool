using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ILS.Domain;
using System.Web.Script.Serialization;
using System.IO;
using System.Drawing;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AboutAdminController : Controller
    {

		ILSContext context;
		public AboutAdminController(ILSContext context)
		{
			this.context = context;
		}

        public ActionResult Index()
        {
			return View();
        }

        public JsonResult ReadEDucationAuthor()
        {
            JsonResult jr =  (JsonResult)EDucationAuthorsList();
            
            return jr;
        }



        public JsonResult CreateEDucationAuthor(string name, string description, string image, string priority)
        {
            List<EDucationAuthor> list = Enumerable.Where<EDucationAuthor>(context.EDucationAuthor, x => x.Name.Equals(name)).ToList();
            
            if (list.Count != 0)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            EDucationAuthor selectedEDucationAuthor = new EDucationAuthor();
            selectedEDucationAuthor.Name = name;
            selectedEDucationAuthor.Description = description;
            selectedEDucationAuthor.Image = image;
            selectedEDucationAuthor.Priority = int.Parse(priority);
            context.EDucationAuthor.Add(selectedEDucationAuthor);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult DeleteEDucationAuthor(string name)
        {
            List<EDucationAuthor> list = Enumerable.Where<EDucationAuthor>(context.EDucationAuthor, x => x.Name.Equals(name)).ToList();

            if (list.Count != 1)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            EDucationAuthor author = list[0];
            context.EDucationAuthor.Remove(author);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EDucationAuthorsList()
        {
            List<EDucationAuthor> jsonList = Enumerable.ToList<EDucationAuthor>(context.EDucationAuthor);
            return Json(new
            {
                jsonList
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EDucationAuthorProfile(string name)
        {
            var query = from author in context.EDucationAuthor
                        where author.Name == name
                        select new
                        {
                            Name = author.Name,
                            Priority = author.Priority,
                            Image = author.Image,
                            Description = author.Description
                        };

            JsonResult jr = new JsonResult();

            jr.Data = query;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        public JsonResult UpdateProfile(string name, string description, string image, string priority)
        {
            EDucationAuthor selectedEDucationAuthor = Enumerable.Single<EDucationAuthor>(context.EDucationAuthor, x => x.Name == name);
            if (selectedEDucationAuthor == null)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            selectedEDucationAuthor.Description = description;
            selectedEDucationAuthor.Image = image;
            selectedEDucationAuthor.Priority = int.Parse(priority);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
  
        }

        public void UploadImage()
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile fileupload = context.Request.Files["Image"];

            fileupload.SaveAs(Server.MapPath(Url.Content("~/Content/Sprites/authors/")) + fileupload.FileName);

            context.Response.Write(new JavaScriptSerializer().Serialize(Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet).Data));
        }






        public JsonResult ReadScreen()
        {
            List<String> list = Directory.GetFiles(Server.MapPath(Url.Content("~/Content/Sprites/screens/full"))).ToList<String>();
            List<ScreenModel> screenshots = new List<ScreenModel>();
            foreach (String item in list)
            {
                screenshots.Add(new ScreenModel(item.Substring(item.IndexOf("screens") + 8).Replace("\\", "/").Replace("full/", "")));
            }
            return Json(new
            {
                screenshots
            }, JsonRequestBehavior.AllowGet);
        }
              

        [HttpPost]
        public JsonResult DeleteScreen(string image)
        {
            if (!System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Sprites/screens/full/")) + image))
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            
            System.IO.File.Delete(Server.MapPath(Url.Content("~/Content/Sprites/screens/full/")) + image);
            System.IO.File.Delete(Server.MapPath(Url.Content("~/Content/Sprites/screens/")) + image.Substring(0, image.IndexOf(".") + 1) + "jpg");
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        public void UploadScreen()
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile fileupload = context.Request.Files["Image"];
            

            fileupload.SaveAs(Server.MapPath(Url.Content("~/Content/Sprites/screens/full/")) + fileupload.FileName);

            Image.GetThumbnailImageAbort myCallback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap fullBitmap = new Bitmap(fileupload.InputStream);
            Image thumbnail = fullBitmap.GetThumbnailImage(192, 115, myCallback, IntPtr.Zero);

            thumbnail.Save(Server.MapPath(Url.Content("~/Content/Sprites/screens/")) 
                + fileupload.FileName.Substring(0, fileupload.FileName.IndexOf(".") + 1) + "jpg", 
                System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.Write(new JavaScriptSerializer().Serialize(Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet).Data));
        }











        public JsonResult ReadAward()
        {
            JsonResult jr = (JsonResult)AwardsList();

            return jr;
        }



        public JsonResult CreateAward(string name, string description, string image, string priority)
        {
            List<Award> list = Enumerable.Where<Award>(context.Award, x => x.Name.Equals(name)).ToList();

            if (list.Count != 0)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            Award selectedAward = new Award();
            selectedAward.Name = name;
            selectedAward.Description = description;
            selectedAward.Image = image;
            selectedAward.Priority = int.Parse(priority);
            context.Award.Add(selectedAward);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteAward(string name)
        {
            List<Award> list = Enumerable.Where<Award>(context.Award, x => x.Name.Equals(name)).ToList();

            if (list.Count != 1)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            Award award = list[0];
            context.Award.Remove(award);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AwardsList()
        {
            List<Award> jsonList = Enumerable.ToList<Award>(context.Award);
            return Json(new
            {
                jsonList
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AwardProfile(string name)
        {
            var query = from award in context.Award
                        where award.Name == name
                        select new
                        {
                            Name = award.Name,
                            Priority = award.Priority,
                            Image = award.Image,
                            Description = award.Description
                        };

            JsonResult jr = new JsonResult();

            jr.Data = query;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        public JsonResult UpdateAward(string name, string description, string image, string priority)
        {
            Award selectedAward = Enumerable.Single<Award>(context.Award, x => x.Name == name);
            if (selectedAward == null)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            selectedAward.Description = description;
            selectedAward.Image = image;
            selectedAward.Priority = int.Parse(priority);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteAwardImage(string image)
        {
            if (!System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Sprites/awards/full/")) + image))
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }

            System.IO.File.Delete(Server.MapPath(Url.Content("~/Content/Sprites/awards/full/")) + image);
            System.IO.File.Delete(Server.MapPath(Url.Content("~/Content/Sprites/awards/")) + image.Substring(0, image.IndexOf(".") + 1) + "jpg");
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
        }


        public void UploadAwardImage()
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile fileupload = context.Request.Files["Image"];


            fileupload.SaveAs(Server.MapPath(Url.Content("~/Content/Sprites/awards/full/")) + fileupload.FileName);

            Image.GetThumbnailImageAbort myCallback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap fullBitmap = new Bitmap(fileupload.InputStream);
            Image thumbnail = fullBitmap.GetThumbnailImage(133, 190, myCallback, IntPtr.Zero);

            thumbnail.Save(Server.MapPath(Url.Content("~/Content/Sprites/awards/"))
                + fileupload.FileName.Substring(0, fileupload.FileName.IndexOf(".") + 1) + "jpg",
                System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.Write(new JavaScriptSerializer().Serialize(Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet).Data));
        }













        public JsonResult ReadAchievement()
        {
            JsonResult jr = (JsonResult)AchievementsList();

            return jr;
        }



        public JsonResult CreateAchievement(string name, string description, string image, string priority)
        {
            List<Achievement> list = Enumerable.Where<Achievement>(context.Achievement, x => x.Name.Equals(name)).ToList();

            if (list.Count != 0)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            Achievement selectedAchievement = new Achievement();
            selectedAchievement.Name = name;
            selectedAchievement.Description = description;
            selectedAchievement.Image = image;
            selectedAchievement.Priority = int.Parse(priority);
            context.Achievement.Add(selectedAchievement);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteAchievement(string name)
        {
            List<Achievement> list = Enumerable.Where<Achievement>(context.Achievement, x => x.Name.Equals(name)).ToList();

            if (list.Count != 1)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            Achievement achievement = list[0];
            context.Achievement.Remove(achievement);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AchievementsList()
        {
            List<Achievement> jsonList = Enumerable.ToList<Achievement>(context.Achievement);
            return Json(new
            {
                jsonList
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AchievementProfile(string name)
        {
            var query = from achievement in context.Achievement
                        where achievement.Name == name
                        select new
                        {
                            Name = achievement.Name,
                            Priority = achievement.Priority,
                            Image = achievement.Image,
                            Description = achievement.Description
                        };

            JsonResult jr = new JsonResult();

            jr.Data = query;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jr;
        }

        public JsonResult UpdateAchievement(string name, string description, string image, string priority)
        {
            Achievement selectedAchievement = Enumerable.Single<Achievement>(context.Achievement, x => x.Name == name);
            if (selectedAchievement == null)
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }
            selectedAchievement.Description = description;
            selectedAchievement.Image = image;
            selectedAchievement.Priority = int.Parse(priority);
            context.SaveChanges();
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteAchievementImage(string image)
        {
            if (!System.IO.File.Exists(Server.MapPath(Url.Content("~/Content/Sprites/achievements/full/")) + image))
            {
                return Json(new
                {
                    success = "false"
                }, JsonRequestBehavior.AllowGet);
            }

            System.IO.File.Delete(Server.MapPath(Url.Content("~/Content/Sprites/achievements/full/")) + image);
            System.IO.File.Delete(Server.MapPath(Url.Content("~/Content/Sprites/achievements/")) + image.Substring(0, image.IndexOf(".") + 1) + "jpg");
            return Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet);
        }


        public void UploadAchievementImage()
        {
            HttpContext context = System.Web.HttpContext.Current;
            HttpPostedFile fileupload = context.Request.Files["Image"];


            fileupload.SaveAs(Server.MapPath(Url.Content("~/Content/Sprites/achievements/full/")) + fileupload.FileName);

            Image.GetThumbnailImageAbort myCallback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap fullBitmap = new Bitmap(fileupload.InputStream);
            Image thumbnail = fullBitmap.GetThumbnailImage(133, 190, myCallback, IntPtr.Zero);

            thumbnail.Save(Server.MapPath(Url.Content("~/Content/Sprites/achievements/"))
                + fileupload.FileName.Substring(0, fileupload.FileName.IndexOf(".") + 1) + "jpg",
                System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.Write(new JavaScriptSerializer().Serialize(Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet).Data));
        }
    }
}
