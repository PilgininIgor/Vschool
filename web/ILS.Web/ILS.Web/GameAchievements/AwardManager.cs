using ILS.Domain;
using ILS.Domain.GameAchievements;

namespace ILS.Web.GameAchievements
{
    public class AwardManager
    {
        public void AddAwardForUser(GameAchievement achievement, User user)
        {
            switch (achievement.AchievementAwardType)
            {
                case AchievementAwardType.Coins:
                    user.Coins += achievement.Score;
                    break;
                default: return;
            }
        }
    }
}