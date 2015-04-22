namespace ILS.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Domain;
    using Models;

    [Authorize(Roles = "Admin")]
    public class GameAchievementsController : Controller
    {
        readonly ILSContext context;

        public GameAchievementsController(ILSContext context)
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
        public JsonResult CreateGameAchievement()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public JsonResult UpdateGameAchievement()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public JsonResult DeleteGameAchievement()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public JsonResult GetGameAchievementsList()
        {
            var gameAchievementsList = context.GameAchievements.Select(achievement => new GameAchievementModel
            {
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
            });
        }
    }
}
