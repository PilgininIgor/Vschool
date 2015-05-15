namespace ILS.Web.GameAchievements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Domain;
    using Domain.GameAchievements;
    using AchievementsExecutors;

    public class AchievementsManager
    {
        public List<GameAchievementRun> ExecuteAchievement(AchievementTrigger trigger, Dictionary<string, object> parameters)
        {
            var context = new ILSContext();

            var changedAchievementRuns = new List<GameAchievementRun>();
            
            foreach (var achievement in context.GameAchievements.Where(x => trigger.Equals(x.AchievementTrigger)).OrderBy(x => x.Priority))
            {
                var type = Type.GetType(achievement.AchievementExecutor, false, true);
                var constructorInfo = type.GetConstructor(new Type[] { });
                var achievementExecutor = (IAchievementExecutor)constructorInfo.Invoke(new object[] { });
                changedAchievementRuns.Add(achievementExecutor.Run(parameters));
            }

            return changedAchievementRuns;
        }
    }
}