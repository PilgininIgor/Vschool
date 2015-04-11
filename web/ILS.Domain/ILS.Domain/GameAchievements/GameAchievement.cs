namespace ILS.Domain.GameAchievements
{
    public class GameAchievement : EntityBase
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Message { get; set; }

        public int Priority { get; set; }

        public int Index { get; set; }

        public AchievementTrigger AchievementTrigger { get; set; }

        public AchievementAwardType AchievementAwardType { get; set; }

        public int Score { get; set; }

        public string AdditionalParameters { get; set; }

        public string AchievementExecutor { get; set; }
    }
}
