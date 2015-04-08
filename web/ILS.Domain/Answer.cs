namespace ILS.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

	public class Answer : EntityBase
	{
        public float TimeSpent { get; set; }
        [ForeignKey("TestRun")] public Guid TestRunId { get; set; }
        [ForeignKey("AnswerVariant")] public Guid? AnswerVariantId { get; set; }

        public virtual TestRun TestRun { get; set; }
        public virtual AnswerVariant AnswerVariant { get; set; }
    }
}
