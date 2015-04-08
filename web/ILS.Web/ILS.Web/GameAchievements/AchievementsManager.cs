using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ILS.Domain;
using ILS.Domain.GameAchievements;

namespace ILS.Web.GameAchievements
{
    public class AchievementsManager
    {
        public void ExecuteAchievement(AchievementTrigger trigger, Dictionary<string, object> parameters)
        {
            ILSContext context = new ILSContext();

            foreach (var achievement in context.GameAchievements.Where(x => trigger.Equals(x.AchievementTrigger)).OrderBy(x => x.Priority))
            {
                Type type = Type.GetType(achievement.AchievementExecutor, false, true);
                ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
                IAchievementExecutor achievementExecutor = (IAchievementExecutor)constructorInfo.Invoke(new object[] { });
                achievementExecutor.Run(parameters);
            }
        }
    }
}