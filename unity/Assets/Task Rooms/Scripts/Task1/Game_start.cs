using UnityEngine;
using System.Collections;
using System;

public class Game_start : MonoBehaviour {
	
	bool gameStarted = false;
	public TextMesh taskText1, taskText2;
	int task, scale, scale2, number1, number2;
	string operation;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.collider.tag == "Player" && !gameStarted)
		{
			gameStarted = true;
			
			System.Random rnd = new System.Random();
			
			task = rnd.Next(0,2);
			if (task == 0)
			{
				scale = rnd.Next(0,3);
				switch (scale)
				{
				case 0: scale = 2; break;
				case 1: scale = 8; break;
				case 2: scale = 16; break;
				default: break;
				}
				int _operation = rnd.Next(0,2);
				switch (_operation)
				{
				case 0: operation = "+"; break;
				case 1: operation = "-"; break;
				case 2: operation = "*"; break;
				default: break;
				}
				number1 = rnd.Next(10,51);
				do
				{
					number2 = rnd.Next(10,51);
				}
				while (number1 == number2);
				if (number2 > number1)
				{
					int b = number2;
					number2 = number1;
					number1 = b;
				}
				taskText1.text = "Действие в " + scale + " СС:";
				taskText2.text = Convert.ToString(number1, scale).ToUpper() + " " + operation + " " + Convert.ToString(number2, scale).ToUpper();
			}
			else if (task == 1)
			{
				scale = rnd.Next(0,4);
				switch (scale)
				{
				case 0: scale = 2; break;
				case 1: scale = 8; break;
				case 2: scale = 10; break;
				case 3: scale = 16; break;
				default: break;
				}
				do
				{
					scale2 = rnd.Next(0,4);
					switch (scale2)
					{
					case 0: scale2 = 2; break;
					case 1: scale2 = 8; break;
					case 2: scale2 = 10; break;
					case 3: scale2 = 16; break;
					default: break;
					}
				}
				while (scale == scale2);
				number1 = rnd.Next(10,51);
				taskText1.text = "Из " + scale + " СС в " + scale2 + " СС:";
				taskText2.text = Convert.ToString(number1, scale).ToUpper();
			}
		}
	}
	
	public string GetResult()
	{
		if (task == 0)
		{
			int result = 0;
			switch (operation)
			{
			case "+": result = number1 + number2; break;
			case "-": result = number1 - number2; break;
			case "*": result = number1 * number2; break;
			default: break;
			}
			return Convert.ToString(result, scale).ToUpper();
		}
		else if (task == 1)
		{
			return Convert.ToString(number1, scale2).ToUpper();
		}
		return "";
	}
	
}

