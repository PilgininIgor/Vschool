namespace ILS.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TestRun : EntityBase
    {
        public int Result { get; set; }
        [ForeignKey("ThemeRun")] public Guid ThemeRunId { get; set; }
        [ForeignKey("Test")] public Guid? TestId { get; set; }

        public ThemeRun ThemeRun { get; set; }
        public Test Test { get; set; }
        public ICollection<QuestionRun> QuestionsRuns { get; set; }

        public TestRun()
        {
            Answers = new List<Answer>();
            QuestionsRuns = new List<QuestionRun>();
        }
    }
}
