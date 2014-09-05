using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class Test: LectionOrTest
    {
        public Test(long id, string name, List<Question> lQuestion, int countQue)
        {
            this.name = name;
            this.lQuestion = lQuestion;
            this.countQue = countQue;
            type = "test";
            this.id = id;
        }

        public string type { get; set; }
        public string name { get; set; }
        public List<Question> lQuestion { get; set; }
        public int countQue { get; set; }

        public long id { get; set; }

        public string nameLection { get; set; }        
        public List<Paragraph> ListParagraphs { get; set; }
    }
}