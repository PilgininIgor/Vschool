using ILS.Domain;
using ILS.Domain.GameAchievements;

namespace ILS.Web.GameAchievements
{
    public class AwardManager
    {
        private readonly ILSContext context;

        public AwardManager(ILSContext context)
        {
            this.context = context;
        }

        public void AddAwardForUser(GameAchievement achievement, User user)
        {
            switch (achievement.AchievementAwardType)
            {
                case AchievementAwardType.Coins:
                    user.Coins += achievement.Score;
                    context.SaveChanges();
                    break;
                default: return;
            }
        }
    }
}