using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
	public class AnswerVariant : EntityBase
	{
        public int OrderNumber { get; set; }
        public string Text { get; set; }
        public bool IfCorrect { get; set; }
        [ForeignKey("Question")] public Guid Question_Id { get; set; }

        public virtual Question Question { get; set; }
    }
}