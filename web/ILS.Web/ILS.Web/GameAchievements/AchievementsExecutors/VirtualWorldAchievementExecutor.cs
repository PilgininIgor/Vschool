namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.GameAchievements;

    public class VirtualWorldAchievementExecutor : IAchievementExecutor
    {
        private readonly ILSContext context;

        public VirtualWorldAchievementExecutor(ILSContext context)
        {
             this.context = context;
        }

        /// <summary>
        /// Required parameters: gameAchievementId : Guid
        /// </summary>
        public GameAchievementRun Run(User user, Dictionary<string, object> parameters)
        {
            var achievementId = (Guid)parameters[AchievementsConstants.GameAchievementIdParamName];

            if (!context.GameAchievementRuns.Any(x => x.UserId == user.Id && x.GameAchievementId == achievementId))
            {
                var addedGameAchievementRun = context.GameAchievementRuns.Add(
                    new GameAchievementRun { User = user, GameAchievement = context.GameAchievements.Find(achievementId), Result = 1, Passed = true, NeedToShow = true });
                context.SaveChanges();
                return addedGameAchievementRun;
            }

            var gameAchievementRun = context.GameAchievementRuns.First(x => x.UserId == user.Id && x.GameAchievementId == achievementId);
            gameAchievementRun.NeedToShow = false;
            context.SaveChanges();
            return gameAchievementRun;
        }
    }
}