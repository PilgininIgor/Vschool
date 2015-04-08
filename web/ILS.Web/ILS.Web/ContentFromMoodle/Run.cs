using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class Run
    {
        //Список лекций
        public List<Lection> Lections;
        public List<Test> Tests;
        List<Question> Questions;        
        string fullPathDirectoryToMoodledata;
        string Database_Host_User_Pass;
        string physpathToFile_PathsToFilesFromMoodle;
        List<string> listPaths = new List<string>();

        //***************************************
        //Поля для проверки добавления контента в виртуальную систему
        public Test t_check;
        public Lection l_check;

        public Run(string Database, string Host, string User, string Pass, string physpathToFile_PathsToFilesFromMoodle)
        {            
            Database_Host_User_Pass = "Database=" + Database + ";Data Source=" + Host + ";User Id=" + User + ";Password=" + Pass;
            CreateDirectoryForFiles(physpathToFile_PathsToFilesFromMoodle);
            Table t1 = new Table("mdl_lesson_pages", Database_Host_User_Pass);
            Table t2 = new Table("mdl_lesson", Database_Host_User_Pass);
            //fullPathDirectoryToMoodledata = GetFullPathDirectoryToMoodledata();
            //для локального поиска файлов
            //fullPathDirectoryToMoodledata = "Z:\\home\\localhost\\www\\moodle2\\moodledata";
            Table t3 = new Table("mdl_question", Database_Host_User_Pass);
            Table t4 = new Table("mdl_quiz", Database_Host_User_Pass);
            Table t5 = new Table("mdl_question_answers", Database_Host_User_Pass);

            this.physpathToFile_PathsToFilesFromMoodle = physpathToFile_PathsToFilesFromMoodle;
            FillListPaths_FromFile_DownloadedListFromMoodle_txt();

            GetQuestions(t3, t5);
            GetTests(t4);
            GetLections(t1, t2);

            t_check = Tests[0];
            l_check = Lections[0];

            /*foreach (Lection l in Lections)
                checkedListBox1.Items.Add(l.nameLection + "(" + l.ListParagraphs.Count.ToString() + ")" + "/L");
            foreach (Test t in Tests)
                checkedListBox1.Items.Add(t.name + "(" + t.lQuestion.Count.ToString() + ")" + "/T");
            */
        }        

        /*private void button1_Click(object sender, EventArgs e)
        {
            Test t_download;
            Lection l_download;
            string name = "";
            foreach (var chItem in checkedListBox1.CheckedItems)
            {
                int i = 0;
                while (chItem.ToString()[i] != '(')
                {
                    name += chItem.ToString()[i];
                    i++;
                }
                if (chItem.ToString()[chItem.ToString().Length - 1] == 'T')
                {
                    foreach (Test t in Tests)
                    {
                        if (t.name == name)
                        {
                            t_download = t;
                            //И добавляем тест в виртуальный мир
                            break;
                        }
                    }
                }
                if (chItem.ToString()[chItem.ToString().Length - 1] == 'L')
                {
                    foreach (Lection l in Lections)
                    {
                        if (l.nameLection == name)
                        {
                            l_download = l;
                            //И добавляем лекцию в виртуальный мир
                            break;
                        }
                    }
                }

            }
        }*/

        void GetQuestions(Table t_questions, Table t_answers_question)
        {
            Questions = new List<Question>();
            string text, type;
            int id_quest;
            bool randomYesNo = false;
            int category;
            string name_files;
            string pathToPicture = null;
            string namePicture = null;

            //переменные для определения вариантов ответов для соотв вопроса
            int id_question_in_table_answers;
            string text_answer;
            bool rightly_answer;
            List<AnswerOnQuestion> answersOnQuestion;

            for (int i = 0; i < t_questions.countRecords; i++)
            {
                if (t_questions.tableInStrings[i, 10] == "multichoice" || t_questions.tableInStrings[i, 10] == "random")
                {
                    id_quest = Convert.ToInt32(t_questions.tableInStrings[i, 0]);
                    text = t_questions.tableInStrings[i, 4];
                    type = t_questions.tableInStrings[i, 3];
                    category = Convert.ToInt32(t_questions.tableInStrings[i, 1]);
                    name_files = t_questions.tableInStrings[i, 6];

                    //По id вопросу мы "бежим" по таблице, пока не запишем в 
                    //соответствующий вопрос ответы
                    answersOnQuestion = new List<AnswerOnQuestion>();
                    for (int iq = 0; iq < t_answers_question.countRecords; iq++)
                    {
                        id_question_in_table_answers = Convert.ToInt32(t_answers_question.tableInStrings[iq, 1]);
                        text_answer = t_answers_question.tableInStrings[iq, 2];
                        if (Convert.ToDouble(t_answers_question.tableInStrings[iq, 3]) != 0)
                            rightly_answer = true;
                        else
                            rightly_answer = false;

                        if (id_question_in_table_answers == id_quest)
                        {
                            answersOnQuestion.Add(new AnswerOnQuestion(text_answer, rightly_answer));
                        }
                    }

                    if (name_files != "")
                    {   
                        //для локального поиска файлов
                        //pathToPicture = GetFullPathToFile.GetFullPathToFile_Method(name_files, fullPathDirectoryToMoodledata);

                        //для поиска файлов на FTP-сервере
                        pathToPicture = GetFullPathToFile_OnFTP(name_files);

                        if (pathToPicture != null)
                        {
                            name_files = GetNameFile(name_files);
                            namePicture = name_files;
                        }
                    }
                    text = Regex.Replace(text, "<[^>]*?>", string.Empty, RegexOptions.IgnoreCase);
                    if (t_questions.tableInStrings[i, 10] == "random")
                    {
                        randomYesNo = true;                        
                        Questions.Add(new Question(id_quest, text, type, randomYesNo, category, pathToPicture, answersOnQuestion, namePicture));
                        randomYesNo = false;
                        name_files = null;
                        pathToPicture = null;
                        namePicture = null;
                    }
                    else
                    {
                        Questions.Add(new Question(id_quest, text, type, randomYesNo, category, pathToPicture, answersOnQuestion, namePicture));
                        name_files = null;
                        pathToPicture = null;
                        namePicture = null;
                    }
                }
            }
        }

        void FillListPaths_FromFile_DownloadedListFromMoodle_txt()
        {
            StreamReader sw = new StreamReader(physpathToFile_PathsToFilesFromMoodle + "\\DownloadedListFromMoodle.txt");
            string sLine = "";
            while (sLine != null)
            {
                sLine = sw.ReadLine();
                if (sLine != null)
                    listPaths.Add(sLine);
            }
            sw.Close();
        }
 
        string GetFullPathToFile_OnFTP(string n_files)
        {
            bool findPath = false;
            
            //string sLine = "";
            //while (sLine != null)
            foreach (string sLine in listPaths)
            {
                string sForParse = "";
                //sLine = sw.ReadLine();
                if (sLine != null)
                    sForParse = sLine;
                else
                    continue;

                int pozSimv;
                try
                {
                    pozSimv = sForParse.LastIndexOf("/");
                    sForParse = sForParse.Substring(0, pozSimv);
                    pozSimv = sForParse.LastIndexOf("/");
                    sForParse = sLine.Substring(pozSimv+1);
                }
                catch (Exception e)
                {
                    return "http://img534.imageshack.us/img534/5839/gmez.jpg";
                }

                if (sForParse == n_files)
                {
                    return sLine;
                    findPath = true;
                    break;
                }                
            }
            
            if (!findPath)
            {
                foreach (string sLine in listPaths)
                {
                    string sForParse = "";
                    //sLine = sw.ReadLine();
                    if (sLine != null)
                        sForParse = sLine;
                    else
                        continue;

                    int pozSimv;
                    try
                    {
                        pozSimv = sForParse.LastIndexOf("/");                        
                        sForParse = sLine.Substring(pozSimv + 1);
                    }
                    catch (Exception e)
                    {
                        return "http://img534.imageshack.us/img534/5839/gmez.jpg";
                    }

                    if (sForParse == n_files)
                    {
                        return sLine;
                        findPath = true;
                        break;
                    }
                }
                //sw.Close();
            }

            if (!findPath)
            {
                return "http://img534.imageshack.us/img534/5839/gmez.jpg";
            }
            
            return null;
        }

        string GetNameFile(string name)
        {
            string name_file = "";
            //В этом цикле выдираем имя из name_file - 
            //Это в случае если имя файла содержит имена\имя директорий
            for (int j = 0; j < name.Length; j++)
            {
                if (name[name.Length - j - 1] != '/')
                    name_file += name[name.Length - j - 1];
                else
                    break;
            }
            //Инвертируем имя файла
            char[] s = new char[name_file.Length];
            for (int j = 0; j < name_file.Length; j++)
            {
                s[j] = name_file[name_file.Length - 1 - j];
            }
            name_file = new string(s);
            return name_file;
        }

        void GetTests(Table t_tests)
        {
            Tests = new List<Test>();
            List<Question> quest_lect;     //Вопросы которые
            string name;                   //входят в какой-то определенный тест  
            string questions;
            int countQuest;
            long id;

            for (int i = 0; i < t_tests.countRecords; i++)
            {
                quest_lect = new List<Question>();
                name = t_tests.tableInStrings[i, 2];
                id = long.Parse(t_tests.tableInStrings[i, 0]);
                questions = t_tests.tableInStrings[i, 16];
                countQuest = Convert.ToInt32(t_tests.tableInStrings[i, 17]);
                if (countQuest == 0) continue;
                //Парсим questions - получаем id_шники вопросов
                string[] questions_lect = questions.Split(',');          //Вопросы данной лекции
                int n = 0;
                for (int j = 0; j < Questions.Count; j++)                // countQuest==questions_lect.Length
                {
                    if (questions_lect.Length == n) break;
                    if (Questions[j].id_question == Convert.ToInt32(questions_lect[n]))
                    {
                        if (Questions[j].randomYesNo)
                        {
                            //Т.е. если вопрос оказался рандомным, то
                            // записываем в список temporary вопросы той же категории, 
                            // что и рандомный. И после этого рандомно выбираем из вопросов
                            // категории один - и добавляем в тест

                            List<Question> temporary = new List<Question>();
                            int n1 = 0;
                            for (int j1 = 0; j1 < Questions.Count; j1++)
                            {
                                if (Questions[j1].category == Questions[j].category
                                    && Questions[j1].id_question != Questions[j].id_question)
                                {
                                    temporary.Add(Questions[j1]);
                                    temporary[n1].randomYesNo = true;
                                    n1++;
                                }
                            }
                            if (temporary.Count != 0)
                            //Это условие выполнится тогда, когда рандомный вопрос
                            // был не типа: "множественный выбор"(т.е. не часть A) 
                            //
                            {
                                Random r = new Random();
                                int z = r.Next(0, temporary.Count);
                                quest_lect.Add(temporary[z]);
                                n++;
                                j = -1;
                            }
                        }
                        else
                        {
                            quest_lect.Add(Questions[j]);
                            n++;
                            j = -1;
                        }
                    }
                }

                /* ВАЖНО! ВАЖНО! ВАЖНО!
                 * ***********************************************************
                 * В тест добавляются вопросы только типа:"множественный выбор"
                 * либо типа:"рандом", которые находятся в категории, содержащей
                 * вопрсы опять же типа:"множественный выбор"
                */
                if (quest_lect.Count != 0)
                    Tests.Add(new Test(id, name, quest_lect, countQuest));
            }
        }

        /// <summary>
        /// Получение списка лекций
        /// </summary>
        /// <param name="t_lect">Данные таблицы mdl_lesson_pages</param>
        /// <param name="t_name">Данные из таблицы mdl_lesson</param>
        void GetLections(Table t_lect, Table t_name)
        {
            Lections = new List<Lection>();
            int usingPages = 0;
            List<string> pages = new List<string>();
            List<string> title = new List<string>();
            List<int> fifo = new List<int>();
            string namePages = "";
            int searchLect = 1;
            int namePage = 0;
            bool b = true;
            while (true)
            {
                for (int i = 0; i < t_lect.countRecords; i++)
                {
                    if (t_lect.tableInStrings[i, 1] == searchLect.ToString())
                    {
                        pages.Add(t_lect.tableInStrings[i, 11]); // Добавляем страницу лекции в список
                        fifo.Add(Convert.ToInt32(t_lect.tableInStrings[i, 2]));
                        title.Add(t_lect.tableInStrings[i, 10]);
                        b = false;
                    }
                }
                usingPages += pages.Count;
                if (b && usingPages == t_lect.countRecords) return; //условие выхода из цикла - когда прочитаны все строки таблицы
                else if (b && usingPages != t_lect.countRecords) { searchLect++; continue; }
                namePages = t_name.tableInStrings[namePage, 2];
                namePage++;
                searchLect++;
                b = true;

                //Сортируем страницы лекции в правильном порядке
                //- по полю prevpageid в таблице
                string p = ""; int n = 0;
                int ij = 0;
                while (ij < fifo.Count - 1)
                {
                    if (fifo[ij] > fifo[ij + 1])
                    {
                        n = fifo[ij + 1];
                        fifo[ij + 1] = fifo[ij];
                        fifo[ij] = n;
                        //----------------------
                        p = pages[ij + 1];
                        pages[ij + 1] = pages[ij];
                        pages[ij] = p;
                        //----------------------
                        p = title[ij + 1];
                        title[ij + 1] = title[ij];
                        title[ij] = p;
                        ij = 0;
                    }
                    else
                        ij++;
                }

                List<Paragraph> listParag = new List<Paragraph>();
                Paragraph parag;
                for (int j = 0; j < pages.Count; j++)
                {
                    //локальный вариант поиска путей к изображениям
                    //parag = new Paragraph(pages[j], title[j], fullPathDirectoryToMoodledata);

                    //вариант поиска путей к изображениям на FTP сервере
                    parag = new Paragraph(pages[j], title[j], listPaths);

                    listParag.Add(parag);
                }

                Lection lect = new Lection(long.Parse(t_name.tableInStrings[namePage-1, 0]), listParag, namePages);
                Lections.Add(lect);

                pages = new List<string>();
                fifo = new List<int>();
                title = new List<string>();
                namePages = "";
            }
        }

        //метод, использовался, когда локально тестировал проект
        string GetFullPathDirectoryToMoodledata()
        {
            Table t = new Table("mdl_config", Database_Host_User_Pass);
            string path = "";
            int numberColumn_name = 0;
            int numberColumn_value = 0;
            for (int i = 0; i < t.fieldTableInStrings.Length; i++)
            {
                if (t.fieldTableInStrings[0, i] == "name")
                    numberColumn_name = i;
                if (t.fieldTableInStrings[0, i] == "value")
                    numberColumn_value = i;
            }
            int numberRecord_geoipfile = 0;
            for (int i = 0; i < t.countRecords; i++)
            {
                if (t.tableInStrings[i, numberColumn_name] == "geoipfile")
                {
                    numberRecord_geoipfile = i;
                    break;
                }
            }
            path = t.tableInStrings[numberRecord_geoipfile, numberColumn_value];
            string[] mass = path.Split('\\', '/');
            path = "";
            /*for (int i = 0; i < mass.Length; i++)
            {
                path += mass[i] + "\\";
                if (mass[i] == "localhost")
                    break;
            }*/
            for (int i = 0; i < 3; i++)
            {
                path += mass[i] + "\\";
            }
            
            return GetPathToDirectory_moodledata(path);            
            //return path + "www\\moodle1\\moodledata\\";

            //return path + "moodledata\\3\\";
            //return path;
        }

        //метод рекурсивный, использовался, когда локально тестировал проект
        private string GetPathToDirectory_moodledata(string dir)
        {
            DirectoryInfo dr = new DirectoryInfo(dir);
            foreach(DirectoryInfo subdir in dr.GetDirectories())
            {
                if (subdir.Name == "moodledata") return subdir.FullName;
                else
                    GetPathToDirectory_moodledata(subdir.FullName);
            }
            return null;
        }

        public void CreateDirectoryForFiles(string path)
        {
            string activeDir = path+'/'+ @"dirforFiles"; //путь к директории в которой будут храниться файлы
            Directory.CreateDirectory(activeDir);
        }

    }
}