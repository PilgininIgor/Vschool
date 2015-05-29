using UnityEngine;
using System.Collections;

public class Check_result : MonoBehaviour {
	
	bool taskSolved = false;
	string[] digits = new string[12];
	public TextMesh taskText1;
	public TextMesh taskText2;
	public TextMesh resultText;
	string answer;
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
			string correctAnswer = GameObject.Find("TriggerStart").GetComponent<Game_start>().GetResult();
			if (answer == correctAnswer)
			{
				taskSolved = true;
				taskText1.text = "Верно!";
				taskText2.text = "Попыток: " + attempts;
				switch (attempts)
				{
				case 1: resultText.text = "Оценка: 5"; break;
				case 2: resultText.text = "Оценка: 4";; break;
				case 3: resultText.text = "Оценка: 3"; break;
				default: break;
				}
			}
			else
			{
				if (attempts == 1)
				{
					resultText.text = "Осталось 2 попытки!";
				}
				if (attempts == 2)
				{
					resultText.text = "Осталась 1 попытка!";
				}
				if (attempts == 3)
				{
					taskText1.text = "Не осталось попыток!";
					taskText2.text = "Верный ответ: " + correctAnswer; 
					resultText.text = "Оценка: 2";
					taskSolved = true;
				}
			}
		}
	}
	
}
