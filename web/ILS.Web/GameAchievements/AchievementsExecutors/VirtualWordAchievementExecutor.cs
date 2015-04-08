﻿namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Domain;
    using Domain.GameAchievements;

    public class VirtualWordAchievementExecutor : IAchievementExecutor
    {
        /// <summary>
        /// Required parameters: gameAchievementId
        /// </summary>
        public GameAchievementRun Run(Dictionary<string, object> parameters)
        {
            Guid achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);

            ILSContext context = new ILSContext();
            User user = context.User.First(x => x.Name == HttpContext.Current.User.Identity.Name);
            if (!context.GameAchievementRuns.Any(x => x.User.Equals(user) && x.GameAchievementId.Equals(achievementId)))
            {
                return context.GameAchievementRuns.Add(
                    new GameAchievementRun { User = user, GameAchievementId = achievementId, Result = 1});
            }

            return null;
        }
    }
}