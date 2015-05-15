using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ILS.Domain;
using ILS.Domain.TestGenerator;
using ILS.Domain.TestGenerator.Settings;
using System.Data.Entity.Core.Objects;
using Novacode;
using System.Drawing;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TestGeneratorController : Controller
    {
        ILSContext context;
        ObjectContext context_obj;

        public TestGeneratorController(ILSContext context)
        {
            this.context = context;
            this.context_obj = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)context).ObjectContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskEditor()
        {
            return View();
        }

        public Guid AddTGTest(Guid parent_id)
        {
            var theme = context.Theme.Find(parent_id);
            int num;
            if (theme.ThemeContents.Count == 0) num = 1;
            else num = theme.ThemeContents.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;

            var testSettings = new TGTestSetting()
            {
                Id = Guid.NewGuid(),
                AnswersMixMode_Id = TGMixMode.IN_RANDOM,
                TasksMixMode_Id = TGMixMode.IN_RANDOM,
                CountOfTaskMode_Id = TGCountOfTaskMode.ALL_TASKS,
                RatingCalculationMode_Id = TGRatingCalculationMode.IN_100_POINT_SCALE,
                IsTimeLimitMode = false
            };

            var newTGTest = new TGTest() { Name = "Новый генерируемый тест №" + num, Theme_Id = parent_id, OrderNumber = num, TestSetting = testSettings };

            context.TGTestSetting.Add(testSettings);
            context.TGTest.Add(newTGTest);
            context.SaveChanges();

            return newTGTest.Id;
        }

        public JsonResult GetData()
        {
            List<Data> dataList = new List<Data>();

            dataList.Add(new Data() { Name = "a", TypeOfData = "Целый" });
            dataList.Add(new Data() { Name = "b", TypeOfData = "Целый" });
            dataList.Add(new Data() { Name = "число_a", TypeOfData = "Число" });
            dataList.Add(new Data() { Name = "число_b", TypeOfData = "Число" });
            dataList.Add(new Data() { Name = "мин", TypeOfData = "Целый" });
            dataList.Add(new Data() { Name = "макс", TypeOfData = "Целый" });
            dataList.Add(new Data() { Name = "средн_арифм", TypeOfData = "Дробный" });

            return Json(new { data = dataList }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult GetAvailableFunctions()
        {
            string jsonString = @"children: [{
                                text: 'СТАНДАРТНЫЕ',
                                expanded: true,
                                children:
                                [{
                                    text: 'Математические',
                                    children: [{
                                        text: 'Тригонометрические',
                                        expanded: true,
                                        children: [{
                                            text: 'СИНУС',
                                            leaf: true
                                        }, {
                                            text: 'КОСИНУС',
                                            leaf: true
                                        }, {
                                            text: 'ТАНГЕНС',
                                            leaf: true
                                        }, {
                                            text: 'КОТАНГЕНС',
                                            leaf: true
                                        }]
                                    }, {
                                        text: 'Сравнения',
                                        expanded: true,
                                        children: [{
                                            text: 'РАВНО',
                                            leaf: true
                                        }, {
                                            text: 'МЕНЬШЕ',
                                            leaf: true
                                        }, {
                                            text: 'БОЛЬШЕ',
                                            leaf: true
                                        }]
                                    }, {
                                        text: 'КОРЕНЬ',
                                        leaf: true
                                    }, {
                                        text: 'МОДУЛЬ',
                                        leaf: true
                                    }],
                                    'expanded': true
                                },
                                {
                                    text: 'Статистические',
                                    expanded: true,
                                    children: [{
                                        text: 'СРЕДНЕЕ',
                                        leaf: true
                                    }, {
                                        text: 'МИНИМУМ',
                                        leaf: true
                                    }, {
                                        text: 'МАКСИМУМ',
                                        leaf: true
                                    }]
                                },
                                {
                                    text: 'Логические',
                                    expanded: true,
                                    children: [{
                                        text: 'ИСТИНА',
                                        leaf: true
                                    }, {
                                        text: 'ЛОЖЬ',
                                        leaf: true
                                    }, {
                                        text: 'И',
                                        leaf: true
                                    }, {
                                        text: 'ИЛИ',
                                        leaf: true
                                    }, {
                                        text: 'НЕ',
                                        leaf: true
                                    }]
                                }]
                            }, {
                                text: 'ПОЛЬЗОВАТЕЛЬСКИЕ',
                                expanded: true,
                                children:
                                [{
                                    text: 'Системы счисления',
                                    children: [{
                                        text: '1',
                                        leaf: true
                                    }, {
                                        text: '2',
                                        leaf: true
                                    }, {
                                        text: '3',
                                        leaf: true
                                    }, {
                                        text: '4',
                                        leaf: true
                                    }]
                                }, {
                                    text: 'Алгебра логики',
                                    expanded: true,
                                    children: [{
                                        text: '5',
                                        leaf: true
                                    }, {
                                        text: '6',
                                        leaf: true
                                    }, {
                                        text: '7',
                                        leaf: true
                                    }, {
                                        text: '8',
                                        leaf: true
                                    }]
                                }]
                            }]";

            return new ContentResult { Content = jsonString, ContentType = "application/json" };
        }

        public JsonResult PersonNewList()
        {
            List<dynamic> persons = new List<dynamic>();
            persons.Add(new { PersonID = 1, FirstName = "Kenan", LastName = "Hançer", CreateDate = DateTime.Now });
            persons.Add(new { PersonID = 2, FirstName = "Kagan", LastName = "Demir", CreateDate = DateTime.Now });

            JsonResult jr = Json(new { data = persons, total = persons.Count }, JsonRequestBehavior.AllowGet);

            return jr;
        }

        [HttpPost]
        public ActionResult CreatePerson(string data)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var deserializedData = jss.Deserialize<dynamic>(data);

            object personID = deserializedData["PersonID"];
            object firstName = deserializedData["FirstName"];
            object lastName = deserializedData["LastName"];
            object createDate = deserializedData["CreateDate"];

            return Json(new
            {
                success = true,
                message = "Create method called successfully"
            });
        }

        [HttpPost]
        public ActionResult EditPerson(string data)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var deserializedData = jss.Deserialize<dynamic>(data);

            object personID = deserializedData["PersonID"];
            object firstName = deserializedData["FirstName"];
            object lastName = deserializedData["LastName"];
            object createDate = deserializedData["CreateDate"];

            return Json(new
            {
                success = true,
                message = "Update method called successfully"
            });
        }

        [HttpPost, ActionName("DeletePerson")]
        public ActionResult DeleteConfirmed(int id)
        {
            return Json(new
            {
                success = true,
                message = "Delete method called successfully"
            });
        }

        public ActionResult ReadTree(string node)
        {
            /* Метод вызывается деревом из файла testgenerator.main.tree.js в двух случаях:
             * Первый - при первоначальной загрузке дерева. Тогда id = "treeRoot", и мы возвращаем список курсов.
             * Второй - когда пользователь разворачивает очередную ветку дерева. Тогда id = идентификатор курса/темы/лекции/теста 
             * (поиском по базе данных мы можем узнать, чего именно), и нам нужно вернуть список тем этого 
             * курса / лекций и тестов этой темы / параграфов этой лекции / вопросов этого теста */

            if (node == "treeRoot")
            {
                /* Начальная загрузка дерева - верхний уровень, возвращаем список курсов. 
                 * Заметьте, что оформлен он именно так, как нужно элементам дерева, т.е. есть параметр iconCls,
                 * определяющий иконку, текст элемента, а также id, с помощью которого мы, в свою очередь, 
                 * сможем развернуть следующую ветку, когда снова вызовем этот метод */
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
                //уровень тем - возвращаем список генерируемых тестов
                return Json(theme.ThemeContents.ToList().OrderBy(x => x.OrderNumber).Where(x => x is TGTest).Select(x => new
                {
                    iconCls = "tgtest",
                    id = x.Id.ToString(),
                    text = x.Name
                }), JsonRequestBehavior.AllowGet);
            }
            var tc = context.ThemeContent.Find(guid);
            if (tc != null)
            {
                //уровень тестов - возвращаем список вопросов
                TGTest test = (TGTest)tc;
                return Json(test.TaskTemplates.ToList().OrderBy(x => x.OrderNumber).Select(x => new
                {
                    iconCls = "tgtasktemplate",
                    id = x.Id.ToString(),
                    text = x.Name,
                    leaf = true
                }), JsonRequestBehavior.AllowGet);
            }
            return new EmptyResult();
        }

        public ActionResult ReadTGTest(Guid id)
        {
            var tgTest = context.TGTest.Find(id);
            return Json(new
            {
                success = true,
                data = new
                {
                    id = tgTest.Id,
                    name = tgTest.Name,
                    description = tgTest.Description,
                    countoftaskmode = tgTest.TestSetting.CountOfTaskMode_Id,
                    answersmixmode = tgTest.TestSetting.AnswersMixMode_Id,
                    tasksmixmode = tgTest.TestSetting.TasksMixMode_Id,
                    countoftasks = tgTest.TestSetting.CountOfTasks,
                    istimelimitmode = tgTest.TestSetting.IsTimeLimitMode,
                    timelimitminutes = tgTest.TestSetting.TimeLimitMinutes,
                    timelimitseconds = tgTest.TestSetting.TimeLimitSeconds,
                    ratingcalculationmode = tgTest.TestSetting.RatingCalculationMode_Id
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadTGTaskTemplate(Guid id)
        {
            var tgTaskTemplate = context.TGTaskTemplate.Find(id);
            return Json(new
            {
                success = true,
                data = new
                {
                    id = tgTaskTemplate.Id,
                    name = tgTaskTemplate.Name,
                    description = tgTaskTemplate.Description
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveTGTaskTemplate(Guid id, string name, string description)
        {
            var tgTaskTemplate = context.TGTaskTemplate.Find(id);
            tgTaskTemplate.Name = name;
            tgTaskTemplate.Description = description;
            context.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult SaveTGTest(Guid id, string name, string description,
            Guid countoftaskmode, int countoftasks, Guid tasksmixmode, Guid answersmixmode, Guid ratingcalculationmode,
            string istimelimitmode, int timelimitminutes, int timelimitseconds)
        {
            var tgTest = context.TGTest.Find(id);
            tgTest.Name = name;
            tgTest.Description = description;
            tgTest.TestSetting.CountOfTaskMode_Id = countoftaskmode;
            tgTest.TestSetting.CountOfTasks = countoftasks;
            tgTest.TestSetting.TasksMixMode_Id = tasksmixmode;
            tgTest.TestSetting.AnswersMixMode_Id = answersmixmode;
            tgTest.TestSetting.RatingCalculationMode_Id = ratingcalculationmode;
            if (tgTest.TestSetting.IsTimeLimitMode = istimelimitmode == "on")
            {
                tgTest.TestSetting.TimeLimitMinutes = timelimitminutes;
                tgTest.TestSetting.TimeLimitSeconds = timelimitseconds;
            }
            else
            {
                tgTest.TestSetting.TimeLimitMinutes = 0;
                tgTest.TestSetting.TimeLimitSeconds = 0;
            };
            context.SaveChanges();
            return Json(new { success = true });
        }

        public ActionResult ReadTGCountOfTaskModes()
        {
            return Json(new
            {
                success = true,
                data = context.TGCountOfTaskMode
                    .OrderBy(x => x.OrderNumber)
                    .Select(x => new { id = x.Id, name = x.Name })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadTGMixModes()
        {
            return Json(new
            {
                success = true,
                data = context.TGMixMode
                    .OrderBy(x => x.OrderNumber)
                    .Select(x => new { id = x.Id, name = x.Name })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadTGRatingCalculationModes()
        {
            return Json(new
            {
                success = true,
                data = context.TGRatingCalculationMode
                    .OrderBy(x => x.OrderNumber)
                    .Select(x => new { id = x.Id, name = x.Name, description = x.Description })
            }, JsonRequestBehavior.AllowGet);
        }

        public Guid AddTGTaskTemplate(Guid parent_id)
        {
            TGTest test = (TGTest)context.ThemeContent.Find(parent_id);
            int num;
            if (test.TaskTemplates.Count == 0) num = 1;
            else num = test.TaskTemplates.OrderBy(x => x.OrderNumber).Last().OrderNumber + 1;
            var taskTemplate = new TGTaskTemplate
            {
                OrderNumber = num,
                Name = "Новый шаблон вопроса №" + num
            };
            test.TaskTemplates.Add(taskTemplate);
            context.SaveChanges();
            return taskTemplate.Id;
        }

        public string RemoveTGTaskTemplate(Guid id, Guid parent_id)
        {
            TGTaskTemplate taskTemplate = context.TGTaskTemplate.Find(id);
            context_obj.DeleteObject(taskTemplate);
            context_obj.SaveChanges();

            int i = 1;
            foreach (var x in ((TGTest)context.TGTest.Find(parent_id)).TaskTemplates.OrderBy(x => x.OrderNumber))
            {
                x.OrderNumber = i;
                i++;
            }
            context.SaveChanges();
            return "OK";
        }




        private const float MULTIPLIER_FOR_CONVERT_TO_CENTIMETERS = 37.789F;

        public static float MARGIN_BOTTOM = 2;
        public static float MARGIN_LEFT = 3;
        public static float MARGIN_RIGHT = (float)1.5;
        public static float MARGIN_TOP = 2;
        public static string FILE_NAME = @"Test.docx";
        public static string COURSE_NAME = "Информатика и ИКТ";
        public static string THEME_NAME = "Основы позиционных систем счисления";

        /// <summary>
        /// Установить поля для документа
        /// </summary>
        /// <param name="document">Документ</param>
        /// <param name="marginTop">Верхнее поле</param>
        /// <param name="marginRight">Правое поле</param>
        /// <param name="marginBottom">Нижнее поле</param>
        /// <param name="marginLeft">Левое поле</param>
        public static void SetPageMargins(DocX document, float marginTop, float marginRight, float marginBottom, float marginLeft)
        {
            document.MarginTop = marginTop * MULTIPLIER_FOR_CONVERT_TO_CENTIMETERS;
            document.MarginRight = marginRight * MULTIPLIER_FOR_CONVERT_TO_CENTIMETERS;
            document.MarginBottom = marginBottom * MULTIPLIER_FOR_CONVERT_TO_CENTIMETERS;
            document.MarginLeft = marginLeft * MULTIPLIER_FOR_CONVERT_TO_CENTIMETERS;
        }
        public static void SetPageHeaderAndFooter(DocX document)
        {
            document.AddHeaders();
            document.AddFooters();
            Header header_default = document.Headers.odd;
            Footer footer_default = document.Footers.odd;

            foreach (var paragraph in header_default.Paragraphs)
                header_default.Paragraphs.Remove(paragraph);

            Novacode.Paragraph p1 = header_default.InsertParagraph("Тест сгенерирован дистанционной обучающей системой \"3Ducation\" / ", false, new Formatting() { FontFamily = new System.Drawing.FontFamily("Times New Roman"), Size = 10, Italic = true });
            p1.Append("http://virtual.itschool.ssau.ru/\n").Font(new System.Drawing.FontFamily("Times New Roman")).Color(Color.Blue).UnderlineStyle(UnderlineStyle.singleLine).FontSize(10);
            p1.Append("Тест создан " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")).Font(new System.Drawing.FontFamily("Times New Roman")).Italic().FontSize(10);

            Novacode.Paragraph p3 = footer_default.InsertParagraph();
            p3.Append("Стр. ").AppendPageNumber(PageNumberFormat.normal);
            p3.Append(" из ").AppendPageCount(PageNumberFormat.normal);
            p3.Alignment = Alignment.center;
        }

        public static void SetTestTitle(DocX document, Formatting defaultFormat, Formatting testTilteFormat)
        {
            document.InsertParagraph("", false, defaultFormat).FontSize(16).Alignment = Alignment.center;
            document.InsertParagraph("ТЕСТ", false, testTilteFormat).Alignment = Alignment.center;
            document.InsertParagraph(String.Format("по курсу: «{0}»", COURSE_NAME), false, defaultFormat).Alignment = Alignment.center;
            document.InsertParagraph(String.Format("по теме: «{0}»", THEME_NAME), false, defaultFormat).Alignment = Alignment.center;
        }

        public FileResult GetFile()
        {
            string filepath = System.IO.Path.GetTempPath() + "Template.docx";
            using (DocX document = DocX.Create(filepath))
            {
                var defaultFormat = new Formatting() { FontFamily = new System.Drawing.FontFamily("Times New Roman"), Size = 14 };
                var testTilteFormat = new Formatting() { FontFamily = new System.Drawing.FontFamily("Times New Roman"), Size = 16, Bold = true };

                SetPageMargins(document, MARGIN_TOP, MARGIN_RIGHT, MARGIN_BOTTOM, MARGIN_LEFT);
                SetPageHeaderAndFooter(document);
                SetTestTitle(document, defaultFormat, testTilteFormat);

                for (int indexOfQuestion = 1; indexOfQuestion < 21; indexOfQuestion++)
                {
                    document.InsertParagraph("", false, defaultFormat).FontSize(16).Alignment = Alignment.center;

                    Table tableQuestion = document.AddTable(2, 2);
                    tableQuestion.Design = TableDesign.TableGrid;
                    tableQuestion.AutoFit = AutoFit.Window;

                    tableQuestion.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Center;

                    tableQuestion.Rows[0].Height = 25f;

                    float availableSpace = document.PageWidth - document.MarginLeft - document.MarginRight;
                    tableQuestion.Rows[0].Cells[0].Width = Math.Round(0.06 * availableSpace);
                    tableQuestion.Rows[1].Cells[0].Width = Math.Round(0.06 * availableSpace);
                    tableQuestion.Rows[0].Cells[1].Width = Math.Round(availableSpace);
                    tableQuestion.Rows[1].Cells[1].Width = Math.Round(availableSpace);

                    Border borderWhite = new Border(Novacode.BorderStyle.Tcbs_none, BorderSize.one, 1, Color.White);
                    tableQuestion.Rows[0].Cells[1].SetBorder(TableCellBorderType.Top, borderWhite);
                    tableQuestion.Rows[1].Cells[1].SetBorder(TableCellBorderType.Top, borderWhite);
                    tableQuestion.Rows[0].Cells[1].SetBorder(TableCellBorderType.Right, borderWhite);
                    tableQuestion.Rows[1].Cells[0].SetBorder(TableCellBorderType.Right, borderWhite);
                    tableQuestion.Rows[1].Cells[1].SetBorder(TableCellBorderType.Right, borderWhite);
                    tableQuestion.Rows[0].Cells[1].SetBorder(TableCellBorderType.Bottom, borderWhite);
                    tableQuestion.Rows[1].Cells[0].SetBorder(TableCellBorderType.Bottom, borderWhite);
                    tableQuestion.Rows[1].Cells[1].SetBorder(TableCellBorderType.Bottom, borderWhite);
                    tableQuestion.Rows[1].Cells[0].SetBorder(TableCellBorderType.Left, borderWhite);
                    tableQuestion.Rows[1].Cells[1].SetBorder(TableCellBorderType.Left, borderWhite);

                    tableQuestion.MergeCellsInColumn(1, 0, 1);

                    tableQuestion.Alignment = Alignment.both;

                    tableQuestion.Rows[0].Cells[0].Paragraphs.First().Append("" + indexOfQuestion).Font(new System.Drawing.FontFamily("Times New Roman")).FontSize(14D).Alignment = Alignment.center;
                    tableQuestion.Rows[0].Cells[1].Paragraphs.First().Append("Какое целое число должно быть записано в ячейке A1,  чтобы диаграмма, построенная по значениям ячеек диапазона A2: С2, соответствовала рисунку? Известно, что все значения ячеек из рассматриваемого диапазона неотрицательны.").Font(new System.Drawing.FontFamily("Times New Roman")).FontSize(14D).Alignment = Alignment.both;


                    Table tableAnswer = document.AddTable(1, 1);
                    for (int i = 0; i < 20; i++)
                    {
                        tableAnswer.InsertColumn();
                    }
                    tableAnswer.Design = TableDesign.TableGrid;
                    tableAnswer.AutoFit = AutoFit.Window;

                    tableAnswer.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Center;

                    tableAnswer.Rows[0].Height = 25f;

                    tableAnswer.Rows[0].Cells[0].Width = Math.Round(0.05 * availableSpace);
                    for (int i = 1; i < 21; i++)
                    {
                        tableAnswer.Rows[0].Cells[i].Width = Math.Round(0.05 * availableSpace);
                    }

                    tableAnswer.Rows[0].Cells[0].SetBorder(TableCellBorderType.Top, borderWhite);
                    tableAnswer.Rows[0].Cells[0].SetBorder(TableCellBorderType.Bottom, borderWhite);
                    tableAnswer.Rows[0].Cells[0].SetBorder(TableCellBorderType.Left, borderWhite);

                    tableAnswer.Alignment = Alignment.both;

                    document.InsertTable(tableQuestion);
                    document.InsertParagraph("", false, defaultFormat).FontSize(6).Alignment = Alignment.center;
                    tableAnswer.Rows[0].Cells[0].Paragraphs.First().Append("ОТВЕТ:").Font(new System.Drawing.FontFamily("Times New Roman")).FontSize(14D).Alignment = Alignment.left;
                    document.InsertTable(tableAnswer);
                }

                document.Save();

                return File(filepath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
        }
    }

    public class Data
    {
        public string Name { get; set; }
        public string TypeOfData { get; set; }
    }

    public class Function
    {
        public string text { get; set; }
        public string name { get; set; }
        public bool leaf { get; set; }
        public bool expanded { get; set; }
    }
}
