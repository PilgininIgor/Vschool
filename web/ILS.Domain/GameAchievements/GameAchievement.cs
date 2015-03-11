namespace ILS.Domain.GameAchievements
{
    public class GameAchievement : EntityBase
    {
        public string Name { get; set; }

        public string DisplayMessage { get; set; }

        public string ImagePath { get; set; }

        public int Priority { get; set; }

        public AchievementTrigger AchievementTrigger { get; set; }

        public string AchievementExecutor { get; set; }
    }
}
