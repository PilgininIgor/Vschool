﻿namespace ILS.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TestRun : EntityBase
    {
        public int Result { get; set; }
        [ForeignKey("ThemeRun")] public Guid ThemeRun_Id { get; set; }
        [ForeignKey("Test")] public Guid? Test_Id { get; set; }
        public DateTime TestDateTime { get; set; }

        public virtual ThemeRun ThemeRun { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<QuestionRun> QuestionsRuns { get; set; }

        public TestRun()
        {
            QuestionsRuns = new List<QuestionRun>();
        }
    }
}