using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
    public class QuestionRun : EntityBase
    {
        public double TimeSpent { get; set; }
        [ForeignKey("TestRun")] public Guid TestRun_Id { get; set; }
        [ForeignKey("Question")] public Guid? Question_Id { get; set; }

        public virtual TestRun TestRun { get; set; }
        public virtual Question Question { get; set; }
    }
}
