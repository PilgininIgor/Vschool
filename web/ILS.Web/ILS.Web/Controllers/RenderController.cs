namespace ILS.Web.Controllers
{
    using Domain;
using Domain.GameAchievements;
using GameAchievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

    public class RenderController : Controller
    {
        private readonly ILSContext context;

        public RenderController(ILSContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult GetProfile(String name)
        {
            User u = context.User.First(x => x.Name == name);
            if (u == null)
            {
                return Json(new
                {
                    success = false
                });
            }
            ProfileModel model = new ProfileModel
            {
                Name = u.FirstName + " " + u.LastName,
                Email = u.Email,
                Money = u.Coins
            };
            List<CourseRun> courses = context.CourseRun.Where(z => z.User.Name.Equals(u.Name)).ToList();
            double progress = courses.Sum(run => run.Progress);
            if (courses.Count != 0)
            {
                progress /= courses.Count;
            }
            model.Progress = (int) progress; 
            List<User> users = context.User.OrderByDescending(x => x.Coins).ToList();
            int rating = users.IndexOf(u);
            model.Rating = rating;
            List<GameAchievementRun> runList = context.GameAchievementRuns.Where(x => x.UserId.Equals(u.Id) && x.Passed).ToList();
            Dictionary<string, string> achievements = runList.ToDictionary(run => run.GameAchievement.Index.ToString(), run => run.GameAchievement.ImagePath);
            model.Achievements = achievements;
            model.AchievementsCount = context.GameAchievements.Count();
            return Json(new
            {
                model
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGameAchievements()
        {
            return Json(new
            {
                achievements = context.GameAchievements.OrderBy(x => x.Index).Select(y => new
                {
                    name = y.Name,
                    index = y.Index
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGameRating()
        {
            return Json(new
            {
                rating = context.User.OrderByDescending(x => x.Coins).Select(y => new
                {
                    name = y.Name,
                    exp = y.Coins
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesProgress(String name)
        {
            User u = context.User.First(x => x.Name == name);
            if (u == null)
            {
                return Json(new
                {
                    success = false
                });
            }

            return Json(context.CourseRun.Where(z => z.User.Name.Equals(u.Name)).OrderByDescending(x => x.Progress).Select(y => new
            {
                id = y.Course.Id,
                name = y.Course.Name,
                progress = y.Progress
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseProgress(String name, String courseId)
        {
            User u = context.User.First(x => x.Name == name);
            if (u == null)
            {
                return Json(new
                {
                    success = false
                });
            }
            Guid id = Guid.Parse(courseId);
            return Json(context.ThemeRun.Where(z => z.Theme.Course_Id.Equals(id)
                    && z.CourseRun.User.Name.Equals(u.Name))
                .OrderByDescending(x => x.Progress).Select(y => new
                {
                    id = y.Theme.Id,
                    name = y.Theme.Name,
                    progress = y.Progress
                }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThemeProgress(String name, String themeId)
        {
            User u = context.User.First(x => x.Name == name);
            if (u == null)
            {
                return Json(new
                {
                    success = false
                });
            }
            Guid id = Guid.Parse(themeId);
            return Json(new
            {
                lectures = context.LectureRun.Where(z => z.Lecture.Theme_Id.Equals(id)
                    && z.ThemeRun.CourseRun.User.Name.Equals(u.Name))
                .Select(y => new
                {
                    id = y.Lecture.Id,
                    name = y.Lecture.Name,
                    progress = y.ParagraphsRuns.Count(c => c.HaveSeen) / y.ParagraphsRuns.Count()
                }).OrderByDescending(v => v.progress),
                tests = context.TestRun.Where(z => z.Test.Theme_Id.Equals(id)
                    && z.ThemeRun.CourseRun.User.Name.Equals(u.Name))
                .GroupBy(test => test.Test, test => (double)test.Result / test.QuestionsRuns.Count() * 100)
                .Select(y => new
                {
                    id = y.Key.Id,
                    name = y.Key.Name,
                    progress = y.Max()
                }).OrderByDescending(v => v.progress)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTestProgress(String name, String testId)
        {
            User u = context.User.First(x => x.Name == name);
            if (u == null)
            {
                return Json(new
                {
                    success = false
                });
            }
            Guid id = Guid.Parse(testId);
            var t = context.QuestionRun.Where(z => z.TestRun.Test_Id.Equals(id));
            var q = t.Where(z => z.TestRun.ThemeRun.CourseRun.User.Name.Equals(u.Name));
            var v = q.GroupBy(test => test.TestRun, run => (double)run.TestRun.Result / run.TestRun.QuestionsRuns.Count() * 100);
            var w = v.Select(y => new
            {
                id = y.Key.Id,
                date = "TODO: date here",
                progress = y.Max()
            });
            var c = w.OrderByDescending(d => d.date);
            return Json(c, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetTestRunProgress(String name, String testRunId)
        {
            User u = context.User.First(x => x.Name == name);
            if (u == null)
            {
                return Json(new
                {
                    success = false
                });
            }
            Guid id = Guid.Parse(testRunId);
            return Json(context.QuestionRun.Where(z => z.TestRun_Id.Equals(id)
                    && z.TestRun.ThemeRun.CourseRun.User.Name.Equals(u.Name))
                .Select(y => new
                {
                    text = y.Question.Text
                }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnityList()
        {
            return Json(new
            {
                coursesNames = context.Course.OrderBy(x => x.Name).Select(x => new
                {
                    id = x.Id,
                    name = x.Name
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnityData(Guid id)
        {
            var c = context.Course.Find(id);
            return Json(new
            {
                name = c.Name,
                themes = c.Themes.OrderBy(x => x.OrderNumber).Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    contents = x.ThemeContents.OrderBy(y => y.OrderNumber).Select(y => new
                    {
                        id = y.Id,
                        name = y.Name,
                        type = (y is Lecture) ? "lecture" : "test",
                        paragraphs = (y is Lecture) ? ((Lecture)y).Paragraphs.OrderBy(z => z.OrderNumber).Select(z => new
                        {
                            orderNumber = z.OrderNumber,
                            header = z.Header,
                            text = z.Text,
                            pictures = z.Pictures.OrderBy(u => u.OrderNumber).Select(u => new
                            {
                                path = u.Path
                            })
                        }) : null,
                        maxMinutes = (y is Test) ? ((Test)y).MaxMinutes : 0,
                        questions = (y is Test) ? ((Test)y).Questions.OrderBy(v => v.OrderNumber).Select(v => new
                        {
                            text = v.Text,
                            picQ = v.PicQ,
                            if_pictured = v.IfPictured,
                            picA = v.PicA,
                            ans_count = v.AnswerVariants.Count,
                            answers = v.AnswerVariants.OrderBy(w => w.OrderNumber).Select(w => new
                            {
                                text = w.OrderNumber + ") " + w.Text
                            })
                        }) : null,
                        outputThemeContentLinks = y.OutputThemeContentLinks.OrderBy(t => y.OrderNumber).Select(t => new
                        {
                            parentThemeContentId = t.ParentThemeContent_Id,
                            linkedThemeContentId = t.LinkedThemeContent_Id,
                            status = GetThemeContentLinkStatus(t)
                        })
                    }),
                    outputThemeLinks = x.OutputThemeLinks.OrderBy(y => x.OrderNumber).Select(y => new
                    {
                        parentThemeId = y.ParentTheme_Id,
                        linkedThemeId = y.LinkedTheme_Id,
                        status = GetThemeLinkStatus(y)
                    })
                }).ToList()
            });
        }

        private string GetThemeLinkStatus(ThemeLink themeLink)
        {
            string status = "open";
            User user = GetCurrentUser();
            var ptLinks = themeLink.PersonalThemeLinks.Where(x => x.CourseRun.User_Id == user.Id);
            var personalThemeLinks = ptLinks as PersonalThemeLink[] ?? ptLinks.ToArray();
            if (!personalThemeLinks.Any())
            {
                return "closed";
            }
            foreach (var ptLink in personalThemeLinks)
            {
                switch (ptLink.Status)
                {
                    case "frozen":
                        return "frozen";
                    case "closed":
                        status = "closed";
                        break;
                }
            }

            return status;
        }

        private string GetThemeContentLinkStatus(ThemeContentLink tcLink)
        {
            string status = "open";
            User user = GetCurrentUser();
            var ptcLinks = tcLink.PersonalThemeContentLinks.Where(x => x.ThemeRun.CourseRun.User_Id == user.Id);
            var personalThemeContentLinks = ptcLinks as PersonalThemeContentLink[] ?? ptcLinks.ToArray();
            if (!personalThemeContentLinks.Any())
            {
                return "closed";
            }
            foreach (var ptcLink in personalThemeContentLinks)
            {
                switch (ptcLink.Status)
                {
                    case "frozen":
                        return "frozen";
                    case "closed":
                        status = "closed";
                        break;
                }
            }

            return status;
        }

        public ActionResult UnityRPG()
        {
            User user = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) user = GetCurrentUser();
            {
                return Json(new
                {
                    ifGuest = ifGuest,
                    username = (ifGuest) ? "" : user.Name,
                    EXP = (ifGuest) ? 0 : user.Coins,
                    facultyStands_Seen = (ifGuest) ? false : user.FacultyStands_Seen,
                    facultyStands_Finish = (ifGuest) ? false : user.FacultyStands_Finish,
                    historyStand_Seen = (ifGuest) ? false : user.HistoryStand_Seen,
                    historyStand_Finish = (ifGuest) ? false : user.HistoryStand_Finish,
                    scienceStand_Seen = (ifGuest) ? false : user.ScienceStand_Seen,
                    scienceStand_Finish = (ifGuest) ? false : user.ScienceStand_Finish,
                    staffStand_Seen = (ifGuest) ? false : user.StaffStand_Seen,
                    staffStand_Finish = (ifGuest) ? false : user.StaffStand_Finish,
                    logotypeJump = (ifGuest) ? false : user.LogotypeJump,
                    tableJump = (ifGuest) ? false : user.TableJump,
                    terminalJump = (ifGuest) ? false : user.TerminalJump,
                    ladderJump_First = (ifGuest) ? false : user.LadderJump_First,
                    ladderJump_All = (ifGuest) ? false : user.LadderJump_All,
                    letThereBeLight = (ifGuest) ? false : user.LetThereBeLight,
                    plantJump_First = (ifGuest) ? false : user.PlantJump_First,
                    plantJump_Second = (ifGuest) ? false : user.PlantJump_Second,
                    barrelRoll = (ifGuest) ? false : user.BarrelRoll,
                    firstVisitLecture = (ifGuest) ? false : user.FirstVisitLecture,
                    firstVisitTest = (ifGuest) ? false : user.FirstVisitTest,
                    teleportations = (ifGuest) ? 0 : user.Teleportations,
                    paragraphsSeen = (ifGuest) ? 0 : user.ParagraphsSeen,
                    testsFinished = (ifGuest) ? 0 : user.TestsFinished
                });
            }
        }

        public JsonResult UnityStat(Guid id)
        {
            var c = context.Course.Find(id);
            if (HttpContext == null || HttpContext.User == null || !HttpContext.User.Identity.IsAuthenticated)
            { //если юзер - гость, то передать "нулевую" статистику (он начнет с самого начала, и сохранять его прогресс мы в базе не будем)
                return Json(new
                {
                    mode = "guest",
                    name = c.Name,
                    progress = 0.0,
                    timeSpent = 0.0,
                    visited = false,
                    completeAll = false,
                    themesRuns = c.Themes.OrderBy(x => x.OrderNumber).Select(x => new
                    {
                        id = "",
                        name = x.Name,
                        progress = 0.0,
                        testsComplete = 0,
                        testsOverall = x.ThemeContents.Count(v => v is Test),
                        timeSpent = 0.0,
                        allLectures = false,
                        allTests = false,
                        allTestsMax = false,
                        completeAll = false,
                        testsRuns = x.ThemeContents.OfType<Test>().OrderBy(y => y.OrderNumber).Select(y => new
                        {
                            answersMinimum = y.MinResult,
                            answersCorrect = 0,
                            answersOverall = y.Questions.Count
                        }),
                        lecturesRuns = x.ThemeContents.OfType<Lecture>().OrderBy(z => z.OrderNumber).Select(z => new
                        {
                            timeSpent = 0.0,
                            paragraphsRuns = z.Paragraphs.Select(u => new
                            {
                                haveSeen = false
                            })
                        })
                    })
                });
            }
            else
            { //если юзер авторизован, то найти его в базе по имени
                var user = GetCurrentUser();
                if (context.CourseRun.Count(x => (x.User.Name == user.Name) && (x.Course_Id == c.Id)) == 0) //ищем в базе статистику юзера по этому курсу
                { //если нет, то он заходит в него впервые, и надо создать ему "нулевую" статистику
                    CourseRun cr = new CourseRun()
                    {
                        Course = c,
                        User = user,
                        Progress = 0.0,
                        TimeSpent = 0.0,
                        Visisted = false,
                        CompleteAll = false
                    };
                    user.CoursesRuns.Add(cr);
                    foreach (var t in c.Themes.OrderBy(x => x.OrderNumber))
                    {
                        ThemeRun tr = new ThemeRun()
                        {
                            Theme = t,
                            CourseRun = cr,
                            Progress = 0.0,
                            TimeSpent = 0.0,
                            TestsComplete = 0,
                            AllLectures = false,
                            AllTests = false,
                            AllTestsMax = false,
                            CompleteAll = false
                        };
                        cr.ThemesRuns.Add(tr);
                        foreach (var tc in t.ThemeContents.OfType<Lecture>().OrderBy(x => x.OrderNumber))
                        {
                            LectureRun lr = new LectureRun() { Lecture = tc, ThemeRun = tr, TimeSpent = 0.0 };
                            tr.LecturesRuns.Add(lr);
                            foreach (var p in tc.Paragraphs.OrderBy(x => x.OrderNumber))
                            {
                                ParagraphRun pr = new ParagraphRun() { Paragraph = p, LectureRun = lr, HaveSeen = false };
                                lr.ParagraphsRuns.Add(pr);
                            }
                        }
                    }
                    context.SaveChanges();
                }
                var course_run = context.CourseRun.First(x => (x.User.Name == user.Name) && (x.Course_Id == c.Id));
                return Json(new
                { //возвращаем статистику юзера - неважно, "нулевая" ли она и только что созданная или же старая и в ней уже есть какой-то прогресс
                    mode = "registered",
                    id = course_run.Id,
                    name = c.Name,
                    progress = course_run.Progress,
                    timeSpent = course_run.TimeSpent,
                    visited = course_run.Visisted,
                    completeAll = course_run.CompleteAll,
                    themesRuns = course_run.ThemesRuns.OrderBy(x => x.Theme.OrderNumber).Select(x => new
                    {
                        id = x.Id,
                        name = x.Theme.Name,
                        progress = x.Progress,
                        testsComplete = x.TestsComplete,
                        testsOverall = x.Theme.ThemeContents.Count(v => v is Test),
                        timeSpent = x.TimeSpent,
                        allLectures = x.AllLectures,
                        allTests = x.AllTests,
                        allTestsMax = x.AllTestsMax,
                        completeAll = x.CompleteAll,
                        testsRuns = x.Theme.ThemeContents.OfType<Test>().OrderBy(y => y.OrderNumber).Select(y => new
                        {
                            answersMinimum = y.MinResult,
                            answersCorrect = (x.TestsRuns.Count(w => w.Test == y) == 0) ? 0 : x.TestsRuns.Where(w => w.Test == y).OrderByDescending(w => w.Result).First().Result,
                            answersOverall = y.Questions.Count
                        }),
                        lecturesRuns = x.LecturesRuns.OrderBy(z => z.Lecture.OrderNumber).Select(z => new
                        {
                            timeSpent = z.TimeSpent,
                            paragraphsRuns = z.ParagraphsRuns.OrderBy(q => q.Paragraph.OrderNumber).Select(q => new
                            {
                                haveSeen = q.HaveSeen
                            })
                        })
                    })
                });

            }
        }

        public int UnitySaveRPG(string s)
        {
            try
            {
                User user = GetCurrentUser();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var obj = jss.Deserialize<dynamic>(s);

                user.Coins = obj["EXP"];
                user.FacultyStands_Seen = obj["facultyStands_Seen"];
                user.FacultyStands_Finish = obj["facultyStands_Finish"];
                user.HistoryStand_Seen = obj["historyStand_Seen"];
                user.HistoryStand_Finish = obj["historyStand_Finish"];
                user.ScienceStand_Seen = obj["scienceStand_Seen"];
                user.ScienceStand_Finish = obj["scienceStand_Finish"];
                user.StaffStand_Seen = obj["staffStand_Seen"];
                user.StaffStand_Finish = obj["staffStand_Finish"];
                user.LogotypeJump = obj["logotypeJump"];
                user.TableJump = obj["tableJump"];
                user.TerminalJump = obj["terminalJump"];
                user.LadderJump_First = obj["ladderJump_First"];
                user.LadderJump_All = obj["ladderJump_All"];
                user.LetThereBeLight = obj["letThereBeLight"];
                user.PlantJump_First = obj["plantJump_First"];
                user.PlantJump_Second = obj["plantJump_Second"];
                user.BarrelRoll = obj["barrelRoll"];
                user.FirstVisitLecture = obj["firstVisitLecture"];
                user.FirstVisitTest = obj["firstVisitTest"];
                user.Teleportations = obj["teleportations"];
                user.ParagraphsSeen = obj["paragraphsSeen"];
                user.TestsFinished = obj["testsFinished"];

                context.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: log this!
            }
            return 1;
        }

        public ActionResult SaveGameAchievement(string triggerValue, string parameters)
        {
            var achievementsManager = new AchievementsManager(context);
            var trigger = (AchievementTrigger)Enum.Parse(typeof(AchievementTrigger), triggerValue);
            var changedAchievementRuns = achievementsManager.ExecuteAchievement(trigger, GetCurrentUser(), new Dictionary<string, object>());
            return Json(changedAchievementRuns.Select(run => new
            {
                name = run.GameAchievement.Name, 
                score = run.GameAchievement.Score,
                result = run.Result,
                passed = run.Passed,
                needToShow = run.NeedToShow
            }));
        }

        public JsonResult GetUserCoinsUrl()
        {
            return Json(GetCurrentUser().Coins, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGameAchievementsForUnity()
        {
            return Json(context.GameAchievements.OrderBy(x => x.Name).Select(x => new
            {
                id = x.Id,
                name = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGameAchievementRuns()
        {
            var user = GetCurrentUser();

            return Json(context.GameAchievementRuns.Where(a => a.UserId.Equals(user.Id)).Select(x => new
            {
                id = x.Id,
                name = x.Result
            }), JsonRequestBehavior.AllowGet);
        }

        public int UnitySave(string s)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var obj = jss.Deserialize<dynamic>(s);
            CourseRun course_run = context.CourseRun.Find(Guid.Parse(obj["id"]));
            course_run.Progress = (double)obj["progress"];
            course_run.TimeSpent = (double)obj["timeSpent"];
            course_run.Visisted = obj["visited"];
            course_run.CompleteAll = obj["completeAll"];
            int i = 0;
            foreach (ThemeRun theme_run in course_run.ThemesRuns.OrderBy(x => x.Theme.OrderNumber))
            {
                theme_run.Progress = (double)obj["themesRuns"][i]["progress"];
                theme_run.TimeSpent = (double)obj["themesRuns"][i]["timeSpent"];
                theme_run.TestsComplete = obj["themesRuns"][i]["testsComplete"];
                theme_run.AllLectures = obj["themesRuns"][i]["allLectures"];
                theme_run.AllTests = obj["themesRuns"][i]["allTests"];
                theme_run.AllTestsMax = obj["themesRuns"][i]["allTestsMax"];
                theme_run.CompleteAll = obj["themesRuns"][i]["completeAll"];

                //при полном прохождении темы открываем индивидуальные связи между темами
                if (theme_run.CompleteAll)
                {
                    foreach (ThemeLink theme_link in theme_run.Theme.OutputThemeLinks)
                    {
                        ICollection<PersonalThemeLink> pt_links = theme_link.PersonalThemeLinks;
                        if (pt_links.Count() == 0)
                        {
                            PersonalThemeLink pt_link = context.PersonalThemeLink.Add(new PersonalThemeLink() { ThemeLink = theme_link, CourseRun = course_run, Status = "open" });
                            theme_link.PersonalThemeLinks.Add(pt_link);
                        }
                        else
                            pt_links.ElementAt(0).Status = "open";
                    }
                }

                int j = 0;
                int p_seen = 0;
                foreach (LectureRun lec_run in theme_run.LecturesRuns.OrderBy(y => y.Lecture.OrderNumber))
                {
                    lec_run.TimeSpent = (double)obj["themesRuns"][i]["lecturesRuns"][j]["timeSpent"];
                    int k = 0;
                    foreach (ParagraphRun p_run in lec_run.ParagraphsRuns.OrderBy(z => z.Paragraph.OrderNumber))
                    {
                        p_run.HaveSeen = obj["themesRuns"][i]["lecturesRuns"][j]["paragraphsRuns"][k]["haveSeen"];
                        if (p_run.HaveSeen) p_seen++;
                        k++;
                    }
                    //при полном прохождении лекции открываем индивидуальные связи между содержимым темы
                    if (p_seen == lec_run.ParagraphsRuns.Count)
                    {
                        foreach (ThemeContentLink tc_link in lec_run.Lecture.OutputThemeContentLinks)
                        {
                            ICollection<PersonalThemeContentLink> ptc_links = tc_link.PersonalThemeContentLinks;
                            if (ptc_links.Count() == 0)
                            {
                                PersonalThemeContentLink ptc_link = context.PersonalThemeContentLink.Add(new PersonalThemeContentLink() { ThemeContentLink = tc_link, ThemeRun = theme_run, Status = "open" });
                                tc_link.PersonalThemeContentLinks.Add(ptc_link);
                            }
                            else
                                ptc_links.ElementAt(0).Status = "open";
                        }
                    }
                    j++;
                }
                i++;
            }
            context.SaveChanges();
            return 1;
        }

        public int UnityTest(string mode, Guid? themeRunId, Guid testId)
        {
            var s = HttpContext.Request.Params["answers"];
            var f = HttpContext.Request.Params["time"].Split(',');

            var test = (Test)context.ThemeContent.Find(testId);

            if (mode != "guest")
            {
                var testRun = new TestRun { Result = 0, TestDateTime = DateTime.Now};
                var themeRun = context.ThemeRun.Find(themeRunId);

                themeRun.TestsRuns.Add(testRun); test.TestRuns.Add(testRun);

                var i = 0;
                //список айди тем, из которых есть ответы с ошибками - нужно для составных тестов 
                var themeErrorsList = new List<Guid>();
                foreach (var question in test.Questions.OrderBy(x => x.OrderNumber))
                {
                    var correct = true;
                    var questionRun = new QuestionRun
                    {
                        Question = question,
                        TimeSpent = Convert.ToDouble(f[question.OrderNumber - 1], System.Globalization.CultureInfo.InvariantCulture)
                    };
                    testRun.QuestionsRuns.Add(questionRun);
                    foreach (var answerVariant in question.AnswerVariants.OrderBy(x => x.OrderNumber))
                    {
                        if (s[i] == '1')
                        {
                            questionRun.Answers.Add(new Answer { AnswerVariant = answerVariant });
                        }
                        if (((s[i] == '1') && !answerVariant.IfCorrect) || ((s[i] == '0') && answerVariant.IfCorrect))
                        {
                            correct = false;
                            if (test.IsComposite)
                            {
                                //тут некорректное сравнение, из-за связи 1 ко многим между тестом и вопросом
                                //по-сути вопросы дулблируются, поэтому ищем одинаковые тексты вопроса 
                                themeErrorsList.Add(context.Question.Where(x => ((x.Text == question.Text)
                                    && (!x.Test.IsComposite))).OrderBy(x => x.Test.Theme_Id).Select(x => x.Test.Theme_Id).ToArray()[0]);
                            }
                        }
                        i += 2;
                    }
                    if (correct)
                    {
                        testRun.Result++;
                    }
                }
                //при верном прохождении теста открываем индивидуальные связи между содержимым темы
                if (testRun.Result >= test.MinResult)
                {
                    foreach (var tcLink in test.OutputThemeContentLinks)
                    {
                        var ptcLinks = tcLink.PersonalThemeContentLinks;
                        if (!ptcLinks.Any())
                        {
                            var ptcLink =
                                context.PersonalThemeContentLink.Add(new PersonalThemeContentLink
                                {
                                    ThemeContentLink = tcLink,
                                    ThemeRun = themeRun,
                                    Status = "open"
                                });
                            tcLink.PersonalThemeContentLinks.Add(ptcLink);
                        }
                        else
                        {
                            ptcLinks.ElementAt(0).Status = "open";
                        }
                    }
                }
                //если тест - составной и не пройден - находим худшую тему и замораживаем связи 
                else if (test.IsComposite)
                {
                    if (themeErrorsList.Count == 0) return testRun.Result;
                    themeErrorsList.Sort();
                    int maxIndex = 0, curStreak = 1, maxStreak = 1;
                    var curGuid = themeErrorsList[0];
                    for (var j = 1; j < themeErrorsList.Count(); j++)
                    {
                        if (curGuid != themeErrorsList[j])
                        {
                            curGuid = themeErrorsList[j];
                            if (curStreak > maxStreak)
                            {
                                maxIndex = j - 1;
                                maxStreak = curStreak;
                            }
                            curStreak = 0;
                        }
                        curStreak++;
                    }
                    if (curStreak > maxStreak)
                    {
                        maxIndex = themeErrorsList.Count() - 1;
                    }

                    FreezeOutputLinks(context.Theme.Find(themeErrorsList[maxIndex]));
                }

                context.SaveChanges();
                return testRun.Result;
            }
            else
            {
                var res = 0;
                var i = 0;
                foreach (var q in test.Questions.OrderBy(x => x.OrderNumber))
                {
                    var correct = true;
                    foreach (var a in q.AnswerVariants.OrderBy(x => x.OrderNumber))
                    {
                        if (((s[i] == '1') && !a.IfCorrect) || ((s[i] == '0') && a.IfCorrect))
                        {
                            correct = false;
                        }
                        i += 2;
                    }
                    if (correct) res++;
                }
                return res;
            }

        }

        private void FreezeOutputLinks(Theme theme)
        {
            var user = GetCurrentUser();
            foreach (var tLink in theme.OutputThemeLinks)
            {
                foreach (var ptLink in tLink.PersonalThemeLinks.Where((y => ((y.CourseRun.User_Id == user.Id)
                    && (y.Status == "open")))))
                {
                    ptLink.Status = "frozen";
                    context.SaveChanges();
                    FreezeOutputLinks(ptLink.ThemeLink.LinkedTheme);
                }
            }
        }

        public ActionResult GetUserName()
        {
            return Json(GetCurrentUser().Name, JsonRequestBehavior.AllowGet);
        }

        private User GetCurrentUser()
        {
            //TODO MAKE REAL AUTHORIZATION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            return String.IsNullOrEmpty(HttpContext.User.Identity.Name) ? context.User.First(x => x.Name == "admin")
                : context.User.First(x => x.Name == HttpContext.User.Identity.Name);
        }
    }
}