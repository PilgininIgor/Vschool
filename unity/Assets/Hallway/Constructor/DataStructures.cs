using UnityEngine;
using System.Collections.Generic;

public class DataStructures : MonoBehaviour
{
    public class Answer
    {
        public string text;
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

    public class OverallRPG
    {
        public bool ifGuest;
        public string username;
        public int EXP;

        //стартовые ачивменты - холл - стенды
        public bool facultyStands_Seen;
        public bool facultyStands_Finish;
        public bool historyStand_Seen;
        public bool historyStand_Finish;
        public bool scienceStand_Seen;
        public bool scienceStand_Finish;
        public bool staffStand_Seen;
        public bool staffStand_Finish;

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
        public int teleportations;
        public int paragraphsSeen;
        public int testsFinished;
    }
}
