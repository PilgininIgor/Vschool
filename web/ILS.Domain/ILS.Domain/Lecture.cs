using System.Collections.Generic;

namespace ILS.Domain
{
    public class Lecture : ThemeContent
    {
        public virtual string Text { get; set; }

        public virtual ICollection<Paragraph> Paragraphs { get; set; }

        public Lecture()
        {
            Paragraphs = new List<Paragraph>();
        }
    }
}