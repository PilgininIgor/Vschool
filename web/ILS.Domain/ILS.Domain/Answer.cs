using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public class Answer : EntityBase
	{
        public float TimeSpent { get; set; }
        [ForeignKey("TestRun")] public Guid TestRun_Id { get; set; }
        [ForeignKey("AnswerVariant")] public Guid? AnswerVariant_Id { get; set; }

        public virtual TestRun TestRun { get; set; }
        public virtual AnswerVariant AnswerVariant { get; set; }
    }
}
