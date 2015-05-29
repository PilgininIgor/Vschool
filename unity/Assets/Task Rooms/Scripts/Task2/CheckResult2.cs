using UnityEngine;
using System.Collections;

public class CheckResult2 : MonoBehaviour {
	
	bool taskSolved = false;
	string[] answer;
	public TextMesh taskText1;
	public TextMesh taskText2;
	public TextMesh taskText3;
	public TextMesh taskText4;
	public TextMesh taskText5;
	public TextMesh taskText6;
	public TextMesh taskText7;
	int attempts = 0;
	
	public bool TaskSolved()
	{
		return taskSolved;
	}
	
	public void Check()
	{
		if (!taskSolved)
		{
			attempts++;
			
			answer = GameObject.Find("insert").GetComponent<InsertItem>().GetAnswer();
			for (int i=0; i<answer.Length; i++)
			{
				switch(answer[i])
				{
				case "AND": answer[i] = "c"; break;
				case "OR": answer[i] = "d"; break;
				case "IMPL": answer[i] = "i"; break;
				case "EQUIV": answer[i] = "e"; break;
				}
			}

			taskText7.text = answer[0] + ", " + answer[1] + ", " + answer[2] + ", " + answer[3]; //проверка

			bool answerIsCorrect = false;
			
			if (answerIsCorrect)
			{
				taskSolved = true; 
				taskText5.text = "Верно!";
				taskText6.text = "Попыток: " + attempts;
				taskText1.text = "Выполнение задания";
				taskText2.text = "закончено,";
				taskText3.text = "результат сохранен.";
				taskText4.text = "";
				switch (attempts)
				{
				case 1: taskText7.text = "Оценка: 5"; break;
				case 2: taskText7.text = "Оценка: 4";; break;
				case 3: taskText7.text = "Оценка: 3"; break;
				default: break;
				}
			}
			else
			{
				if (attempts == 1)
				{
					taskText5.text = "Ошибка!";
					taskText6.text = "Осталось 2 попытки!";
				}
				if (attempts == 2)
				{
					taskText5.text = "Ошибка!";
					taskText6.text = "Осталась 1 попытка!";
				}
				if (attempts == 3)
				{
					taskText5.text = "Не осталось попыток!";
					taskText6.text = "Оценка: 2"; 
					taskSolved = true;
				}
			}
		}
	}
	
}