using System.Collections.Generic;

namespace ILS.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuestionRun : EntityBase
    {
        public double TimeSpent { get; set; }
        [ForeignKey("TestRun")] public Guid TestRunId { get; set; }
        [ForeignKey("Question")] public Guid? QuestionId { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public TestRun TestRun { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }

        public QuestionRun()
        {
            Answers = new List<Answer>();
        }
    }
}
