using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckResult3 : MonoBehaviour {

    bool taskSolved = false;
    public TextMesh taskText1;
    public TextMesh taskText2;
    public TextMesh taskText3;
    public TextMesh taskText4;
    public TextMesh taskText5;
    int numberOfTries = 0;

    public bool TaskSolved()
    {
        return taskSolved;
    }

    public void Check()
    {
        if (!taskSolved)
        {
            numberOfTries++;
            
            GameObject[] cyls = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().instCylindersRight;
            bool check = true;
            for (int i = 0; i < cyls.Length; i++)
            {
                if (cyls[i] != null)
                {
                    if (cyls[i].name != "cyl" + (i + 1))
                    {
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            }

            if (check)
            {
                int optimal = (int)Mathf.Pow((float)2, (float)cyls.Length) - 1;
                int numberOfTurns = GameObject.Find("Main Camera").GetComponent<TakingCylinder>().GetNumberOfTurns();
                int limitOf5 = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().GetLimitOf5();
                int limitOf4 = GameObject.Find("TriggerStart").GetComponent<Game3_Start>().GetLimitOf4();

                if (numberOfTurns - optimal <= limitOf5)
                {
                    taskText1.text = "Задание выполнено!";
                    taskText2.text = "Вы решили задание за";
                    taskText3.text = "оптимальное число шагов.";
                    taskText4.text = "Оценка: 5";
                    taskText5.text = "";
                }
                if (numberOfTurns - optimal > limitOf5 && numberOfTurns - optimal <= limitOf5 + limitOf4)
                {
                    taskText1.text = "Задание выполнено!";
                    taskText2.text = "Вы решили задание за";
                    taskText3.text = "число шагов, близкое";
                    taskText4.text = "к оптимальному.";
                    taskText5.text = "Оценка: 4";
                }
                if (numberOfTurns - optimal > limitOf5 + limitOf4)
                {
                    taskText1.text = "Задание выполнено!";
                    taskText2.text = "Вы решили задание за";
                    taskText3.text = "число шагов, значительно";
                    taskText4.text = "превышающее оптимальное.";
                    taskText5.text = "Оценка: 3";
                }

                taskSolved = true;

                var parameters = new Dictionary<string, string>();
                parameters["id"] = Global.content.id;
                parameters["turns_str"] = numberOfTurns.ToString();

                var httpConnector = GameObject.Find("buttonOK").GetComponent<HttpConnector>();
                httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.CheckTask3Url, parameters, www =>
                {
                });
            }
            else
            {
                if (numberOfTries == 1)
                {
                    taskText5.text = "Осталось 2 попытки.";
                }
                if (numberOfTries == 2)
                {
                    taskText5.text = "Осталась 1 попытка.";
                }
                if (numberOfTries == 3)
                {
                    taskText4.text = "Задание провалено.";
                    taskText5.text = "Оценка: 2";
                    taskSolved = true;

                    var parameters = new Dictionary<string, string>();
                    parameters["id"] = Global.content.id;
                    parameters["turns_str"] = "-1";

                    var httpConnector = GameObject.Find("buttonOK").GetComponent<HttpConnector>();
                    httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.CheckTask3Url, parameters, www =>
                    {
                    });
                }
            }
        }
    }
}
