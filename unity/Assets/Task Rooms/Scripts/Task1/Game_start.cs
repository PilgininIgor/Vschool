using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JsonFx.Json;

public class Game_start : MonoBehaviour {
	
	bool gameStarted = false;
	public TextMesh taskText1, taskText2;
	int task, scale, scale2, number1, number2;
	string operation;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.GetComponent<Collider>().tag == "Player" && !gameStarted)
		{
			gameStarted = true;

            var parameters = new Dictionary<string, string>();
            parameters["id"] = Global.content.id;

            var httpConnector = GameObject.Find("TriggerStart").GetComponent<HttpConnector>();
			httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.GetTask1Url, parameters, www =>
            {
                StartTask1(www.text);
				},
				w=>{});
		}
	}

    void StartTask1(string taskStr)
    {
        taskStr = taskStr.Replace("\"", "");
        string[] task = taskStr.Split(',');
        taskText1.text = task[0];
        taskText2.text = task[1]; 
    }

}

