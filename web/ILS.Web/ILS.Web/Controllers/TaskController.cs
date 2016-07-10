using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using ILS.Domain;

namespace ILS.Web.Controllers
{
    public class TaskController : Controller
    {
        ILSContext context;

        public TaskController(ILSContext context)
        {
            this.context = context;
        }

        public JsonResult GetTask3(Guid id)
        {
            //var tasks = context.ThemeContent.Where(themeContent => themeContent is Task3Content);
            //System.Random rnd = new System.Random();
            //int k = rnd.Next(0, tasks.Count());
            //var task = tasks.ToList()[k];
            var task = context.ThemeContent.Find(id) as Task3Content;

            var user = GetCurrentUser();
            var themeRun = context.ThemeRun.First(localThemeRun => localThemeRun.CourseRun.User_Id == user.Id /*&& localThemeRun.Theme_Id == task.Theme.Id*/);
            var taskRuns = context.Task3Run.Where(localTask => localTask.ThemeRun.Id == themeRun.Id);
            Task3Run taskRun;
            if (taskRuns == null || taskRuns.Count() == 0)
            {
                taskRun = new Task3Run
                {
                    Task3Content = task,
                    ThemeRun = themeRun,
                };
                context.Task3Run.Add(taskRun);
            }
            else
            {
                taskRun = taskRuns.First();
                taskRun.Task3Content = task;
            }

            taskRun.NumberOfTurns = 0;
            context.SaveChanges();

            string taskStr = task.NumberOfCylinders + "," + task.LimitOf5 + "," + task.LimitOf4;

            return Json(taskStr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckTask3(Guid id, string turns_str)
        {
            var task = context.ThemeContent.Find(id) as Task3Content;

            var user = GetCurrentUser();
            var taskRun = context.Task3Run.First(localTask => localTask.ThemeRun.CourseRun.User_Id == user.Id);

            int optimal = (int)Math.Pow((double)2, (double)task.NumberOfCylinders) - 1;
            int turns = int.Parse(turns_str);

            taskRun.NumberOfTurns = turns;
            if (turns - optimal <= task.LimitOf5)
            {
                taskRun.Result = 5;
            }
            if (turns - optimal > task.LimitOf5 && turns - optimal <= task.LimitOf4)
            {
                taskRun.Result = 4;
            }
            if (turns - optimal > task.LimitOf4)
            {
                taskRun.Result = 3;
            }
            if (turns == -1)
            {
                taskRun.Result = 2;
            }

            context.SaveChanges();

            return Json("");
        }

        public JsonResult GetTask1(Guid id)
        {            
            //var tasks = context.ThemeContent.Where(themeContent => themeContent is Task1Content);
            //System.Random rnd = new System.Random();
            //int k = rnd.Next(0, tasks.Count());
            //var task = tasks.ToList()[k];
            var task = context.ThemeContent.Find(id) as Task1Content;

            var user = GetCurrentUser();
            var themeRun = context.ThemeRun.First(localThemeRun => localThemeRun.CourseRun.User_Id == user.Id /*&& localThemeRun.Theme_Id == task.Theme.Id*/);
            var taskRuns = context.Task1Run.Where(localTask => localTask.ThemeRun.Id == themeRun.Id);
            Task1Run taskRun;
            if (taskRuns == null || taskRuns.Count() == 0)
            {
                taskRun = new Task1Run
                {
                    Task1Content = task,
                    ThemeRun = themeRun,
                };
                context.Task1Run.Add(taskRun);
            }
            else
            {
                taskRun = taskRuns.First();
                taskRun.Task1Content = task;
            }

            taskRun.AttemptsNumber = 0;
            context.SaveChanges();

            string taskStr = "";

            if (task.Type == "operation")
            {
                taskStr = "Действие в " + task.Scale1 + " СС:" + "," + Convert.ToString(task.Number1, task.Scale1).ToUpper() + " "
                    + task.Operation + " " + Convert.ToString(task.Number2, task.Scale1).ToUpper();
            }
            else if (task.Type == "translation")
            {
                taskStr = "Из " + task.Scale1 + " СС в " + task.Scale2 + " СС:" + ","
                    + Convert.ToString(task.Number1, task.Scale1).ToUpper();
            }

            return Json(taskStr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckTask1(Guid id, string answer)
        {
            var task = context.ThemeContent.Find(id) as Task1Content;
            
            var user = GetCurrentUser();
            var taskRun = context.Task1Run.First(localTask => localTask.ThemeRun.CourseRun.User_Id == user.Id);

            taskRun.AttemptsNumber++;

            string correctAnswer = "";

            if (task.Type == "operation")
            {
                int result = 0;
                switch (task.Operation)
                {
                    case "+": result = task.Number1 + task.Number2; break;
                    case "-": result = task.Number1 - task.Number2; break;
                    case "*": result = task.Number1 * task.Number2; break;
                    default: break;
                }
                correctAnswer = Convert.ToString(result, task.Scale1).ToUpper();
            }
            else if (task.Type == "translation")
            {
                correctAnswer = Convert.ToString(task.Number1, task.Scale2).ToUpper();
            }

            bool check = correctAnswer == answer;

            if (check)
            {
                switch (taskRun.AttemptsNumber)
                {
                    case 1: taskRun.Result = 5; break;
                    case 2: taskRun.Result = 4; break;
                    case 3: taskRun.Result = 3; break;
                    default: break;
                }

            }
            else
            {
                if (taskRun.AttemptsNumber == 3)
                {
                    taskRun.Result = 2;
                }
            }

            context.SaveChanges();

            string resultStr = check + "," + correctAnswer + "," + taskRun.AttemptsNumber;

            return Json(resultStr);
        }

        public JsonResult GetTask2(Guid id)
        {
            //var tasks = context.ThemeContent.Where(themeContent => themeContent is Task2Content);
            //System.Random rnd = new System.Random();
            //int k = rnd.Next(0, tasks.Count());
            //var task = tasks.ToList()[k];
            var task = context.ThemeContent.Find(id) as Task2Content;

            var user = GetCurrentUser();
            var themeRun = context.ThemeRun.First(localThemeRun => localThemeRun.CourseRun.User_Id == user.Id /*&& localThemeRun.Theme_Id == task.Theme.Id*/);
            var taskRuns = context.Task2Run.Where(localTask => localTask.ThemeRun.Id == themeRun.Id);
            Task2Run taskRun;
            if (taskRuns == null || taskRuns.Count() == 0)
            {
                taskRun = new Task2Run
                {
                    Task2Content = task,
                    ThemeRun = themeRun,
                };
                context.Task2Run.Add(taskRun);
            }
            else
            {
                taskRun = taskRuns.First();
                taskRun.Task2Content = task;
            }

            taskRun.AttemptsNumber = 0;
            context.SaveChanges();

            return Json(task.TaskString, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckTask2(Guid id, string el1, string el2, string el3, string el4)
        {
            var task = context.ThemeContent.Find(id) as Task2Content;

            var user = GetCurrentUser();
            var taskRun = context.Task2Run.First(localTask => localTask.ThemeRun.CourseRun.User_Id == user.Id);

            taskRun.AttemptsNumber++;

            string[] answer = new string[4] { el1, el2, el3, el4 };
            string formula = task.TaskString;

            int k = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                if (formula[i] == 'b' || formula[i] == 'o')
                {
                    k++;
                }
            }

            bool check = true;
            for (int i = 0; i < k; i++)
            {
                if (answer[i] == "1" || answer[i] == "0")
                {
                    int pos = formula.IndexOf("b");
                    formula = formula.Remove(pos, 1);
                    formula = formula.Insert(pos, answer[i]);
                }
                if (answer[i] == "c" || answer[i] == "d" || answer[i] == "i" || answer[i] == "e")
                {
                    int pos = formula.IndexOf("o");
                    formula = formula.Remove(pos, 1);
                    formula = formula.Insert(pos, answer[i]);
                }
                if (answer[i] == "")
                {
                    check = false;
                }
            }

            string resultStr = "";
            bool check2 = false;

            if (check)
            {
                check2 = FormulaExecuter.Eval(FormulaExecuter.PrepareFormula(formula));

                if (check2)
                {
                    switch (taskRun.AttemptsNumber)
                    {
                        case 1: taskRun.Result = 5; break;
                        case 2: taskRun.Result = 4; break;
                        case 3: taskRun.Result = 3; break;
                        default: break;
                    }

                }
                else
                {
                    if (taskRun.AttemptsNumber == 3)
                    {
                        taskRun.Result = 2;
                    }
                }

                context.SaveChanges();

                resultStr = check2 + "," + taskRun.AttemptsNumber;

                return Json(resultStr);
            }

            if (taskRun.AttemptsNumber == 3)
            {
                taskRun.Result = 2;
            }

            context.SaveChanges();

            resultStr = check2 + "," + taskRun.AttemptsNumber;

            return Json(resultStr);
        }

        private User GetCurrentUser()
        {
            return String.IsNullOrEmpty(HttpContext.User.Identity.Name) ? context.User.First(x => x.Name == "admin")
                : context.User.First(x => x.Name == HttpContext.User.Identity.Name);
        }

        public class FormulaExecuter
        {
            public static string PrepareFormula(string f)
            {

                return f.Replace("1", "true").Replace("0", "false");
            }

            public static bool Eval(string expression)
            {
                CSharpCodeProvider codeProvider = new CSharpCodeProvider();

                string sourceCode = string.Format(@"
                    namespace MyAssembly
                    {{
                        public class Evaluator
                        {{
                            public bool Eval()
                            {{
                                return {0};
                            }}

                            public static bool c(bool p1, bool p2)
                            {{
                                return (p1 && p2);
                            }}


                            public static bool d(bool p1, bool p2)
                            {{
                                return (p1 || p2);
                            }}


                            public static bool e(bool p1, bool p2)
                            {{
                                return (p1 == p2);
                            }}


                            public static bool i(bool p1, bool p2)
                            {{
                                return (!p1 || p2);
                            }}
                        }}
                    }}

                ", expression);

                CompilerResults results =
                    codeProvider
                    .CompileAssemblyFromSource(new CompilerParameters(), new string[] { sourceCode });

                Assembly assembly = results.CompiledAssembly;
                dynamic evaluator =
                    Activator.CreateInstance(assembly.GetType("MyAssembly.Evaluator"));
                return evaluator.Eval();
            }
        }

    }
}
