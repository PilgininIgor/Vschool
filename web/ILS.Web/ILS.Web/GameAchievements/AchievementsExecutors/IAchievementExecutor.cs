namespace ILS.Web.GameAchievements.AchievementsExecutors
 {
    using System.Collections.Generic;
    using Domain;
    using Domain.GameAchievements;

    public interface IAchievementExecutor
    {
        GameAchievementRun Run(User user, Dictionary<string, object> parameters);
    }
 }