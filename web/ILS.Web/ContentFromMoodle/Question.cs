using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILS.Web.ContentFromMoodle
{
    public class Question
    {
        public Question(int id_question, string text, string type, bool randomYesNo,
                        int category, string pathToPicture, List<AnswerOnQuestion> answersOnQuestion, string namePicture)
        {
            this.id_question = id_question;
            this.text = text;
            this.type = type;
            this.randomYesNo = randomYesNo;
            this.category = category;
            this.pathToPicture = pathToPicture;
            this.answersOnQuestion = answersOnQuestion;
            this.namePicture = namePicture;
        }

        //Тип вопроса(A1, A2...)
        public string type { get; set; }
        //ID вопроса в БД
        public int id_question { get; set; }
        //Текст вопроса
        public string text { get; set; }
        //Является ли вопрос рандомным или 
        //простым(множественный выбор)
        public bool randomYesNo { get; set; }
        //Поле - определяет к какой категории относится вопрос
        public int category { get; set; }
        //Картинка прикрепленные к вопросу
        //поле содержит путь откуда будет
        //скачиваться картинка(картинка одна,
        //т.к в LMS Moodle к вопросу теста 
        //vможно добавить только одну картинку)
        public string pathToPicture { get; set; }
        public string namePicture { get; set; }
        //Варианты ответов на вопрос
        public List<AnswerOnQuestion> answersOnQuestion { get; set; }
    }
}