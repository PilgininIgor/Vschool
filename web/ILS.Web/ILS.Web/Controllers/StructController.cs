using ILS.Domain;
using ILS.Web.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

//using ziparciv = Ionic.Zip;
//using HtmlAgilityPack;

namespace ILS.Web.Controllers
{
    using ILS.Domain.TestGenerator;
    using ILS.Web.ContentFromMoodle;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects;
    using System.Net;
    using Paragraph = ILS.Domain.Paragraph;
    using Question = ILS.Domain.Question;
    using Test = ILS.Domain.Test;

    [Authorize(Roles = "Admin, Teacher")]
    public class StructController : JsonController
    {
        ILSContext context;

        ObjectContext context_obj;
        ObjectContext contextGenerator_obj;
        ContentFromMoodle.Run r = null;

        private Guid themeId;
        private Theme theme;

        private string physpathToFile_PathsToFilesFromMoodle;

        public StructController(ILSContext context)
        {
            this.context = context;
            this.context_obj = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
            this.contextGenerator_obj = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
        }

        //[Authorize(Roles = "Teacher")]
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Teacher")]
        /*public string AddDoc(Guid id, string type_file, HttpPostedFileBase file)
        {
            if (!ziparciv.ZipFile.IsZipFile(file.InputStream,true)){
                return "{\"success\":false}";
            }
            file.InputStream.Position = 0;
            //var zipFileToRead = file.FileName;
            var p = context.ThemeContent.Find(id);
           // var l = context.ThemeContent.Find(parent_id);
            string path = "/Course_" + p.Theme.Course_Id.ToString();
            path += "/Theme_" + p.Theme_Id.ToString();
            path += "/Lecture_" + p.Theme_Id.ToString();
            string virtpath = HttpContext.Request.Url.ToString().Replace("/Struct/AddDoc", "") + "/Content/pics_base" + path;
            path = Server.MapPath("~/Content/pics_base") + path;

            string extractToFolder = Server.MapPath("~/Content/temp/") + file.FileName.Substring(0, file.FileName.Length - ".zip".Length);

            string folderimages = "";
            string htmFileName = "";
            using (var zip = ziparciv.ZipFile.Read(file.InputStream))   // Read( zipFileToRead))
            {
                foreach (var entry in zip.Entries)
                {
                    entry.Extract(extractToFolder, ziparciv.ExtractExistingFileAction.OverwriteSilently);
                    if (entry.FileName.Substring(entry.FileName.Length - ".htm".Length, ".htm".Length) == ".htm")
                    {
                        htmFileName = extractToFolder + "\\" + entry.FileName;
                        folderimages = entry.FileName.Substring(0,entry.FileName.Length - ".htm".Length) + ".files";
                    }
                }
            }
            
            var l = context.ThemeContent.Find(id);
            int num;
            if (l.Paragraphs.Count == 0) num = 1;
            else num = l.Paragraphs.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            Paragraph par;
          //  l.Paragraphs.Add(par);
         //   context.SaveChanges();
           

            if (type_file == "lecture") {
                HtmlWeb webDoc = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = webDoc.Load(htmFileName);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//p");//SelectNodes(".//img")
                string srcimg = "";
                string text = "";
                string texttemp = "";
                string path_par = "";
                string virtpath_par = "";
                
                string header = "";
                
                Boolean flag = true;
                Picture[] pics = new Picture[50];
                Picture pic = new Picture();
                int picnum=1;
                if (nodes != null)
                {
                    foreach (var tag in nodes)
                    {
                        if (tag.InnerText != "")
                        {
                            if (tag.GetAttributeValue("class", "") == "vwheader")
                            {
                                
                                if (flag) {
                                    flag = false;
                                }
                                else {
                                    par = new Paragraph { OrderNumber = num, Header = header, Text = text };
                                    l.Paragraphs.Add(par);
                                    context.SaveChanges();
                                    num++;
                                    path_par = path+"/Paragraph_" + par.Id.ToString();
                                    virtpath_par = virtpath + "/Paragraph_" + par.Id.ToString();
                                    for (int i = 0 ; i < picnum-1; i++){
                                        Picture picitem=pics[i];
                                        string file_in = extractToFolder+"/"+picitem.Path;
                                        string file_out = path_par+picitem.Path.Replace(folderimages, "");
                                        if (!Directory.Exists(path_par)) Directory.CreateDirectory(path_par); 
                                        System.IO.File.Copy(file_in, file_out,true);
                                        par.Pictures.Add(new Picture { OrderNumber = picitem.OrderNumber, Path = virtpath_par + picitem.Path.Replace(folderimages, "") });
                                    }
                                    context.SaveChanges();
                                    picnum = 1;
                                    
                                    pics.Initialize();
                                }
                                header = tag.InnerText;
                                text = "";
                                //textBox2.AppendText(Environment.NewLine + Environment.NewLine + "                       " + tag.InnerText + Environment.NewLine + Environment.NewLine);
                            }
                            else if (tag.GetAttributeValue("class", "") == "vwparagraf")
                            {
                                if (!flag)
                                {
                                    texttemp = tag.InnerText;
                                    texttemp = texttemp.Replace("&nbsp;", "");
                                    texttemp = texttemp.Replace("\r\n", "");
                                    text = text + texttemp + "\r\n";
                                }
                                //textBox2.AppendText("    " + text + Environment.NewLine);
                            }

                        }
                        HtmlNodeCollection images = tag.SelectNodes(".//img");
                        if (images != null)
                            if (!flag)                                
                            {
                                foreach (var image in images)
                                {
                                
                                    string src = image.GetAttributeValue("src", "");
                                    if (src != "")
                                    {
                                        pics[picnum - 1] = new Picture();
                                        pics[picnum - 1].OrderNumber = picnum;
                                        pics[picnum - 1].Path = src;  
                                        picnum++;
                                    
                                        //srcimg = srcimg + src + Environment.NewLine;
                                    }
                                }
                            }
                        
                    }
                    //textBox2.AppendText("Список изображений этого абзаца:" + Environment.NewLine + srcimg);
                    ////HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    //// doc.Load(htmFileName);
                    //// foreach(HtmlNode link in doc.DocumentElement.SelectNodes("//a[@href]")
                    //// {
                    ////    HtmlAttribute att = link.Attributes["href"];
                    ////    att.Value = FixLink(att);
                    //// }
                    //// doc.Save("file.htm");
                }
                if (header != "")
                {
                    par = new Paragraph { OrderNumber = num, Header = header, Text = text };
                    l.Paragraphs.Add(par);
                    context.SaveChanges();
                    num++;
                    path_par = path + "/Paragraph_" + par.Id.ToString();
                    virtpath_par = virtpath + "/Paragraph_" + par.Id.ToString();
                    for (int i = 0; i < picnum - 1; i++)
                    {
                        Picture picitem = pics[i];
                        string file_in = extractToFolder + "/" + picitem.Path;
                        string file_out = path_par + picitem.Path.Replace(folderimages, "");
                        if (!Directory.Exists(path_par)) Directory.CreateDirectory(path_par);
                        System.IO.File.Copy(file_in, file_out, true);
                        par.Pictures.Add(new Picture { OrderNumber = picitem.OrderNumber, Path = virtpath_par + picitem.Path.Replace(folderimages, "") });
                        context.SaveChanges();
                    }
                    int ljk = 1;
                }
                return "{\"success\":true}";
            }
            else if (type_file == "test") {

                return "{\"success\":true}";
            }
            else return "{\"success\":false}";
            
        }*/

        public ActionResult ReadTree(string node)
        {
            /*Метод вызывается деревом из файла struct.js в двух случаях. Первый - при первоначальной загрузке дерева.
            Тогда id = "treeRoot", и мы возвращаем список курсов. Второй - когда пользователь разворачивает очередную ветку дерева.
            Тогда id = идентификатор курса/темы/лекции/теста (поиском по базе данных мы можем узнать, чего именно),
            и нам нужно вернуть список тем этого курса / лекций и тестов этой темы / параграфов этой лекции / вопросов этого теста*/
            if (node == "treeRoot")
            {
                /*верхний уровень - возвращаем список курсов. Заметьте, что оформлен он именно так, как нужно элементам дерева,
                т.е. есть параметр iconCls, определяющий иконку, текст элемента, а также id, с помощью которого мы,
                в свою очередь, сможем развернуть следующую ветку, когда снова вызовем этот метод*/

                return Json(context.Course.ToList().OrderBy(x => x.Name).Select(x => new
                {
                    iconCls = "course",
                    id = x.Id.ToString(),
                    text = x.Name
                }), JsonRequestBehavior.AllowGet);
            }
            var guid = Guid.Parse(node);
            var course = context.Course.Find(guid);
            if (course != null)
            {
                //уровень курса - возвращаем список тем
                return Json(course.Themes.ToList().OrderBy(x => x.OrderNumber).Select(x => new
                {
                    iconCls = "theme",
                    id = x.Id.ToString(),
                    text = x.Name
                }), JsonRequestBehavior.AllowGet);
            }
            var theme = context.Theme.Find(guid);
            if (theme != null)
            {
                //уровень тем - возвращаем список лекций и тестов
                return Json(theme.ThemeContents.ToList().Where(x => ((x is Lecture) || (x is Test) || (x is Task1Content) || (x is Task2Content) || (x is IslandContent))).OrderBy(x => x.OrderNumber).Select(x => new
                {
                    //iconCls = (x is Lecture) ? "lecture" : "test",
                    iconCls = (x is Lecture) ? "lecture" : (x is Test) ? "test" : (x is Task1Content) ? "tgtasktemplate" : "tgtest",
                    id = x.Id.ToString(),
                    text = x.Name,
                    difficulty = (x is Test)? ((Test)x).TestDifficulty : 0,
                    minutes = (x is Test) ? ((Test)x).MaxMinutes : 0,
                }), JsonRequestBehavior.AllowGet);
            }
            var tc = context.ThemeContent.Find(guid);
            if (tc != null)
            {
                if (tc is Lecture)
                {
                    //уровень лекций - возвращаем список параграфов
                    Lecture lec = (Lecture)tc;
                    return Json(lec.Paragraphs.ToList().OrderBy(x => x.OrderNumber).Select(x => new
                    {
                        iconCls = "paragraph",
                        id = x.Id.ToString(),
                        text = x.Header,
                        leaf = true
                    }), JsonRequestBehavior.AllowGet);
                }
                else if (tc is Test)
                {
                    //уровень тестов - возвращаем список вопросов
                    Test test = (Test)tc;
                    return Json(test.Questions.ToList().OrderBy(x => x.OrderNumber).Select(x => new
                    {
                        iconCls = "question",
                        id = x.Id.ToString(),
                        text = "Вопрос №" + x.OrderNumber,
                        leaf = true
                    }), JsonRequestBehavior.AllowGet);
                }
            }
            return new EmptyResult();
        }

        #region Read-Save
        /*метод, который возвращает информацию о курсе, теме или содержимом темы в зависимости от глубины дерева,
         переданной параметром. Информация однотипная, поэтому я и объединил три разных запроса-ответа в один метод*/
        public ActionResult ReadCTTC(Guid id, int depth)
        {
            if (depth == 1)
            {
                var c = context.Course.Find(id);
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        id = c.Id,
                        name = c.Name,
                        type = "course",
                        ordernumber = ""
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            if (depth == 2)
            {
                var t = context.Theme.Find(id);
                return Json(new
                {
                    success = true,
                    data = new
                    {
                        id = t.Id,
                        name = t.Name,
                        type = "theme",
                        ordernumber = t.OrderNumber
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            var tc = context.ThemeContent.Find(id);
            return Json(new
            {
                success = true,
                data = new
                {
                    id = tc.Id,
                    name = tc.Name,
                    type = (tc is Lecture) ? "lecture" : (tc is Test) ? "test" : "task",
                    ordernumber = tc.OrderNumber,
                    difficulty = (tc is Test) ? ((Test)tc).TestDifficulty : 0,
                    minutes = (tc is Test) ? ((Test)tc).MaxMinutes : 0
                }
            }, JsonRequestBehavior.AllowGet);
        }

        /*метод, сохраняющий изменения в имени указанного курса / темы / содержимого. Самый простенький*/
        public ActionResult SaveCTTC(Guid id, string type, string name)
        {
            if (type == "course") { var c = context.Course.Find(id); c.Name = name; }
            else if (type == "theme") { var t = context.Theme.Find(id); t.Name = name; }
            else { var tc = context.ThemeContent.Find(id); tc.Name = name; }
            context.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult SaveTest(Guid id, string name, int difficulty, int minutes)
        {
            var test = context.ThemeContent.Find(id) as Test;
            test.Name = name;
            test.TestDifficulty = difficulty;
            test.MaxMinutes = minutes;
            context.SaveChanges();
            return Json(new { success = true });
        }

        /*Чтение параграфа. Немного посложнее, т.к. количество картинок заранее неизвестно, и размер JSON-ответа
        получается динамическим. Через словари, возможно, было бы логичнее, но пусть остается так, для разнообразия*/
        public ActionResult ReadParagraph(Guid id)
        {
            var p = context.Paragraph.Find(id);
            JavaScriptSerializer sr = new JavaScriptSerializer();
            string s = "{ \"success\" : true, \"data\" : {";
            s += " \"id\" : \"" + p.Id + "\",";
            s += " \"ordernumber\" : \"" + p.OrderNumber + "\",";
            s += " \"header\" : \"" + p.Header + "\",";
            s += " \"text\" : \"" + p.Text + "\",";
            s += " \"piccount\" : " + p.Pictures.Count;
            foreach (var x in p.Pictures.OrderBy(x => x.OrderNumber))
            {
                s += ", \"pic" + x.OrderNumber + "_path\" : \"" + x.Path + "\"";
            }
            for (var i = p.Pictures.Count + 1; i <= 20; i++)
            {
                s += ", \"pic" + i + "_path\" : \"\"";
            }
            s += "} }";
            object obj = sr.Deserialize<Object>(s);
            return Json(sr.Deserialize<Object>(s), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public string SaveParagraph(Guid id, string header, string text, int piccount)
        {
            var p = context.Paragraph.Find(id);
            p.Header = header;
            p.Text = text;

            string path = "";
            path += "/Course_" + p.Lecture.Theme.Course_Id.ToString();
            path += "/Theme_" + p.Lecture.Theme_Id.ToString();
            path += "/Lecture_" + p.Lecture_Id.ToString();
            path += "/Paragraph_" + p.Id.ToString();
            string physpath = Server.MapPath("~/Content/pics_base") + path;
            string virtpath = HttpContext.Request.Url.ToString().Replace("/Struct/SaveParagraph", "") + "/Content/pics_base" + path;
            if (!Directory.Exists(physpath)) Directory.CreateDirectory(physpath);

            if (p.Pictures.Count > piccount)
            {
                while (p.Pictures.Count > piccount) context_obj.DeleteObject(p.Pictures.OrderBy(x => x.OrderNumber).Last());
            }
            else if (p.Pictures.Count < piccount)
            {
                for (var i = p.Pictures.Count + 1; i <= piccount; i++)
                    p.Pictures.Add(new Picture { OrderNumber = i, Path = null });
            }
            context_obj.SaveChanges();

            foreach (var pic in p.Pictures.OrderBy(x => x.OrderNumber))
            {
                HttpPostedFileBase file = HttpContext.Request.Files["pic" + pic.OrderNumber + "_file"];
                if (file.ContentLength != 0)
                {
                    if (System.IO.File.Exists(pic.Path)) System.IO.File.Delete(pic.Path);
                    file.SaveAs(physpath + "/" + pic.OrderNumber + "_" + file.FileName);
                    pic.Path = virtpath + "/" + pic.OrderNumber + "_" + file.FileName;
                }
            }

            context.SaveChanges();
            return "{\"success\":true}";
        }

        public ActionResult ReadQuestion(string id_s)
        {
            //HttpContext.Request.UserLanguages.Count();
            var c = context.Question.Find(Guid.Parse(id_s));
            return Json(new
            {
                success = true,
                data = new
                {
                    id = c.Id,
                    ordernumber = c.OrderNumber,
                    text = c.Text,
                    picq_path = (c.PicQ != null) ? c.PicQ : "",
                    pica_path = (c.PicA != null) ? c.PicA : "",
                    rb = (c.IfPictured) ? "by_pic" : "by_txt",
                    anscount1 = c.AnswerVariants.Count,
                    anscount2 = c.AnswerVariants.Count,
                    q1_text = (!c.IfPictured) ? c.AnswerVariants.Single(x => x.OrderNumber == 1).Text : "",
                    q2_text = (!c.IfPictured) ? c.AnswerVariants.Single(x => x.OrderNumber == 2).Text : "",
                    q3_text = (!c.IfPictured && (c.AnswerVariants.Count >= 3)) ? c.AnswerVariants.Single(x => x.OrderNumber == 3).Text : "",
                    q4_text = (!c.IfPictured && (c.AnswerVariants.Count >= 4)) ? c.AnswerVariants.Single(x => x.OrderNumber == 4).Text : "",
                    q5_text = (!c.IfPictured && (c.AnswerVariants.Count >= 5)) ? c.AnswerVariants.Single(x => x.OrderNumber == 5).Text : "",
                    q1_stat = (!c.IfPictured) ? c.AnswerVariants.Single(x => x.OrderNumber == 1).IfCorrect : false,
                    q2_stat = (!c.IfPictured) ? c.AnswerVariants.Single(x => x.OrderNumber == 2).IfCorrect : false,
                    q3_stat = (!c.IfPictured && (c.AnswerVariants.Count >= 3)) ? c.AnswerVariants.Single(x => x.OrderNumber == 3).IfCorrect : false,
                    q4_stat = (!c.IfPictured && (c.AnswerVariants.Count >= 4)) ? c.AnswerVariants.Single(x => x.OrderNumber == 4).IfCorrect : false,
                    q5_stat = (!c.IfPictured && (c.AnswerVariants.Count >= 5)) ? c.AnswerVariants.Single(x => x.OrderNumber == 5).IfCorrect : false,
                    avp1 = (c.IfPictured) ? c.AnswerVariants.Single(x => x.OrderNumber == 1).IfCorrect : false,
                    avp2 = (c.IfPictured) ? c.AnswerVariants.Single(x => x.OrderNumber == 2).IfCorrect : false,
                    avp3 = (c.IfPictured && (c.AnswerVariants.Count >= 3)) ? c.AnswerVariants.Single(x => x.OrderNumber == 3).IfCorrect : false,
                    avp4 = (c.IfPictured && (c.AnswerVariants.Count >= 4)) ? c.AnswerVariants.Single(x => x.OrderNumber == 4).IfCorrect : false,
                    avp5 = (c.IfPictured && (c.AnswerVariants.Count >= 5)) ? c.AnswerVariants.Single(x => x.OrderNumber == 5).IfCorrect : false
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public string SaveQuestion(Guid id, string text, string rb, int? anscount1, int? anscount2,
                                         HttpPostedFileWrapper picq_file, HttpPostedFileWrapper pica_file,
                                         string q1_text, string q2_text, string q3_text, string q4_text, string q5_text,
                                         string q1_stat, string q2_stat, string q3_stat, string q4_stat, string q5_stat,
                                         string avp1, string avp2, string avp3, string avp4, string avp5)
        {
            var q = context.Question.Find(id);
            q.Text = text;

            if (picq_file != null)
            {
                string path = "";
                path += "/Course_" + q.Test.Theme.Course_Id.ToString();
                path += "/Theme_" + q.Test.Theme_Id.ToString();
                path += "/Test_" + q.Test_Id.ToString();
                string physpath = Server.MapPath("~/Content/pics_base") + path;
                string virtpath = HttpContext.Request.Url.ToString().Replace("/Struct/SaveQuestion", "") + "/Content/pics_base" + path;
                if (!Directory.Exists(physpath)) Directory.CreateDirectory(physpath);
                if (System.IO.File.Exists(q.PicQ)) System.IO.File.Delete(q.PicQ); //если есть старый, то удаляем его
                physpath += "/" + q.OrderNumber + "q_" + picq_file.FileName;
                virtpath += "/" + q.OrderNumber + "q_" + picq_file.FileName;
                picq_file.SaveAs(physpath);
                q.PicQ = virtpath;
            }

            if (rb == "by_txt")
            {
                q.IfPictured = false; q.PicA = null;
                while (q.AnswerVariants.Count() > 0) context_obj.DeleteObject(q.AnswerVariants.First());
                q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 1, Text = q1_text, Question = q, IfCorrect = (q1_stat != null) ? true : false });
                q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 2, Text = q2_text, Question = q, IfCorrect = (q2_stat != null) ? true : false });
                if (anscount1 >= 3) q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 3, Question = q, Text = q3_text, IfCorrect = (q3_stat != null) ? true : false });
                if (anscount1 >= 4) q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 4, Question = q, Text = q4_text, IfCorrect = (q4_stat != null) ? true : false });
                if (anscount1 == 5) q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 5, Question = q, Text = q5_text, IfCorrect = (q5_stat != null) ? true : false });
            }
            else
            {
                q.IfPictured = true;
                while (q.AnswerVariants.Count() > 0) context_obj.DeleteObject(q.AnswerVariants.First());
                q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 1, Text = null, IfCorrect = (avp1 != null) ? true : false });
                q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 2, Text = null, IfCorrect = (avp2 != null) ? true : false });
                if (anscount2 >= 3) q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 3, Text = null, IfCorrect = (avp3 != null) ? true : false });
                if (anscount2 >= 4) q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 4, Text = null, IfCorrect = (avp4 != null) ? true : false });
                if (anscount2 == 5) q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 5, Text = null, IfCorrect = (avp5 != null) ? true : false });
                if (pica_file != null)
                {
                    string path = "";
                    path += "/Course_" + q.Test.Theme.Course_Id.ToString();
                    path += "/Theme_" + q.Test.Theme_Id.ToString();
                    path += "/Test_" + q.Test_Id.ToString();
                    string physpath = Server.MapPath("~/Content/pics_base") + path;
                    string virtpath = HttpContext.Request.Url.ToString().Replace("/Struct/SaveQuestion", "") + "/Content/pics_base" + path;
                    if (!Directory.Exists(physpath)) Directory.CreateDirectory(physpath);
                    if (System.IO.File.Exists(q.PicA)) System.IO.File.Delete(q.PicA); //если есть старый, то удаляем его
                    physpath += "/" + q.OrderNumber + "a_" + pica_file.FileName;
                    virtpath += "/" + q.OrderNumber + "a_" + pica_file.FileName;
                    pica_file.SaveAs(physpath);
                    q.PicA = virtpath;
                }
            }

            context.SaveChanges(); context_obj.SaveChanges();
            return "{\"success\":true}";
        }

        public ActionResult ReadTask1(string id_s)
        {
            var task = context.ThemeContent.Find(Guid.Parse(id_s)) as Task1Content;
            return Json(new
            {
                success = true,
                data = new
                {
                    Id = task.Id,
                    ordernumber = task.OrderNumber,
                    rb_task = task.Type,
                    scale = (task.Type == "operation") ? task.Scale1.ToString() : "2",
                    operation = (task.Type == "operation") ? task.Operation : "+",
                    number1 = (task.Type == "operation") ? task.Number1 : 10,
                    number2 = (task.Type == "operation") ? task.Number2 : 10,
                    scale1 = (task.Type == "translation") ? task.Scale1.ToString() : "2",
                    scale2 = (task.Type == "translation") ? task.Scale2.ToString() : "10",
                    number = (task.Type == "translation") ? task.Number1 : 10
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTask1(Guid Id, string rb_task, string scale, string operation, int? number1, int? number2, string scale1, string scale2, int? number)
        {
            var task = context.ThemeContent.Find(Id) as Task1Content;
            task.Type = rb_task;
            task.Operation = (rb_task == "operation") ? operation : "";
            task.Number1 = (rb_task == "operation") ? (int)number1 : (int)number;
            task.Number2 = (rb_task == "operation") ? (int)number2 : 0;
            task.Scale1 = (rb_task == "translation") ? int.Parse(scale1) : int.Parse(scale);
            task.Scale2 = (rb_task == "translation") ? int.Parse(scale2) : 0;
            context.SaveChanges();
            return Json(new { success = true });
        }
        
        #endregion

        #region Add-Remove
        /*Дальше идет ряд однотипных методов по добавлению / удалению сущностей. Есть одна тонкость, связанная с порядковыми
        номерами: при добавлении новой сущности нужно найти ее соседа с самым большим номером и сделать плюс один, при
        удалении - сделать минус один всем соседям, номера которых больше номера удаленной сущности*/
        public Guid AddCourse()
        {
            var c = new Course { Name = "Новый курс" };
            context.Course.Add(c);
            context.SaveChanges();
            return c.Id;
        }

        public string RemoveCourse(Guid id)
        {
            Course c = context.Course.Find(id);
            string path = Server.MapPath("~/Content/pics_base") + "/Course_" + c.Id.ToString();
            if (Directory.Exists(path)) Directory.Delete(path, true);
            context_obj.DeleteObject(c);
            context_obj.SaveChanges();
            return "OK";
        }

        public Guid AddTheme(Guid parent_id)
        {
            var c = context.Course.Find(parent_id);
            int ordnmb;
            if (c.Themes.Count == 0) ordnmb = 1;
            else ordnmb = c.Themes.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var t = new Theme
            {
                OrderNumber = ordnmb,
                Name = "Новая тема"
            };
            c.Themes.Add(t);
            context.SaveChanges();
            return t.Id;
        }

        public string RemoveTheme(Guid id, Guid parent_id)
        {
            Theme t = context.Theme.Find(id);
            string path = Server.MapPath("~/Content/pics_base") + "/Course_" + t.Course_Id.ToString() + "/Theme_" + t.Id.ToString();
            if (Directory.Exists(path)) Directory.Delete(path, true);
            RemoveThemeLinks(t);
            context_obj.DeleteObject(t);
            context_obj.SaveChanges();

            int i = 1;
            foreach (var x in context.Course.Find(parent_id).Themes.OrderBy(x => x.OrderNumber))
            {
                x.OrderNumber = i;
                i++;
            }
            context.SaveChanges();
            return "OK";
        }

        public Guid AddLecture(Guid parent_id)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = new Lecture { OrderNumber = num, Name = "Новая лекция" };
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        public Guid AddTest(Guid parent_id)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = new Test { OrderNumber = num, Name = "Новый тест", TestDifficulty = 1, MaxMinutes = 60};
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        public Guid AddTask1(Guid parent_id)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = RandomTask1(num);
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        Task1Content RandomTask1(int num) //Генерация рандомного задания на алгебру логики. Заменить на форму
        {
            Task1Content tc = new Task1Content { OrderNumber = num, Name = "Задание на системы счисления" };
            
            int task, scale, scale2, number1, number2;
            string operation = "";

            System.Random rnd = new System.Random();

            task = rnd.Next(0, 2);
            if (task == 0)
            {
                scale = rnd.Next(0, 3);
                switch (scale)
                {
                    case 0: scale = 2; break;
                    case 1: scale = 8; break;
                    case 2: scale = 16; break;
                    default: break;
                }
                int _operation = rnd.Next(0, 2);
                switch (_operation)
                {
                    case 0: operation = "+"; break;
                    case 1: operation = "-"; break;
                    case 2: operation = "*"; break;
                    default: break;
                }
                number1 = rnd.Next(10, 51);
                do
                {
                    number2 = rnd.Next(10, 51);
                }
                while (number1 == number2);
                if (number2 > number1)
                {
                    int b = number2;
                    number2 = number1;
                    number1 = b;
                }

                tc = new Task1Content
                {
                    OrderNumber = num,
                    Name = "Задание на системы счисления",
                    Type = "operation",
                    Operation = operation,
                    Number1 = number1,
                    Number2 = number2,
                    Scale1 = scale,
                    Scale2 = 0
                };
            }
            else if (task == 1)
            {
                scale = rnd.Next(0, 4);
                switch (scale)
                {
                    case 0: scale = 2; break;
                    case 1: scale = 8; break;
                    case 2: scale = 10; break;
                    case 3: scale = 16; break;
                    default: break;
                }
                do
                {
                    scale2 = rnd.Next(0, 4);
                    switch (scale2)
                    {
                        case 0: scale2 = 2; break;
                        case 1: scale2 = 8; break;
                        case 2: scale2 = 10; break;
                        case 3: scale2 = 16; break;
                        default: break;
                    }
                }
                while (scale == scale2);
                number1 = rnd.Next(10, 51);

                tc = new Task1Content
                {
                    OrderNumber = num,
                    Name = "Задание на системы счисления",
                    Type = "translation",
                    Operation = "",
                    Number1 = number1,
                    Number2 = 0,
                    Scale1 = scale,
                    Scale2 = scale2
                };
            }

            return tc;
        }

        public Guid AddTask2(Guid parent_id)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = RandomTask2(num);
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        Task2Content RandomTask2(int num) //Выбор одного из заданий на алгебру логики. Заменить на форму
        {
            var tasks = new List<string> //список возможных заданий
            {
                "e(d(i(b,b),e(b,1)),i(1,0))",
                "c(c(d(0,b),o(0,0)),c(o(1,0),e(b,0)))",
                "e(c(i(1,b),d(0,b)),i(o(1,0),o(0,0)))",
                "c(e(b,b),c(o(1,0),b))",
                "e(d(c(1,b),b),e(o(1,0),b))"
            };

            System.Random rnd = new System.Random();

            int i = rnd.Next(0, tasks.Count);

            Task2Content tc = new Task2Content
            {
                OrderNumber = num,
                Name = "Задание на алгебру логики",
                TaskString = tasks[i]
            };

            return tc;
        }

        public Guid AddIsland(Guid parent_id)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = new IslandContent { OrderNumber = num, Name = "Локация Остров" };
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        public string RemoveContent(Guid id, Guid parent_id)
        {
            ThemeContent tc = context.ThemeContent.Find(id);

            string path = Server.MapPath("~/Content/pics_base");
            path += "/Course_" + tc.Theme.Course_Id.ToString();
            path += "/Theme_" + tc.Theme_Id.ToString();
            if (tc is Lecture) path += "/Lecture_";
            else if (tc is Test) path += "/Test_";
            else if (tc is Task1Content) path += "/Task1Content_";
            else if (tc is Task2Content) path += "/Task2Content_";
            else if (tc is IslandContent) path += "/IslandContent_";
            path += tc.Id.ToString();
            if (Directory.Exists(path)) Directory.Delete(path, true);
            RemoveThemeContentLinks(tc);

            if (tc is TGTest)
            {
                var testSettings = ((TGTest)tc).TestSetting;
                context_obj.DeleteObject(tc);
                context_obj.DeleteObject(testSettings);
            }
            else
            {
                context_obj.DeleteObject(tc);
            }

            context_obj.SaveChanges();

            int i = 1;
            foreach (var x in context.Theme.Find(parent_id).ThemeContents.OrderBy(x => x.OrderNumber))
            {
                x.OrderNumber = i;
                i++;
            }
            context.SaveChanges();
            return "OK";
        }

        public Guid AddParagraph(Guid parent_id)
        {
            Lecture l = (Lecture)context.ThemeContent.Find(parent_id);
            int num;
            if (l.Paragraphs.Count == 0) num = 1;
            else num = l.Paragraphs.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var p = new Paragraph { OrderNumber = num, Header = "Новый параграф", Text = "Текст параграфа" };
            l.Paragraphs.Add(p);
            context.SaveChanges();
            return p.Id;
        }

        public string RemoveParagraph(Guid id, Guid parent_id)
        {
            Paragraph p = context.Paragraph.Find(id);

            string path = Server.MapPath("~/Content/pics_base");
            path += "/Course_" + p.Lecture.Theme.Course_Id.ToString();
            path += "/Theme_" + p.Lecture.Theme_Id.ToString();
            path += "/Lecture_" + p.Lecture_Id.ToString();
            path += "/Paragraph_" + p.Id.ToString();
            if (Directory.Exists(path)) Directory.Delete(path, true);

            context_obj.DeleteObject(p);
            context_obj.SaveChanges();

            int i = 1;
            foreach (var x in ((Lecture)context.ThemeContent.Find(parent_id)).Paragraphs.OrderBy(x => x.OrderNumber))
            {
                x.OrderNumber = i;
                i++;
            }
            context.SaveChanges();
            return "OK";
        }

        public Guid AddQuestion(Guid parent_id)
        {
            Test t = (Test)context.ThemeContent.Find(parent_id);
            int num;
            if (t.Questions.Count == 0) num = 1;
            else num = t.Questions.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var q = new Question
            {
                OrderNumber = num,
                Text = "Текст вопроса",
                PicQ = null,
                IfPictured = false,
                PicA = null
            };
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 1, Text = "Вариант ответа №1", IfCorrect = false });
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 2, Text = "Вариант ответа №2", IfCorrect = false });
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 3, Text = "Вариант ответа №3", IfCorrect = false });
            t.Questions.Add(q);
            context.SaveChanges();
            return q.Id;
        }

        public string RemoveQuestion(Guid id, Guid parent_id)
        {
            Question q = context.Question.Find(id);
            context_obj.DeleteObject(q);
            context_obj.SaveChanges();

            int i = 1;
            foreach (var x in ((Lecture)context.ThemeContent.Find(parent_id)).Paragraphs.OrderBy(x => x.OrderNumber))
            {
                x.OrderNumber = i;
                i++;
            }
            context.SaveChanges();
            return "OK";
        }


        private void RemoveThemeLinks(Theme theme)
        {
            foreach (ThemeLink tl in context.ThemeLink.Where(x => x.LinkedTheme_Id == theme.Id))
            {
                context_obj.DeleteObject(tl);
            }
        }

        private void RemoveThemeContentLinks(ThemeContent themeContent)
        {
            foreach (ThemeContentLink tcl in context.ThemeContentLink.Where(x => x.LinkedThemeContent_Id == themeContent.Id))
            {
                context_obj.DeleteObject(tcl);
            }
        }


        #endregion

        #region Move
        /*А это два метода, которые меняют местами порядковые номера этой и предыдущей (MoveUp) либо этой и следующей
        (MoveDown) сущностей. В редакторе курсов это пока что единственное средство, с помощью которого можно регулировать
        порядок следования тем и всего прочего, но его должно быть вполне достаточно*/
        public string MoveUp(Guid id, Guid parent_id, int depth, string type)
        {
            if (depth == 2)
            {
                var this_one = context.Theme.Find(id);
                int num = this_one.OrderNumber;
                if (num > 1)
                {
                    var parent = context.Course.Find(parent_id);
                    var prev_one = parent.Themes.Where(x => x.OrderNumber == num - 1).First();
                    this_one.OrderNumber = num - 1;
                    prev_one.OrderNumber = num;
                }
            }
            else if (depth == 3)
            {
                var this_one = context.ThemeContent.Find(id);
                int num = this_one.OrderNumber;
                if (num > 1)
                {
                    var parent = context.Theme.Find(parent_id);
                    var prev_one = parent.ThemeContents.Where(x => x.OrderNumber == num - 1).First();
                    this_one.OrderNumber = num - 1;
                    prev_one.OrderNumber = num;
                }
            }
            else if (type == "paragraph")
            {
                var this_one = context.Paragraph.Find(id);
                int num = this_one.OrderNumber;
                if (num > 1)
                {
                    var parent = context.ThemeContent.Find(parent_id);
                    var prev_one = ((Lecture)parent).Paragraphs.Where(x => x.OrderNumber == num - 1).First();
                    this_one.OrderNumber = num - 1;
                    prev_one.OrderNumber = num;
                }
            }
            else if (type == "question")
            {
                var this_one = context.Question.Find(id);
                int num = this_one.OrderNumber;
                if (num > 1)
                {
                    var parent = context.ThemeContent.Find(parent_id);
                    var prev_one = ((Test)parent).Questions.Where(x => x.OrderNumber == num - 1).First();
                    this_one.OrderNumber = num - 1;
                    prev_one.OrderNumber = num;
                }
            }
            else if (type == "tgtasktemplate")
            {
                var this_one = context.TGTaskTemplate.Find(id);
                int num = this_one.OrderNumber;
                if (num > 1)
                {
                    var parent = context.ThemeContent.Find(parent_id);
                    var prev_one = ((TGTest)parent).TaskTemplates.Where(x => x.OrderNumber == num - 1).First();
                    this_one.OrderNumber = num - 1;
                    prev_one.OrderNumber = num;
                }
            }
            context.SaveChanges();
            return "OK";
        }

        public string MoveDown(Guid id, Guid parent_id, int depth, string type)
        {
            if (depth == 2)
            {
                var this_one = context.Theme.Find(id);
                var parent = context.Course.Find(parent_id);
                int num = this_one.OrderNumber;
                int num_max = parent.Themes.OrderBy(x => x.OrderNumber).Last().OrderNumber;
                if (num < num_max)
                {
                    var next_one = parent.Themes.First(x => x.OrderNumber == num + 1);
                    this_one.OrderNumber = num + 1;
                    next_one.OrderNumber = num;
                }
            }
            else if (depth == 3)
            {
                var this_one = context.ThemeContent.Find(id);
                var parent = context.Theme.Find(parent_id);
                int num = this_one.OrderNumber;
                int num_max = parent.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber;
                if (num < num_max)
                {
                    var next_one = parent.ThemeContents.First(x => x.OrderNumber == num + 1);
                    this_one.OrderNumber = num + 1;
                    next_one.OrderNumber = num;
                }
            }
            else if (type == "paragraph")
            {
                var this_one = context.Paragraph.Find(id);
                var parent = context.ThemeContent.Find(parent_id);
                int num = this_one.OrderNumber;
                int num_max = ((Lecture)parent).Paragraphs.OrderBy(x => x.OrderNumber).Last().OrderNumber;
                if (num < num_max)
                {
                    var next_one = ((Lecture)parent).Paragraphs.First(x => x.OrderNumber == num + 1);
                    this_one.OrderNumber = num + 1;
                    next_one.OrderNumber = num;
                }
            }
            else if (type == "question")
            {
                var this_one = context.Question.Find(id);
                var parent = context.ThemeContent.Find(parent_id);
                int num = this_one.OrderNumber;
                int num_max = ((Test)parent).Questions.OrderBy(x => x.OrderNumber).Last().OrderNumber;
                if (num < num_max)
                {
                    var next_one = ((Test)parent).Questions.First(x => x.OrderNumber == num + 1);
                    this_one.OrderNumber = num + 1;
                    next_one.OrderNumber = num;
                }
            }
            else if (type == "tgtasktemplate")
            {
                var this_one = context.TGTaskTemplate.Find(id);
                var parent = context.ThemeContent.Find(parent_id);
                int num = this_one.OrderNumber;
                int num_max = ((TGTest)parent).TaskTemplates.OrderBy(x => x.OrderNumber).Last().OrderNumber;
                if (num < num_max)
                {
                    var next_one = ((TGTest)parent).TaskTemplates.First(x => x.OrderNumber == num + 1);
                    this_one.OrderNumber = num + 1;
                    next_one.OrderNumber = num;
                }
            }
            context.SaveChanges();
            return "OK";
        }
        #endregion

        #region Moodle

        /*пример работы со словарями
        public ActionResult ReadTest(Guid id)
        {
            var test = context.Test.Single(x => x.Id == id);
            var dict = new Dictionary<string, string>();
            dict["id"] = test.Id.ToString();
            dict["name"] = test.Name;
            dict["trnd"] = test.RandomizeQuestions.ToString().ToLowerInvariant();
            var closedQuestions = test.Questions.OfType<ClosedQuestion>();
            for (int i = 1; i <= closedQuestions.Count(); i++)
            {
                var taskTemplate = closedQuestions.ElementAt(i - 1);
                dict["type" + i] = "text";
                dict["qid" + i] = taskTemplate.Id.ToString();
                dict["question" + i] = taskTemplate.Text;
                dict["qrnd" + i] = taskTemplate.RandomizeAnswers.ToString().ToLowerInvariant();
                for (int j = 1; j <= taskTemplate.Answers.Count; j++)
                {
                    var a = taskTemplate.Answers.ElementAt(j - 1);
                    dict["aid" + i + j] = a.Id.ToString();
                    dict["answer" + i + j] = a.Text;
                    dict["correct" + i + j] = (a.Weight > 0).ToString().ToLowerInvariant();
                }
            }
            return Json(new Dictionary<string, object>() { 
				{"data", dict },
				{"success", "true"}
			}, JsonRequestBehavior.AllowGet);
        }*/

        //Метод добавления контента из LMS Moodle
        public string AddMoodle(Guid id, string DB, string host, string user, string password)
        {
            Guid id_lectOrTest;
            Guid id_ParagraphOrQuestion;
            bool ThemeNoExist = true;
            themeId = id;
            theme = context.Theme.Find(id);

            physpathToFile_PathsToFilesFromMoodle = Server.MapPath("~/Content/DownloadedListFromMoodle");
            if (!Directory.Exists(physpathToFile_PathsToFilesFromMoodle)) Directory.CreateDirectory(physpathToFile_PathsToFilesFromMoodle);

            //если ни разу не делали выгрузку путей к файлам, расположенным на ftp сервере
            if (!System.IO.File.Exists(this.physpathToFile_PathsToFilesFromMoodle + "\\DownloadedListFromMoodle.txt"))
            {
                ContentFromMoodle.GetLisAllFiles.Run();
                List<string> listPathFiles = ContentFromMoodle.GetLisAllFiles.listPathFile;

                physpathToFile_PathsToFilesFromMoodle = Server.MapPath("~/Content/DownloadedListFromMoodle");
                if (!Directory.Exists(physpathToFile_PathsToFilesFromMoodle)) Directory.CreateDirectory(physpathToFile_PathsToFilesFromMoodle);

                FileInfo fi = new FileInfo(physpathToFile_PathsToFilesFromMoodle + "/DownloadedListFromMoodle.txt");
                StreamWriter sw = fi.CreateText();
                foreach (string pathFile in listPathFiles)
                {
                    sw.WriteLine(pathFile);
                }
                sw.Close();
            }

            if (r == null)
                try
                {
                    r = new ContentFromMoodle.Run(DB, host, user, password, physpathToFile_PathsToFilesFromMoodle);
                }
                catch (Exception e)
                {
                    return "{\"success\":false}";
                }

            foreach (var v in r.Lections)
            {
                ThemeNoExist = true;
                for (int i = 0; i < theme.ThemeContents.Count; i++)
                {
                    if (v.nameLection == theme.ThemeContents.ElementAt(i).Name)
                        ThemeNoExist = false;
                }
                if (ThemeNoExist)
                {
                    id_lectOrTest = AddLectureMoodle(id, v.nameLection);
                    foreach (var v1 in v.ListParagraphs)
                    {
                        id_ParagraphOrQuestion = AddParagraphMoodle(id_lectOrTest, v1.nameParagraph, v1.textParagraph);
                        SavePicturesForParagraphMoodle(id_ParagraphOrQuestion, v1.s_pictures, v1.s_name_pictures);
                    }
                }
            }
            foreach (var v in r.Tests)
            {
                ThemeNoExist = true;
                for (int i = 0; i < theme.ThemeContents.Count; i++)
                {
                    if (v.name == theme.ThemeContents.ElementAt(i).Name)
                        ThemeNoExist = false;
                }
                if (ThemeNoExist)
                {
                    id_lectOrTest = AddTestMoodle(id, v.name);
                    foreach (var v1 in v.lQuestion)
                    {
                        id_ParagraphOrQuestion = AddQuestionMoodle(id_lectOrTest, v1.text);
                        SaveQuestionMoodle(id_ParagraphOrQuestion, v1);
                    }
                }
                //}
            }


            string path = "/Course_" + theme.Course.Id.ToString();

            return "{\"success\":true}";
        }


        //Метод формирования путей всех файлов, добавленных в лекции и тесты в системе LMS Moodle
        //[Authorize(Roles = "Teacher")]
        public string MoodleListUpdate(Guid id)
        {
            //return "{\"success\":true}";

            //if (r == null)
            //    r = new ContentFromMoodle.Run("distanceitschool", "localhost", "zhek", "dosSqrt03Den");
            //норм
            //r = new ContentFromMoodle.Run("distanceitschool", "distance.itschool.ssau.ru", "distanceitschool", "zD6ZCZA");

            ContentFromMoodle.GetLisAllFiles.Run();
            List<string> listPathFiles = ContentFromMoodle.GetLisAllFiles.listPathFile;

            physpathToFile_PathsToFilesFromMoodle = Server.MapPath("~/Content/DownloadedListFromMoodle");
            if (!Directory.Exists(physpathToFile_PathsToFilesFromMoodle)) Directory.CreateDirectory(physpathToFile_PathsToFilesFromMoodle);

            FileInfo fi = new FileInfo(physpathToFile_PathsToFilesFromMoodle + "/DownloadedListFromMoodle.txt");
            StreamWriter sw = fi.CreateText();
            //= new StreamWriter(physpath + "/DownloadedListFromMoodle.txt", false, Encoding.UTF8);
            foreach (string pathFile in listPathFiles)
            {
                sw.WriteLine(pathFile);
            }
            sw.Close();

            return "{\"success\":true}";
        }
        public class ContentBaseArray : List<ContentBase>
        {
            public ContentBaseArray()
            {

            }

            public ContentBaseArray(IEnumerable<ContentBase> arr)
            {
                this.AddRange(arr);
            }

        }
        public string ListDownloadedContentFromMoodle(Guid id, string host, string db, string user, string password, string list)
        {
            Guid id_lectOrTest;
            Guid id_ParagraphOrQuestion;
            theme = context.Theme.Find(id);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            // не определён конструктор без параметров ContentBase[] при десериализации массива с одним объектом.
            // вынести в отдельный класс и определить конструктор без параметров.
            var lectionsAndTests = jss.Deserialize<ContentBase[]>(list);
            bool ThemeNoExist = true;

            physpathToFile_PathsToFilesFromMoodle = Server.MapPath("~/Content/DownloadedListFromMoodle");
            if (!Directory.Exists(physpathToFile_PathsToFilesFromMoodle)) Directory.CreateDirectory(physpathToFile_PathsToFilesFromMoodle);

            //если ни разу не делали выгрузку путей к файлам, расположенным на ftp сервере
            if (!System.IO.File.Exists(this.physpathToFile_PathsToFilesFromMoodle + "\\DownloadedListFromMoodle.txt"))
            {
                ContentFromMoodle.GetLisAllFiles.Run();
                List<string> listPathFiles = ContentFromMoodle.GetLisAllFiles.listPathFile;

                physpathToFile_PathsToFilesFromMoodle = Server.MapPath("~/Content/DownloadedListFromMoodle");
                if (!Directory.Exists(physpathToFile_PathsToFilesFromMoodle)) Directory.CreateDirectory(physpathToFile_PathsToFilesFromMoodle);

                FileInfo fi = new FileInfo(physpathToFile_PathsToFilesFromMoodle + "/DownloadedListFromMoodle.txt");
                StreamWriter sw = fi.CreateText();
                foreach (string pathFile in listPathFiles)
                {
                    sw.WriteLine(pathFile);
                }
                sw.Close();
            }

            if (r == null)
                try
                {
                    r = new ContentFromMoodle.Run(db, host, user, password, physpathToFile_PathsToFilesFromMoodle);
                }
                catch (Exception e)
                {
                    return "{\"success\":false}";
                }

            foreach (
                var v in
                    r.Lections.Where(
                        x => lectionsAndTests.Where(e => e.Type == ContentType.Lecture).Select(e => e.Id).Contains(x.id)))
            {
                ThemeNoExist = true;
                for (int i = 0; i < theme.ThemeContents.Count; i++)
                {
                    if (v.nameLection == theme.ThemeContents.ElementAt(i).Name)
                        ThemeNoExist = false;
                }
                if (ThemeNoExist)
                {
                    id_lectOrTest = AddLectureMoodle(id, v.nameLection);
                    foreach (var v1 in v.ListParagraphs)
                    {
                        id_ParagraphOrQuestion = AddParagraphMoodle(id_lectOrTest, v1.nameParagraph, v1.textParagraph);
                        SavePicturesForParagraphMoodle(id_ParagraphOrQuestion, v1.s_pictures, v1.s_name_pictures);
                    }
                }
            }
            foreach (var v in r.Tests.Where(
                        x => lectionsAndTests.Where(e => e.Type == ContentType.Test).Select(e => e.Id).Contains(x.id)))
            {
                ThemeNoExist = true;
                for (int i = 0; i < theme.ThemeContents.Count; i++)
                {
                    if (v.name == theme.ThemeContents.ElementAt(i).Name)
                        ThemeNoExist = false;
                }
                if (ThemeNoExist)
                {
                    id_lectOrTest = AddTestMoodle(id, v.name);
                    foreach (var v1 in v.lQuestion)
                    {
                        id_ParagraphOrQuestion = AddQuestionMoodle(id_lectOrTest, v1.text);
                        SaveQuestionMoodle(id_ParagraphOrQuestion, v1);
                    }
                }
            }

            return "{\"success\":true}";
        }

        public ActionResult GetListFromMoodle(string BD, string host, string login, string password)
        {
            physpathToFile_PathsToFilesFromMoodle = Server.MapPath("~/Content/DownloadedListFromMoodle");

            List<ContentBase> lectionsAndTests = new List<ContentBase>();

            r = null;
            r = new ContentFromMoodle.Run(BD, host, login, password, physpathToFile_PathsToFilesFromMoodle);
            for (int i = 0; i < r.Lections.Count; i++)
            {
                var lection = r.Lections[i];
                lectionsAndTests.Add(new ContentBase(lection.id, lection.nameLection + "(лекция)", ContentType.Lecture));
            }
            for (int i = 0; i < r.Tests.Count; i++)
            {
                var test = r.Tests[i];
                lectionsAndTests.Add(new ContentBase(test.id, test.name + "(тест)", ContentType.Test));
            }

            return Json(lectionsAndTests);
        }

        public Guid AddQuestionMoodle(Guid parent_id, string text)
        {
            var t = context.ThemeContent.Find(parent_id);
            int num;
            if (((Test)t).Questions.Count == 0) num = 1;
            else num = ((Test)t).Questions.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var q = new Question
            {
                OrderNumber = num,
                Text = text,
                PicQ = null,
                IfPictured = false,
                PicA = null
            };
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 1, Text = "Вариант ответа №1", IfCorrect = false });
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 2, Text = "Вариант ответа №2", IfCorrect = false });
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 3, Text = "Вариант ответа №3", IfCorrect = false });
            q.AnswerVariants.Add(new AnswerVariant { OrderNumber = 4, Text = "Вариант ответа №4", IfCorrect = false });
            ((Test)t).Questions.Add(q);
            context.SaveChanges();
            return q.Id;
        }

        public Guid AddParagraphMoodle(Guid parent_id, string name, string text)
        {
            var l = context.ThemeContent.Find(parent_id);
            int num;
            var lecture = l as Lecture;
            if (lecture.Paragraphs.Count == 0) num = 1;
            else num = lecture.Paragraphs.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var p = new Paragraph { OrderNumber = num, Header = name, Text = text };
            lecture.Paragraphs.Add(p);
            context.SaveChanges();
            return p.Id;
        }

        public Guid AddTestMoodle(Guid parent_id, string name)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = new Test { OrderNumber = num, Name = name };
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        public Guid AddLectureMoodle(Guid parent_id, string name)
        {
            var t = context.Theme.Find(parent_id);
            int num;
            if (t.ThemeContents.Count == 0) num = 1;
            else num = t.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var tc = new Lecture { OrderNumber = num, Name = name };
            t.ThemeContents.Add(tc);
            context.SaveChanges();
            return tc.Id;
        }

        [ValidateInput(false)]
        public string SavePicturesForParagraphMoodle(Guid id, string[] pict, string[] name_pict)
        {
            if (pict != null)
            {
                var p = context.Paragraph.Find(id);

                string path = "";
                string error_path = Server.MapPath("~/Content/pics_base");
                path += "/Course_" + p.Lecture.Theme.Course_Id.ToString();
                path += "/Theme_" + p.Lecture.Theme_Id.ToString();
                path += "/Lecture_" + p.Lecture_Id.ToString();
                path += "/Paragraph_" + p.Id.ToString();
                string physpath = Server.MapPath("~/Content/pics_base") + path;
                string virtpath = HttpContext.Request.Url.ToString().Replace("/Struct/addMoodle", "") + "/Content/pics_base" + path;
                if (!Directory.Exists(physpath)) Directory.CreateDirectory(physpath);

                for (var i = p.Pictures.Count + 1; i <= pict.Length; i++)
                {
                    p.Pictures.Add(new Picture { OrderNumber = i, Path = null });
                }

                context_obj.SaveChanges();

                int iz = 0;
                foreach (var pic in p.Pictures.OrderBy(x => x.OrderNumber))
                {
                    WebClient webClient = new WebClient();
                    if (pict[iz].IndexOf("http://") > -1)
                    {

                        try
                        {
                            webClient.DownloadFile(pict[iz], physpath + "/" + pic.OrderNumber + "_" + name_pict[iz]);
                        }
                        catch (Exception e)
                        {
                            //http://lenagold.ru/fon/clipart/s/simb/znak25.jpg
                            webClient.DownloadFile("http://img534.imageshack.us/img534/5839/gmez.jpg",
                                physpath + "/" + pic.OrderNumber + "_" + name_pict[iz]);
                        }
                    }
                    //если путь к изображению - это путь для скачивания с FTP сервера
                    else if (pict[iz].IndexOf("ftp://") > -1)
                    {
                        try
                        {
                            DownloadFileFTP(physpath + "/" + pic.OrderNumber + "_" + name_pict[iz], pict[iz]);
                        }
                        catch (Exception e)
                        {
                            //http://lenagold.ru/fon/clipart/s/simb/znak25.jpg
                            webClient.DownloadFile("http://img534.imageshack.us/img534/5839/gmez.jpg",
                                physpath + "/" + pic.OrderNumber + "_" + name_pict[iz]);
                        }
                    }
                    else
                    {
                        webClient.DownloadFile("http://img534.imageshack.us/img534/5839/gmez.jpg",
                                physpath + "/" + pic.OrderNumber + "_" + name_pict[iz]);
                    }
                    pic.Path = virtpath + "/" + pic.OrderNumber + "_" + name_pict[iz];
                    iz++;
                }

                context.SaveChanges();
            }
            return "{\"success\":true}";
        }

        //Загрузка файла с FTP сервера
        private void DownloadFileFTP(string inputfilepath, string ftphost_Plus_ftpfilepath)
        {
            string ftpfullpath = ftphost_Plus_ftpfilepath;

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential("distanceitschool", "4tXeKbwK");
                byte[] fileData = request.DownloadData(ftpfullpath);

                using (FileStream file = System.IO.File.Create(inputfilepath))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }
            }
        }

        [ValidateInput(false)]
        public string SaveQuestionMoodle(Guid id, ContentFromMoodle.Question question)
        {
            var q = context.Question.Find(id);
            q.Text = question.text;

            if (question.pathToPicture != null)
            {
                string path = "";
                path += "/Course_" + q.Test.Theme.Course_Id.ToString();
                path += "/Theme_" + q.Test.Theme_Id.ToString();
                path += "/Test_" + q.Test_Id.ToString();
                string physpath = Server.MapPath("~/Content/pics_base") + path;
                string virtpath = HttpContext.Request.Url.ToString().Replace("/Struct/addMoodle", "") + "/Content/pics_base" + path;
                if (!Directory.Exists(physpath)) Directory.CreateDirectory(physpath);
                if (System.IO.File.Exists(q.PicQ)) System.IO.File.Delete(q.PicQ); //если есть старый, то удаляем его
                physpath += "/" + q.OrderNumber + "q_" + question.namePicture;
                virtpath += "/" + q.OrderNumber + "q_" + question.namePicture;

                WebClient webClient = new WebClient();
                //webClient.DownloadFile(question.pathToPicture, physpath);

                if (question.pathToPicture.IndexOf("http://") > -1)
                {

                    try
                    {
                        webClient.DownloadFile(question.pathToPicture, physpath);
                    }
                    catch (Exception e)
                    {
                        //http://lenagold.ru/fon/clipart/s/simb/znak25.jpg
                        webClient.DownloadFile("http://img534.imageshack.us/img534/5839/gmez.jpg",
                            physpath);
                    }
                }
                //если путь к изображению - это путь для скачивания с FTP сервера
                else if (question.pathToPicture.IndexOf("ftp://") > -1)
                {
                    try
                    {
                        DownloadFileFTP(physpath, question.pathToPicture);
                    }
                    catch (Exception e)
                    {
                        //http://lenagold.ru/fon/clipart/s/simb/znak25.jpg
                        webClient.DownloadFile("http://img534.imageshack.us/img534/5839/gmez.jpg",
                            physpath);
                    }
                }
                else
                {
                    webClient.DownloadFile("http://img534.imageshack.us/img534/5839/gmez.jpg",
                            physpath);
                }

                q.PicQ = virtpath;
                //taskTemplate.PicA = virtpath;

                q.IfPictured = false;
                while (q.AnswerVariants.Count() > 0) context_obj.DeleteObject(q.AnswerVariants.First());
                int count_var = (question.answersOnQuestion.Count <= 5) ? question.answersOnQuestion.Count : 5;
                for (int i = 0; i < count_var; i++)
                {
                    q.AnswerVariants.Add(new AnswerVariant
                    {
                        OrderNumber = i + 1,
                        Text = question.answersOnQuestion[i].text,
                        IfCorrect = question.answersOnQuestion[i].rightly
                    });
                }
            }
            else
            {
                q.IfPictured = false; q.PicA = null;
                while (q.AnswerVariants.Count() > 0) context_obj.DeleteObject(q.AnswerVariants.First());
                int count_var = (question.answersOnQuestion.Count <= 5) ? question.answersOnQuestion.Count : 5;
                for (int i = 0; i < count_var; i++)
                {
                    q.AnswerVariants.Add(new AnswerVariant
                    {
                        OrderNumber = i + 1,
                        Text = question.answersOnQuestion[i].text,
                        IfCorrect = question.answersOnQuestion[i].rightly
                    });
                }
            }

            context.SaveChanges(); context_obj.SaveChanges();
            return "{\"success\":true}";
        }

        #endregion
    }
}
