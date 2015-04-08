using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ILS.Domain;
using ILS.Domain.TestGenerator;

namespace ILS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TestGeneratorController : Controller
    {
        ILSContext context;

        public TestGeneratorController(ILSContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
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
                    iconCls = (x is Lecture) ? "lecture" : "test",
                    id = x.Id.ToString(),
                    text = x.Name
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
                else
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
