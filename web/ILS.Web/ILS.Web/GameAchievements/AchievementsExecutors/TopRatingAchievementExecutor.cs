namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using Domain.GameAchievements;

    public class TopRatingAchievementExecutor : IAchievementExecutor
    {
        private readonly ILSContext context;

        public TopRatingAchievementExecutor(ILSContext context)
        {
             this.context = context;
        }
        /// <summary>
        /// Required parameters: gameAchievementId, 
        /// </summary>
        public GameAchievementRun Run(User user, Dictionary<string, object> parameters)
        {
            var achievementId = new Guid(parameters[AchievementsConstants.GameAchievementIdParamName] as string);
            return null;
        }
    }
}
