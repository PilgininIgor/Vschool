using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
    public class TestRun : EntityBase
    {
        public int Result { get; set; }
        [ForeignKey("ThemeRun")] public Guid ThemeRun_Id { get; set; }
        [ForeignKey("Test")] public Guid? Test_Id { get; set; }

        public virtual ThemeRun ThemeRun { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<QuestionRun> QuestionsRuns { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public TestRun()
        {
            Answers = new List<Answer>();
            QuestionsRuns = new List<QuestionRun>();
        }
    }
}
