using System.Collections.Generic;
using System.Linq;
using System.Web;
using ILS.Domain;
using ILS.Domain.GameAchievements;

namespace ILS.Web.GameAchievements
{
    public class VirtualWordAchievementExecutor : IAchievementExecutor
    {
        public void Run(Dictionary<string, object> parameters)
        {
            string achievementName = parameters[AchievementsConstants.GameAchievementParamName] as string;
            //TODO parse it

            ILSContext context = new ILSContext();
            User user = context.User.First(x => x.Name == HttpContext.Current.User.Identity.Name);
            if (context.GameAchievementRuns.Any(x => x.User.Equals(user)))
            {
                //TODO
                context.GameAchievementRuns.Add(new GameAchievementRun {User = user, GameAchievement = null});
            }
        }
    }
}