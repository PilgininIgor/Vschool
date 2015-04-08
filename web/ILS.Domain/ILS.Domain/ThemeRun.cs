using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
    public class ThemeRun : EntityBase
    {
        public double Progress { get; set; }
        public double TimeSpent { get; set; }
        public int TestsComplete { get; set; }
        public bool AllLectures { get; set; }
        public bool AllTests { get; set; }
        public bool AllTestsMax { get; set; }
        public bool CompleteAll { get; set; }
        [ForeignKey("CourseRun")] public Guid CourseRun_Id { get; set; }
        [ForeignKey("Theme")] public Guid? Theme_Id { get; set; }

        public virtual CourseRun CourseRun { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual ICollection<TestRun> TestsRuns { get; set; }
        public virtual ICollection<LectureRun> LecturesRuns { get; set; }

        public ThemeRun()
        {
            TestsRuns = new List<TestRun>();
            LecturesRuns = new List<LectureRun>();
        }               
    }
}
