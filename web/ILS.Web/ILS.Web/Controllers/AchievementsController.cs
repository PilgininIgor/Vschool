using System.Collections.Generic;

namespace ILS.Web.Controllers
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using Domain;
    using Domain.GameAchievements;
    using Models;

    [Authorize(Roles = "Admin")]
    public class AchievementsController : Controller
    {
        readonly ILSContext context;

        public AchievementsController(ILSContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void CreateGameAchievement(GameAchievementModel gameAchievementModel)
        {
            context.GameAchievements.Add(new GameAchievement
            {
                AchievementAwardType = (AchievementAwardType)Enum.Parse(typeof(AchievementAwardType), gameAchievementModel.AchievementAwardType),
                AchievementExecutor = gameAchievementModel.AchievementExecutor,
                AchievementTrigger = (AchievementTrigger)Enum.Parse(typeof(AchievementTrigger), gameAchievementModel.AchievementTrigger),
                AdditionalParameters = gameAchievementModel.AdditionalParameters,
                ImagePath = gameAchievementModel.ImagePath,
                Index = gameAchievementModel.Index,
                Message = gameAchievementModel.Message,
                Name = gameAchievementModel.Name,
                Priority = gameAchievementModel.Priority,
                Score = gameAchievementModel.Score,
            });
            context.SaveChanges();
        }

        [HttpPost]
        public void UpdateGameAchievement(GameAchievementModel gameAchievementModel)
        {
            var achievementToUpdate = context.GameAchievements.Find(gameAchievementModel.GameAchievementId);
            achievementToUpdate.AchievementAwardType = (AchievementAwardType)
                    Enum.Parse(typeof (AchievementAwardType), gameAchievementModel.AchievementAwardType);
            achievementToUpdate.Name = gameAchievementModel.Name;
            achievementToUpdate.AchievementTrigger = (AchievementTrigger)
                    Enum.Parse(typeof(AchievementTrigger), gameAchievementModel.AchievementTrigger);
            achievementToUpdate.AchievementExecutor = gameAchievementModel.AchievementExecutor;
            achievementToUpdate.AdditionalParameters = gameAchievementModel.AdditionalParameters;
            achievementToUpdate.ImagePath = gameAchievementModel.ImagePath;
            achievementToUpdate.Index = gameAchievementModel.Index;
            achievementToUpdate.Message = gameAchievementModel.Message;
            achievementToUpdate.Priority = gameAchievementModel.Priority;
            achievementToUpdate.Score = gameAchievementModel.Score;
            context.SaveChanges();
        }

        [HttpPost]
        public void DeleteGameAchievement(GameAchievementModel gameAchievementModel)
        {
            var achievementToDelete = context.GameAchievements.Find(gameAchievementModel.GameAchievementId);
            context.GameAchievements.Remove(achievementToDelete);
            context.SaveChanges();
        }

        [HttpGet]
        public JsonResult GetGameAchievementsList()
        {
            var achievements = context.GameAchievements.OrderBy(achievement => achievement.Index);
            var achievementModelsList = new List<GameAchievementModel>(achievements.Count());

            foreach (var achievement in achievements)
            {
                achievementModelsList.Add(new GameAchievementModel
                {
                    GameAchievementId = achievement.Id,
                    AchievementAwardType = GetAchievementAwardTypeName(achievement.AchievementAwardType, true),
                    Name = achievement.Name,
                    AchievementExecutor = achievement.AchievementExecutor,
                    AdditionalParameters = achievement.AdditionalParameters,
                    AchievementTrigger = GetAchievementTriggerName(achievement.AchievementTrigger, true),
                    ImagePath = achievement.ImagePath,
                    Index = achievement.Index,
                    Message = achievement.Message,
                    Priority = achievement.Priority,
                    Score = achievement.Score
                });
            }

            return Json(new
            {
                data = achievementModelsList
            }, JsonRequestBehavior.AllowGet);
        }

        public void UploadAchievementImage()
        {
            var httpContext = System.Web.HttpContext.Current;
            var fileUpload = httpContext.Request.Files["Image"];

            fileUpload.SaveAs(Server.MapPath(Url.Content("~/Content/Sprites/profile/achievement/")) + fileUpload.FileName);

            Image.GetThumbnailImageAbort myCallback = () => DisableAsyncSupport;
            var fullBitmap = new Bitmap(fileUpload.InputStream);
            var thumbnail = fullBitmap.GetThumbnailImage(90, 90, myCallback, IntPtr.Zero);

            thumbnail.Save(Server.MapPath(Url.Content("~/Content/Sprites/profile/achievement/"))
                + fileUpload.FileName.Substring(0, fileUpload.FileName.IndexOf(".", StringComparison.Ordinal) + 1) + "jpg",
                System.Drawing.Imaging.ImageFormat.Jpeg);

            httpContext.Response.Write(new JavaScriptSerializer().Serialize(Json(new
            {
                success = "true"
            }, JsonRequestBehavior.AllowGet).Data));
        }

        private static string GetAchievementTriggerName(AchievementTrigger achievementTrigger, bool isRussian)
        {
            switch (achievementTrigger)
            {
                case AchievementTrigger.Game:
                    return "Игра";
                case AchievementTrigger.Test:
                    return "Тест";
                case AchievementTrigger.Lecture:
                    return "Лекция";
                case AchievementTrigger.Theme:
                    return "Тема";
                case AchievementTrigger.Course:
                    return "Курс";
                default:
                    return "";
            }
        }

        private static string GetAchievementAwardTypeName(AchievementAwardType achievementAwardType, bool isRussian)
        {
            switch (achievementAwardType)
            {
                case AchievementAwardType.Coins:
                    return "Монеты";
                case AchievementAwardType.Rating:
                    return "Рейтинг";
                default:
                    return "";
            }
        }
    }
}
