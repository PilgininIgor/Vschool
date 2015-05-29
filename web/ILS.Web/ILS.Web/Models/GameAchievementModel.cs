namespace ILS.Web.Models
{
    using System;

    public class GameAchievementModel
    {
        public Guid GameAchievementId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Message { get; set; }
        public int Index { get; set; }
        public int Priority { get; set; }
        public string AchievementTrigger { get; set; }
        public string AchievementAwardType { get; set; }
        public int Score { get; set; }
        public string AdditionalParameters { get; set; }
        public string AchievementExecutor { get; set; }

        public GameAchievementModel(string name, string imagePath, string message, int index,
            int priority, int score, string additionalParameters, string achievementExecutor)
        {
            Name = name;
            ImagePath = imagePath;
            Message = message;
            Index = index;
            Priority = priority;
            Score = score;
            AdditionalParameters = additionalParameters;
            AchievementExecutor = achievementExecutor;
        }

        public GameAchievementModel()
        {
            //Nothing to do here
        }
    }
}