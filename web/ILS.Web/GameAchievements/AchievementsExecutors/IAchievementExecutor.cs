namespace ILS.Web.GameAchievements.AchievementsExecutors
{
    using System.Collections.Generic;
    using ILS.Domain.GameAchievements;

    public interface IAchievementExecutor
    {
        GameAchievementRun Run(Dictionary<string, object> parameters); 
    }
}
