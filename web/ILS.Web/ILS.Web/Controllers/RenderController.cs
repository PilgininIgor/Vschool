namespace ILS.Web.Controllers
{
    using Domain;
    using Domain.GameAchievements;
    using GameAchievements;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    public class RenderController : Controller
    {
        private readonly ILSContext context;
        private readonly AchievementsManager achievementsManager;

        public RenderController(ILSContext context)
        {
            this.context = context;
            achievementsManager = new AchievementsManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        //TODO: remove
        public ActionResult CreateSomeTestRunsForCurrentUser()
        {
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            if (u == null)
                return View();

            Course course = context.Course.First();
            Theme theme = context.Theme.First(x => x.Course_Id.Equals(course.Id));
            Lecture lecture = (Lecture)context.ThemeContent.First(x => x.Theme_Id.Equals(theme.Id) && x is Lecture);
            Test test = (Test)context.ThemeContent.First(x => x.Theme_Id.Equals(theme.Id) && x is Test);
            Paragraph paragraph = context.Paragraph.First(x => x.Lecture_Id.Equals(lecture.Id));
            Question question = context.Question.First(x => x.Test_Id.Equals(test.Id));

            //context.CourseRun.Remove(context.CourseRun.First(x => x.User.Name.Equals(u.Name)));

            CourseRun courseRun = new CourseRun();
            courseRun.Progress = 50;
            courseRun.User = u;
            courseRun.TimeSpent = 100;
            courseRun.Course = course;

            ThemeRun themeRun = new ThemeRun();
            themeRun.Progress = 35;
            themeRun.Theme = theme;
            themeRun.CourseRun = courseRun;

            LectureRun lectureRun = new LectureRun();
            lectureRun.Lecture = lecture;
            lectureRun.TimeSpent = 20;
            lectureRun.ThemeRun = themeRun;

            TestRun testRun = new TestRun();
            testRun.Result = 1;
            testRun.Test = test;
            testRun.ThemeRun = themeRun;

            QuestionRun questionRun = new QuestionRun();
            questionRun.Question = question;
            questionRun.TimeSpent = 10;
            questionRun.TestRun = testRun;

            ParagraphRun paragraphRun = new ParagraphRun();
            paragraphRun.HaveSeen = true;
            paragraphRun.Paragraph = paragraph;
            paragraphRun.LectureRun = lectureRun;

            context.QuestionRun.Add(questionRun);
            context.ThemeRun.Add(themeRun);
            context.TestRun.Add(testRun);
            context.LectureRun.Add(lectureRun);
            context.ParagraphRun.Add(paragraphRun);
            context.CourseRun.Add(courseRun);

            context.SaveChanges();
            return View();
        }


        public JsonResult GetProfile(String name)
        {
            //TODO: use name instead of current user to be able to look for other profiles
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            ProfileModel model = new ProfileModel();
            model.Name = u.FirstName + " " + u.LastName;
            model.Email = u.Email;
            model.Money = u.Coins;
            model.Progress = 23; //TODO: load progress
            List<User> users = context.User.OrderByDescending(x => x.Coins).ToList();
            int rating = users.IndexOf(u);
            model.Rating = rating;
            Dictionary<string, string> achievements = new Dictionary<string, string>();
            List<GameAchievementRun> runList = context.GameAchievementRuns.Where(x => x.UserId.Equals(u.Id)).ToList();
            foreach (GameAchievementRun run in runList)
            {
                achievements.Add(run.GameAchievement.Index.ToString(), run.GameAchievement.ImagePath);
            }
            model.Achievements = achievements;
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
                    name = y.Name
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
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);

            return Json(context.CourseRun.Where(z => z.User.Name.Equals(u.Name)).OrderByDescending(x => x.Progress).Select(y => new
            {
                id = y.Course.Id,
                name = y.Course.Name,
                progress = y.Progress
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseProgress(String name, String courseId)
        {
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
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
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
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
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
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
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            Guid id = Guid.Parse(testRunId);
            return Json(context.QuestionRun.Where(z => z.TestRun_Id.Equals(id)
                    && z.TestRun.ThemeRun.CourseRun.User.Name.Equals(u.Name))
                .Select(y => new
                {
                    text = y.Question.Text
                }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnityList()
        {
            return Json(new
            {
                coursesNames = context.Course.OrderBy(x => x.Name).Select(x => new
                {
                    id = x.Id,
                    name = x.Name
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnityData(Guid id)
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
                })
            });
        }

        private string GetThemeLinkStatus(ThemeLink themeLink)
        {
            string status = "open";
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            var ptLinks = themeLink.PersonalThemeLinks.Where(x => x.CourseRun.User_Id == u.Id);
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
            User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            var ptcLinks = tcLink.PersonalThemeContentLinks.Where(x => x.ThemeRun.CourseRun.User_Id == u.Id);
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
            User u = null;
            bool ifGuest = !HttpContext.User.Identity.IsAuthenticated;
            if (!ifGuest) u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            {
                return Json(new
                {
                    ifGuest = ifGuest,
                    username = (ifGuest) ? "" : u.Name,
                    EXP = (ifGuest) ? 0 : u.Coins,
                    facultyStands_Seen = (ifGuest) ? false : u.FacultyStands_Seen,
                    facultyStands_Finish = (ifGuest) ? false : u.FacultyStands_Finish,
                    historyStand_Seen = (ifGuest) ? false : u.HistoryStand_Seen,
                    historyStand_Finish = (ifGuest) ? false : u.HistoryStand_Finish,
                    scienceStand_Seen = (ifGuest) ? false : u.ScienceStand_Seen,
                    scienceStand_Finish = (ifGuest) ? false : u.ScienceStand_Finish,
                    staffStand_Seen = (ifGuest) ? false : u.StaffStand_Seen,
                    staffStand_Finish = (ifGuest) ? false : u.StaffStand_Finish,
                    logotypeJump = (ifGuest) ? false : u.LogotypeJump,
                    tableJump = (ifGuest) ? false : u.TableJump,
                    terminalJump = (ifGuest) ? false : u.TerminalJump,
                    ladderJump_First = (ifGuest) ? false : u.LadderJump_First,
                    ladderJump_All = (ifGuest) ? false : u.LadderJump_All,
                    letThereBeLight = (ifGuest) ? false : u.LetThereBeLight,
                    plantJump_First = (ifGuest) ? false : u.PlantJump_First,
                    plantJump_Second = (ifGuest) ? false : u.PlantJump_Second,
                    barrelRoll = (ifGuest) ? false : u.BarrelRoll,
                    firstVisitLecture = (ifGuest) ? false : u.FirstVisitLecture,
                    firstVisitTest = (ifGuest) ? false : u.FirstVisitTest,
                    teleportations = (ifGuest) ? 0 : u.Teleportations,
                    paragraphsSeen = (ifGuest) ? 0 : u.ParagraphsSeen,
                    testsFinished = (ifGuest) ? 0 : u.TestsFinished
                });
            }
        }

        public ActionResult UnityStat(Guid id)
        {
            var c = context.Course.Find(id);
            if (!HttpContext.User.Identity.IsAuthenticated)
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
                var u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
                if (context.CourseRun.Count(x => (x.User.Name == u.Name) && (x.Course_Id == c.Id)) == 0) //ищем в базе статистику юзера по этому курсу
                { //если нет, то он заходит в него впервые, и надо создать ему "нулевую" статистику
                    CourseRun cr = new CourseRun()
                    {
                        Course = c,
                        User = u,
                        Progress = 0.0,
                        TimeSpent = 0.0,
                        Visisted = false,
                        CompleteAll = false
                    };
                    u.CoursesRuns.Add(cr);
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
                var course_run = context.CourseRun.First(x => (x.User.Name == u.Name) && (x.Course_Id == c.Id));
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
                User u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var obj = jss.Deserialize<dynamic>(s);

                u.Coins = obj["EXP"];
                u.FacultyStands_Seen = obj["facultyStands_Seen"];
                u.FacultyStands_Finish = obj["facultyStands_Finish"];
                u.HistoryStand_Seen = obj["historyStand_Seen"];
                u.HistoryStand_Finish = obj["historyStand_Finish"];
                u.ScienceStand_Seen = obj["scienceStand_Seen"];
                u.ScienceStand_Finish = obj["scienceStand_Finish"];
                u.StaffStand_Seen = obj["staffStand_Seen"];
                u.StaffStand_Finish = obj["staffStand_Finish"];
                u.LogotypeJump = obj["logotypeJump"];
                u.TableJump = obj["tableJump"];
                u.TerminalJump = obj["terminalJump"];
                u.LadderJump_First = obj["ladderJump_First"];
                u.LadderJump_All = obj["ladderJump_All"];
                u.LetThereBeLight = obj["letThereBeLight"];
                u.PlantJump_First = obj["plantJump_First"];
                u.PlantJump_Second = obj["plantJump_Second"];
                u.BarrelRoll = obj["barrelRoll"];
                u.FirstVisitLecture = obj["firstVisitLecture"];
                u.FirstVisitTest = obj["firstVisitTest"];
                u.Teleportations = obj["teleportations"];
                u.ParagraphsSeen = obj["paragraphsSeen"];
                u.TestsFinished = obj["testsFinished"];

                context.SaveChanges();
            }
            catch (Exception e)
            {
                // TODO: log this!
            }
            return 1;
        }

        public ActionResult SaveGameAchievement(String achievementId)
        {
            var changedAchievementRuns = achievementsManager.ExecuteAchievement(AchievementTrigger.Game,
                new Dictionary<string, object> { { AchievementsConstants.GameAchievementIdParamName, achievementId } });
            return Json(changedAchievementRuns.Select(a => new { id = a.Id, GameAchievement = a.GameAchievementId }));
        }

        public ActionResult GetGameAchievementsForUnity()
        {
            return Json(context.GameAchievements.OrderBy(x => x.Name).Select(x => new
            {
                id = x.Id,
                name = x.Name,
                message = x.Message
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGameAchievementRuns()
        {
            var user = context.User.First(x => x.Name == HttpContext.User.Identity.Name);

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
                var testRun = new TestRun { Result = 0 };
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
            var u = context.User.First(x => x.Name == HttpContext.User.Identity.Name);
            foreach (var tLink in theme.OutputThemeLinks)
            {
                foreach (var ptLink in tLink.PersonalThemeLinks.Where((y => ((y.CourseRun.User_Id == u.Id)
                    && (y.Status == "open")))))
                {
                    ptLink.Status = "frozen";
                    context.SaveChanges();
                    FreezeOutputLinks(ptLink.ThemeLink.LinkedTheme);
                }
            }
        }

    }
}