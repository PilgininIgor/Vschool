namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System;
    using System.Collections.Generic;
    using Domain.GameAchievements;

    public class TopRatingAchievementExecutor : IAchievementExecutor
    {
        /// <summary>
        /// Required parameters: gameAchievementId, 
        /// </summary>
        public GameAchievementRun Run(Dictionary<string, object> parameters)
        {
            Guid achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);
            return null;
        }
    }
}
