namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System.Collections.Generic;
    using Domain.GameAchievements;

    public interface IAchievementExecutor
    {
        /// <summary>
        /// See required parameters for each AchievementExecutor       
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>added GameAchievementRun or null</returns>
        GameAchievementRun Run(Dictionary<string, object> parameters); 
    }
}
