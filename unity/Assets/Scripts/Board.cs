using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
    //массивы с информацией о вопросах (одинаковый индекс - одинаковый вопрос)
    public string[] qText; //путь к картинке для вопроса либо 0, если таковой не предусмотрено

    public string[] //текст вопроса
        qPicPath; //путь к картинке для вопроса либо 0, если таковой не предусмотрено

    public int[] qType; //тип ответов: 0 - текстом, 1 - картинкой
    public string[] qAns; //ответы текстом (тип = 0) либо путь к картинке с ними (тип = 1)
    public int[] qAnsNum; //количество вариантов ответа

    public List<bool[]> a; //двумерный массив двоичных значений, запоминающий все ответы пользователя
    //a[i][j] = true/false - пользователь выбрал/не выбрал j-тый вариант ответа на i-тый вопрос
    public List<double> t = new List<double>();
    //одномерный массив секунд, которые считают время ответа на каждый вопрос

    //t[i] = 125.55 - i-тый вопрос был активен (выведен на стенд) в течение 2 минут и 5.55 секунд
    public int i = 0; //номер текущего вопроса (нумеруются с нуля)
    public float prev_time;
    public string test_id;
    public int theme_num;
    public int test_num;

    private GameObject Text_Header, CubeGroup1, CubeGroup2, CubeGroup3, CubeGroup4, CubeGroup5;

    //это функция инициализации, ее должен предварительно вызвать внешний скрипт
    public void generateTest(DataStructures.ThemeContent test, int theme_num1, int test_num1)
    {
        theme_num = theme_num1;
        test_num = test_num1;
        if (test.questions.Count == 0)
        {
            //если вопросов нет, то превращаем кнопку в обычную надпись, т.к.
            //рассчитаны на присутствие хотя бы одного полноценного вопроса
            //transform.parent.transform.parent.transform.Find("Board Begin/Button").GetComponent(TextMesh).text = "Why so empty?";
            Destroy(transform.parent.transform.parent.transform.Find("Board Begin/Button").GetComponent<BoxCollider>());
        }
        else
        {
            //если все нормально, то переписываем данные из иерархии во внутренние массивы
            //да, работать с той же иерархией по ссылкам, а не плодить переменные, было бы лучше,
            //но тестовая комната разрабатывалась заранее, а менять скрипты в надцатный раз ну совсем нехочеццо
            test_id = test.id;
            qAnsNum = new int[test.questions.Count];
            qAns = new String[test.questions.Count];
            qText = new String[test.questions.Count];
            for (var l = 0; l < test.questions.Count; l++)
            {
                qText[l] = DivideText(test.questions[l].text);
                qAnsNum[l] = test.questions[l].ans_count;
                if (test.questions[l].picQ == null) qPicPath[l] = "0";
                else qPicPath[l] = test.questions[l].picQ;
                if (test.questions[l].if_pictured)
                {
                    qType[l] = 1;
                    qAns[l] = test.questions[l].picA;
                }
                else
                {
                    qType[l] = 0;
                    qAns[l] = "";
                    for (var u = 0; u < qAnsNum[l] - 1; u++)
                        qAns[l] += test.questions[l].answers[u].text + "\n";
                    qAns[l] += test.questions[l].answers[qAnsNum[l] - 1].text;
                }
            }
            initializeArrays();
            UpdateBeginning();
        }
    }

    private string DivideText(string str)
    {
        var s = str.Split(" "[0]);
        var t = transform.Find("Text_Question").gameObject;
        var width = transform.Find("Plane_Board").transform.localScale.x * 10;
        var first = 0;
        var i = 0;

        while (i < s.Length - 1)
        {
            t.GetComponent<TextMesh>().text = "";
            for (var j = first; j <= i; j++) t.GetComponent<TextMesh>().text += s[j];
            t.GetComponent<TextMesh>().text += " " + s[i + 1];
            if (t.GetComponent<MeshRenderer>().bounds.size.x <= width)
            {
                s[i] += " ";
            }
            else
            {
                s[i] += "\n";
                first = i + 1;
            }
            i++;
        }

        t.GetComponent<TextMesh>().text = "";
        var res = "";
        for (i = 0; i < s.Length; i++) res += s[i];
        return res;
    }

    public void initializeArrays()
    {
        int j, k;
        a = new List<bool[]>();
        for (j = 0; j < qText.Length; j++)
            a.Add(new bool[qAnsNum[j]]);
        for (j = 0; j < qText.Length; j++)
            t.Add(0.00);
    }

    public void UpdateBeginning()
    {
        prev_time = Time.timeSinceLevelLoad;

        transform.Find("Text_Question").GetComponent<TextMesh>().text = qText[i];
        Text_Header.GetComponent<TextMesh>().text = "Вопрос №" + (i + 1);

        if (qPicPath[i] == "0") transform.Find("Plane_RightButton").renderer.enabled = false;
        else transform.Find("Plane_RightButton").renderer.enabled = true;

        switch (qAnsNum[i])
        {
            case 3:
                CubeGroup4.transform.Find("Cube4").renderer.enabled = false;
                CubeGroup5.transform.Find("Cube5").renderer.enabled = false;
                CubeGroup1.transform.localPosition.Set(-2, CubeGroup1.transform.localPosition.y,
                    CubeGroup1.transform.localPosition.z);
                CubeGroup2.transform.localPosition.Set(0, CubeGroup2.transform.localPosition.y,
                    CubeGroup2.transform.localPosition.z);
                CubeGroup3.transform.localPosition.Set(2, CubeGroup3.transform.localPosition.y,
                    CubeGroup3.transform.localPosition.z);
                break;
            case 4:
                CubeGroup4.transform.Find("Cube4").renderer.enabled = true;
                CubeGroup5.transform.Find("Cube5").renderer.enabled = false;
                CubeGroup1.transform.localPosition.Set(-3, CubeGroup1.transform.localPosition.y,
                    CubeGroup1.transform.localPosition.z);
                CubeGroup2.transform.localPosition.Set(-1, CubeGroup2.transform.localPosition.y,
                    CubeGroup2.transform.localPosition.z);
                CubeGroup3.transform.localPosition.Set(1, CubeGroup3.transform.localPosition.y,
                    CubeGroup3.transform.localPosition.z);
                CubeGroup4.transform.localPosition.Set(3, CubeGroup4.transform.localPosition.y,
                    CubeGroup4.transform.localPosition.z);
                break;
            case 5:
                CubeGroup4.transform.Find("Cube4").renderer.enabled = true;
                CubeGroup5.transform.Find("Cube5").renderer.enabled = true;
                CubeGroup1.transform.localPosition.Set(-4, CubeGroup1.transform.localPosition.y,
                    CubeGroup1.transform.localPosition.z);
                CubeGroup2.transform.localPosition.Set(-2, CubeGroup2.transform.localPosition.y,
                    CubeGroup2.transform.localPosition.z);
                CubeGroup3.transform.localPosition.Set(0, CubeGroup3.transform.localPosition.y,
                    CubeGroup3.transform.localPosition.z);
                CubeGroup4.transform.localPosition.Set(2, CubeGroup4.transform.localPosition.y,
                    CubeGroup4.transform.localPosition.z);
                CubeGroup5.transform.localPosition.Set(4, CubeGroup5.transform.localPosition.y,
                    CubeGroup5.transform.localPosition.z);
                break;
        }
    }

    private void UpdateQuestion()
    {
        UpdateBeginning();

        transform.Find("Plane_LeftButton").GetComponent<Left_Button>().looking_at_question = true;

        transform.Find("Plane_LeftButton").transform.Find("Answers").gameObject.active = true;
        transform.Find("Plane_LeftButton").transform.Find("Question").gameObject.active = false;
        transform.Find("Text_Question").renderer.enabled = true;
        transform.Find("Plane_Pic_Answers").renderer.enabled = false;

        if (a[i][0])
        {
            CubeGroup1.transform.Find("Cube1").animation.Play("CubeActiveUp");
            CubeGroup1.transform.Find("Cube1").GetComponent<Cube>().is_active = true;
        }
        else
        {
            CubeGroup1.transform.Find("Cube1").animation.Play("CubeInactiveUp");
            CubeGroup1.transform.Find("Cube1").GetComponent<Cube>().is_active = false;
        }
        if (a[i][1])
        {
            CubeGroup2.transform.Find("Cube2").animation.Play("CubeActiveUp");
            CubeGroup2.transform.Find("Cube2").GetComponent<Cube>().is_active = true;
        }
        else
        {
            CubeGroup2.transform.Find("Cube2").animation.Play("CubeInactiveUp");
            CubeGroup2.transform.Find("Cube2").GetComponent<Cube>().is_active = false;
        }
        if (a[i][2])
        {
            CubeGroup3.transform.Find("Cube3").animation.Play("CubeActiveUp");
            CubeGroup3.transform.Find("Cube3").GetComponent<Cube>().is_active = true;
        }
        else
        {
            CubeGroup3.transform.Find("Cube3").animation.Play("CubeInactiveUp");
            CubeGroup3.transform.Find("Cube3").GetComponent<Cube>().is_active = false;
        }
        if (qAnsNum[i] > 3)
        {
            if (a[i][3])
            {
                CubeGroup4.transform.Find("Cube4").animation.Play("CubeActiveUp");
                CubeGroup4.transform.Find("Cube4").GetComponent<Cube>().is_active = true;
            }
            else
            {
                CubeGroup4.transform.Find("Cube4").animation.Play("CubeInactiveUp");
                CubeGroup4.transform.Find("Cube4").GetComponent<Cube>().is_active = false;
            }
        }
        if (qAnsNum[i] == 5)
        {
            if (a[i][4])
            {
                CubeGroup5.transform.Find("Cube5").animation.Play("CubeActiveUp");
                CubeGroup5.transform.Find("Cube5").GetComponent<Cube>().is_active = true;
            }
            else
            {
                CubeGroup5.transform.Find("Cube5").animation.Play("CubeInactiveUp");
                CubeGroup5.transform.Find("Cube5").GetComponent<Cube>().is_active = false;
            }
        }
    }
}
