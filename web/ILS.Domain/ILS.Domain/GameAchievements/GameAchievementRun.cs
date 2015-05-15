using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain.GameAchievements
{
    public class GameAchievementRun : EntityBase
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("GameAchievement")]
        public Guid GameAchievementId { get; set; }

        public virtual User User { get; set; }
        public virtual GameAchievement GameAchievement { get; set; }
        public int Result { get; set; }
        public bool Passed { get; set; }
        public bool NeedToShow { get; set; }
    }
}
