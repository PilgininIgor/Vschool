namespace ILS.Web.GameAchievements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.GameAchievements;
    using AchievementsExecutors;

    public class AchievementsManager
    {
        private readonly AwardManager awardManager = new AwardManager();
        private readonly ILSContext context = new ILSContext();

        public IEnumerable<GameAchievementRun> ExecuteAchievement(AchievementTrigger trigger, User user, Dictionary<string, object> parameters)
        {
            var context = new ILSContext();

            var achievementIdString = parameters[AchievementsConstants.GameAchievementIdParamName] as string;
            if (!String.IsNullOrEmpty(achievementIdString))
            {
                var achievement = context.GameAchievements.Find(new Guid(achievementIdString));
                if (achievement != null)
                {
                    return new List<GameAchievementRun> {FindAndExecute(achievement, user, parameters)};
                }
            }

            return context.GameAchievements.Where(x => trigger == x.AchievementTrigger).OrderBy(x => x.Priority)
                .Select(achievement => FindAndExecute(achievement, user, parameters)).ToList();
        }

        private GameAchievementRun FindAndExecute(GameAchievement achievement, User user, Dictionary<string, object> parameters)
        {
            var type = Type.GetType(achievement.AchievementExecutor, false, true);
            var constructorInfo = type.GetConstructor(new Type[] { });
            var achievementExecutor = (IAchievementExecutor)constructorInfo.Invoke(new object[] { });
            var resultRun = achievementExecutor.Run(user, parameters);
            if (resultRun.Passed && resultRun.NeedToShow)
            {
                awardManager.AddAwardForUser(achievement, user);
                context.SaveChanges();
            }
            return resultRun;
        }
    }
}