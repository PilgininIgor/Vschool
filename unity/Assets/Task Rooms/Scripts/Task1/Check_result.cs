using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Check_result : MonoBehaviour {
	
	bool taskSolved = false;
	string[] digits = new string[12];
	public TextMesh taskText1;
	public TextMesh taskText2;
	public TextMesh resultText;
	
	public bool TaskSolved()
	{
		return taskSolved;
	}
	
	public void Check()
	{
		if (!taskSolved)
		{
            string answer = "";

			for (int i=0; i < digits.Length; i++)
			{
				digits[i] = GameObject.Find("cube_detector"+(i+1).ToString()).GetComponent<Cube_detection>().GetDigit();
			}
			string digit = digits[0];
			answer = digit;
			int k = 1;
			while (digit != "-" && k < digits.Length)
			{
				digit = digits[k];
				if (digit != "-")
				{
					answer += digit;
				}
				k++;
			}

			//string correctAnswer = GameObject.Find("TriggerStart").GetComponent<Game_start>().GetResult();

            var parameters = new Dictionary<string, string>();
			parameters["id"] = Global.content.id;
            parameters["answer"] = answer;

            var httpConnector = GameObject.Find("buttonOK").GetComponent<HttpConnector>();
            httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.CheckTask1Url, parameters, www =>
            {
                ProcessResult(www.text);
            });
		}
	}

    private void ProcessResult(string resultStr)
    {
        resultStr = resultStr.Replace("\"","");
        string[] result = resultStr.Split(',');
        bool answerIsCorrect = "true" == result[0].ToLower();
        int attemptsNumber = int.Parse(result[2]);

        if (answerIsCorrect)
        {
            taskSolved = true;
            taskText1.text = "Верно!";
            taskText2.text = "Попыток: " + attemptsNumber;
            switch (attemptsNumber)
            {
                case 1: resultText.text = "Оценка: 5"; break;
                case 2: resultText.text = "Оценка: 4"; ; break;
                case 3: resultText.text = "Оценка: 3"; break;
                default: break;
            }
        }
        else
        {
            if (attemptsNumber == 1)
            {
                resultText.text = "Осталось 2 попытки!";
            }
            if (attemptsNumber == 2)
            {
                resultText.text = "Осталась 1 попытка!";
            }
            if (attemptsNumber == 3)
            {
                taskText1.text = "Не осталось попыток!";
                taskText2.text = "Верный ответ: " + result[1];
                resultText.text = "Оценка: 2";
                taskSolved = true;
            }
        }
    }
	
}
