using System.Collections;

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
            ILSContext context = new ILSContext();

            List<GameAchievementRun> changedAchievementRuns = new List<GameAchievementRun>();
            
            foreach (var achievement in context.GameAchievements.Where(x => trigger.Equals(x.AchievementTrigger)).OrderBy(x => x.Priority))
            {
                Type type = Type.GetType(achievement.AchievementExecutor, false, true);
                ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
                IAchievementExecutor achievementExecutor = (IAchievementExecutor)constructorInfo.Invoke(new object[] { });
                changedAchievementRuns.Add(achievementExecutor.Run(parameters));
            }

            return changedAchievementRuns;
        }
    }
}