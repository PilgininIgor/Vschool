using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckResult2 : MonoBehaviour {
	
	bool taskSolved = false;
	public TextMesh taskText1;
	public TextMesh taskText2;
	public TextMesh taskText3;
	public TextMesh taskText4;
	public TextMesh taskText5;
	public TextMesh taskText6;
	public TextMesh taskText7;
    public Texture textureOff;
    public Texture textureOn;
    public Texture textureFalse;

	public bool TaskSolved()
	{
		return taskSolved;
	}
	
	public void Check()
	{
		if (!taskSolved) 
		{
			string[] answer = GameObject.Find ("insert").GetComponent<InsertItem> ().GetAnswer ();
			for (int i=0; i<answer.Length; i++) {
					switch (answer [i]) {
					case "AND":
							answer [i] = "c";
							break;
					case "OR":
							answer [i] = "d";
							break;
					case "IMPL":
							answer [i] = "i";
							break;
					case "EQUIV":
							answer [i] = "e";
							break;
					}
			}

			var parameters = new Dictionary<string, string>();
			parameters["el1"] = answer[0];
			parameters["el2"] = answer[1];
			parameters["el3"] = answer[2];
			parameters["el4"] = answer[3];
            //taskText7.text = answer[0] + ", " + answer[1] + ", " + answer[2] + ", " + answer[3]; //проверка
			var httpConnector = GameObject.Find ("buttonOK").GetComponent<HttpConnector> ();
			httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.CheckTask2Url, parameters, www =>
			{
				ProcessResult(www.text);
			});
		}
	}

    private void ProcessResult(string resultStr)
    {
        resultStr = resultStr.Replace("\"", "");
        string[] result = resultStr.Split(',');
        bool answerIsCorrect = "true" == result[0].ToLower();
        int attemptsNumber = int.Parse(result[1]);
			
		if (answerIsCorrect)
		{
			taskSolved = true;
            StartCoroutine(ColorLinks(false, textureOn, textureOff));
			taskText5.text = "Верно!";
            taskText6.text = "Попыток: " + attemptsNumber;
			taskText1.text = "Выполнение задания";
			taskText2.text = "закончено,";
			taskText3.text = "результат сохранен.";
			taskText4.text = "";
            switch (attemptsNumber)
			{
			case 1: taskText7.text = "Оценка: 5"; break;
			case 2: taskText7.text = "Оценка: 4";; break;
			case 3: taskText7.text = "Оценка: 3"; break;
			default: break;
			}
		}
		else
		{
            if (attemptsNumber == 1)
			{
                StartCoroutine(ColorLinks(true,textureFalse, textureOff));
				taskText5.text = "Ошибка!";
				taskText6.text = "Осталось 2 попытки!";
			}
            if (attemptsNumber == 2)
			{
                StartCoroutine(ColorLinks(true, textureFalse, textureOff));
				taskText5.text = "Ошибка!";
				taskText6.text = "Осталась 1 попытка!";
			}
            if (attemptsNumber == 3)
			{
                StartCoroutine(ColorLinks(false, textureFalse, textureOff));
				taskText5.text = "Не осталось попыток!";
				taskText6.text = "Оценка: 2"; 
				taskSolved = true;
			}
		}
	}

    private IEnumerator ColorLinks(bool change, Texture texture1, Texture texture2)
    {
        GameObject[] links = GameObject.Find("TriggerStart").GetComponent<Game2_Start>().links;
        for (int i = 0; i < links.Length; i++)
        {
            if (links[i] != null)
            {
                links[i].renderer.material.mainTexture = texture1;                
            }
        }

        yield return new WaitForSeconds(1);

        if (change)
        {  
            for (int i = 0; i < links.Length; i++)
            {
                if (links[i] != null)
                {
                    links[i].renderer.material.mainTexture = texture2;
                }
            }
        }
    }

}