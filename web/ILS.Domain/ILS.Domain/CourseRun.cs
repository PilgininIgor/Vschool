using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
    public class CourseRun : EntityBase
    {
        public double Progress { get; set; }
        public double TimeSpent { get; set; }
        public bool Visisted { get; set; }
        public bool CompleteAll { get; set; }
        [ForeignKey("User")] public Guid User_Id { get; set; }
        [ForeignKey("Course")] public Guid? Course_Id { get; set; }

        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<ThemeRun> ThemesRuns { get; set; }

        public CourseRun()
        {
            ThemesRuns = new List<ThemeRun>();
        }
    }
}
