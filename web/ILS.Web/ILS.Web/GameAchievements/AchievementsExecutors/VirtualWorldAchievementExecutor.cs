namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Domain;
    using Domain.GameAchievements;

    public class VirtualWorldAchievementExecutor : IAchievementExecutor
    {
        /// <summary>
        /// Required parameters: gameAchievementId
        /// </summary>
        public GameAchievementRun Run(User user, Dictionary<string, object> parameters)
        {
            var achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);
            var context = new ILSContext();

            if (!context.GameAchievementRuns.Any(x => x.UserId == user.Id && x.GameAchievementId == achievementId))
            {
                return context.GameAchievementRuns.Add(
                    new GameAchievementRun { User = user, GameAchievement = context.GameAchievements.Find(achievementId), Result = 1, Passed = true, NeedToShow = true });
            }

            var gameAchievementRun = context.GameAchievementRuns.First(x => x.UserId == user.Id && x.GameAchievementId == achievementId);
            gameAchievementRun.NeedToShow = false;
            return gameAchievementRun;
        }
    }
}