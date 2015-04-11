namespace ILS.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    ﻿using System.Collections.Generic;

    public class QuestionRun : EntityBase
    {
        public double TimeSpent { get; set; }
        [ForeignKey("TestRun")]
        public Guid TestRun_Id { get; set; }
        [ForeignKey("Question")]
        public Guid? Question_Id { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public TestRun TestRun { get; set; }
        public Question Question { get; set; }

        public QuestionRun()
        {
            Answers = new List<Answer>();
        }
    }
}
