using System.Collections;

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
        private readonly AwardManager awardManager;
        private readonly ILSContext context;

        public AchievementsManager(ILSContext context)
        {
            this.context = context;
            awardManager = new AwardManager(context);
        }

        public IEnumerable<GameAchievementRun> ExecuteAchievement(AchievementTrigger trigger, User user, Dictionary<string, object> parameters)
        {
            var result = new List<GameAchievementRun>();
            foreach (var achievement in context.GameAchievements.Where(x => trigger == x.AchievementTrigger).OrderBy(x => x.Priority).ToList())
            {
                result.Add(FindAndExecute(achievement, user, parameters));
            }
            return result;
        }

        private GameAchievementRun FindAndExecute(GameAchievement achievement, User user, Dictionary<string, object> parameters)
        {
            var currentParameters = new Dictionary<string, object>(parameters);
            currentParameters.Add(AchievementsConstants.GameAchievementIdParamName, achievement.Id);

            var type = Type.GetType(achievement.AchievementExecutor, false, true);
            var constructorInfo = type.GetConstructor(new[] { typeof(ILSContext) });
            var achievementExecutor = (IAchievementExecutor)constructorInfo.Invoke(new object[] { context });
            var resultRun = achievementExecutor.Run(user, currentParameters);
            if (resultRun.Passed && resultRun.NeedToShow)
            {
                awardManager.AddAwardForUser(achievement, user);
                context.SaveChanges();
            }
            return resultRun;
        }
    }
}