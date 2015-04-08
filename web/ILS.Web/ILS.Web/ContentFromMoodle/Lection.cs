using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class Lection: LectionOrTest
    {
        //В конструктор передаем список страниц, образующих лекцию
        public Lection(long id, List<Paragraph> paragraphs, string name)
        {
            this.nameLection = name;
            this.ListParagraphs = paragraphs;
            type = "lecture";
            this.id = id;
        }

        public string type { get; set; }
        
        public long id { get; set; }
        //имя лекции
        public string nameLection { get; set; }
        //Название параграфа
        public List<Paragraph> ListParagraphs { get; set; }
        public string name { get; set; }
        public List<Question> lQuestion { get; set; }
        public int countQue { get; set; }
    }
}