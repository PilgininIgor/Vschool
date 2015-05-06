using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using ILS.Domain;
using ILS.Domain.GameAchievements;
using ILS.Web.Models;

namespace ILS.Web.Controllers
{
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
        public JsonResult ReadGameAchievement()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void CreateGameAchievement(GameAchievementModel gameAchievementModel)
        {
            context.GameAchievements.Add(new GameAchievement
            {
//               AchievementAwardType = gameAchievementModel.AchievementAwardType,
                AchievementExecutor = gameAchievementModel.AchievementExecutor,
//               AchievementTrigger = gameAchievementModel.AchievementTrigger,
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
            achievementToUpdate.Name = gameAchievementModel.Name;
            achievementToUpdate.AchievementExecutor = gameAchievementModel.AchievementExecutor;
            achievementToUpdate.AdditionalParameters = gameAchievementModel.AdditionalParameters;
            achievementToUpdate.ImagePath = gameAchievementModel.ImagePath;
            achievementToUpdate.Index = gameAchievementModel.Priority;
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
            var gameAchievementsList = context.GameAchievements.Select(achievement => new GameAchievementModel
            {
                GameAchievementId = achievement.Id,  
                Name = achievement.Name,
                AchievementExecutor = achievement.AchievementExecutor,
                AdditionalParameters = achievement.AdditionalParameters,
                ImagePath = achievement.ImagePath,
                Index = achievement.Priority,
                Message = achievement.Message,
                Priority = achievement.Priority,
                Score = achievement.Score
            }).ToList();

            return Json(new
            {
                data = gameAchievementsList
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
