﻿namespace ILS.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Task3Run : EntityBase
    {
        public int Result { get; set; }
        public int AttemptsNumber { get; set; }
        
        [ForeignKey("ThemeRun")] public Guid ThemeRun_Id { get; set; }
        [ForeignKey("Task3Content")] public Guid? Task3ContentId { get; set; }

        public virtual ThemeRun ThemeRun { get; set; }
        public virtual Task3Content Task3Content { get; set; }
    }
}