﻿namespace ILS.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Task2Run : EntityBase
    {
        public int Result { get; set; }
        public int AttemptsNumber { get; set; }
        
        [ForeignKey("ThemeRun")] public Guid ThemeRun_Id { get; set; }
        [ForeignKey("Task2Content")] public Guid? Task2ContentId { get; set; }

        public virtual ThemeRun ThemeRun { get; set; }
        public virtual Task2Content Task2Content { get; set; }
    }
}