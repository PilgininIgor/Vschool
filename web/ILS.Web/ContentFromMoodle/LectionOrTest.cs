using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public interface LectionOrTest
    {
        long id { get; set; }
        string nameLection { get; set; }
        string type { get; set; }
        List<Paragraph> ListParagraphs { get; set; }
        string name { get; set; }
        List<Question> lQuestion { get; set; }
        int countQue { get; set; }
    }
}