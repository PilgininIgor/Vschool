using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILS.Domain
{
    public class Paragraph : EntityBase
    {
        public int OrderNumber { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        [ForeignKey("Lecture")] public Guid Lecture_Id { get; set; }

        public virtual Lecture Lecture { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        public Paragraph()
        {
            Pictures = new List<Picture>();
        }
    }
}
