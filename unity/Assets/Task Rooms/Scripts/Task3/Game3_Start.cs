using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using JsonFx.Json;
public class Game3_Start : MonoBehaviour
{

    bool gameStarted = false;
    public GameObject panelLeft;
    public GameObject[] cylinders;
    public GameObject[] instCylindersLeft, instCylindersCenter, instCylindersRight;
    public int highestLeft, highestCenter, highestRight;

    int limitOf5, limitOf4;

    void OnTriggerEnter(Collider other)
    {
        if (other.collider.tag == "Player" && !gameStarted)
        {
            gameStarted = true;

            var parameters = new Dictionary<string, string>();
            parameters["id"] = Global.content.id;

            var httpConnector = GameObject.Find("TriggerStart").GetComponent<HttpConnector>();
            httpConnector.Post(HttpConnector.ServerUrl + HttpConnector.GetTask3Url, parameters, www =>
            {
                StartTask3(www.text);
            });
        }
    }

    void StartTask3(string taskStr)
    {
        taskStr = taskStr.Replace("\"", "");
        string[] taskStrArr = taskStr.Split(',');
        int n = int.Parse(taskStrArr[0]);
        limitOf5 = int.Parse(taskStrArr[1]);
        limitOf4 = int.Parse(taskStrArr[2]);
        instCylindersLeft = new GameObject[n];
        instCylindersCenter = new GameObject[n];
        instCylindersRight = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            instCylindersLeft[i] = Instantiate(cylinders[i], transform.position, transform.rotation) as GameObject;
            instCylindersLeft[i].name = "cyl" + (i + 1);
            instCylindersLeft[i].transform.parent = panelLeft.transform;
            instCylindersLeft[i].transform.localPosition = new Vector3(0, 1.2f * (i + 1), 0);
            instCylindersLeft[i].transform.Rotate(new Vector3(0, 180, 0));
        }
        highestLeft = n;
        highestCenter = 0;
        highestRight = 0;
    }

    public int GetLimitOf5()
    {
        return limitOf5;
    }

    public int GetLimitOf4()
    {
        return limitOf4;
    }
}
