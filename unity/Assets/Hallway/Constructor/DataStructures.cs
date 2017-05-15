using UnityEngine;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour
{
    public const float buttonSize = 50;
    public const float buttonSpace = 10;
    public class Answer
    {
        public string text;
		public bool is_true;
    }

    public class Picture
    {
        public string path;
    }

    public class Paragraph
    {
        public int orderNumber;
        public string header;
        public string text;
        public List<Picture> pictures;
    }

    public class Question
    {
        public string text; //текст вопроса
        public string picQ; //путь к картинке-пояснению либо null, если оной нет
        public bool if_pictured; //true, если ответы картинкой, false - если текстом
        public string picA; //путь к ответам картинкой (if_pictured=true) либо null (if_pictured=false)
        public int ans_count; //количество вариантов ответа (да, если они текстом, то это известно и через answers.Count, а вот если картинкой, то нет)
        public List<Answer> answers; //список вопросов текстом (очевидно пуст, если if_pictured=true)
    }

    public class ThemeContent
    {
        public string id;
        public string name;
        public string type;
        public int maxMinutes;
        public List<Question> questions;
        public List<Paragraph> paragraphs;
        public List<ThemeContentLink> outputThemeContentLinks;
    }

    public class ThemeContentLink 
    {
	    public string parentThemeContentId;
	    public string linkedThemeContentId;
	    public string status;
    }

    public class Theme
    {
        public string id;
        public string name;
        public List<ThemeContent> contents;
        public List<ThemeLink> outputThemeLinks;
    }

    public class ThemeLink
    {
        public string parentThemeId;
        public string linkedThemeId;
        public string status;
    }

    public class Course
    {
        public string name;
        public List<Theme> themes;
    }

    public class CourseRun
    {
        public string id;
        public string mode;
        public string name;
        public float progress;
        public float timeSpent;
        public List<ThemeRun> themesRuns;

        public bool visited;
        public bool completeAll;
    }

    public class ThemeRun
    {
        public string id;
        public string name;
        public float progress;
        public int testsComplete;
        public int testsOverall;
        public float timeSpent;
        public List<TestRun> testsRuns;
        public List<LectureRun> lecturesRuns;

        public bool allLectures;
        public bool allTests;
        public bool allTestsMax;
        public bool completeAll;
    }

    //здесь будет только по одному (лучшему) результату для каждого теста, а не все попытки, все попытки останутся на сервере
    public class TestRun
    {
        public int answersMinimum;
        public int answersCorrect;
        public int answersOverall;
    }

    public class LectureRun
    {
        public float timeSpent;
        public List<ParagraphRun> paragraphsRuns;
    }

    public class ParagraphRun
    {
        public bool haveSeen;
    }

    //--------------

    public class GameAchievement
    {
        public string id;
        public string name;
    }

    public class GameAchievementRun
    {
        public string name;
        public int score;
        public int result;
        public bool passed;
        public bool needToShow;
    }
    public enum AchievementTrigger
    {
        Education,
        Test,
        Lecture,
        Theme,
        Course,
        Paragraph,
        Multiplayer,
        Guide,
        Stend,
        Teleport
    }

    public class OverallRPG
    {
        public bool ifGuest;
        public int EXP;

        //стартовые ачивменты - холл - остальное
        public bool logotypeJump;
        public bool tableJump;
        public bool terminalJump;
        public bool ladderJump_First;
        public bool ladderJump_All;
        public bool letThereBeLight;

        //стартовые ачивменты - коридоры
        public bool plantJump_First;
        public bool plantJump_Second;
        public bool barrelRoll;

        public bool firstVisitLecture;
        public bool firstVisitTest;
    }
}
