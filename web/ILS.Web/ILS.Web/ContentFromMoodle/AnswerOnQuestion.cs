using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class AnswerOnQuestion
    {
        public AnswerOnQuestion(string text, bool rightly)
        {
            this.rightly = rightly;
            this.text = text;
        }

        public string text { get; set; }
        public bool rightly { get; set; }
    }
}