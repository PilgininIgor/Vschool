using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpConnector : MonoBehaviour
{
    //TODO move to config file
    public const string ServerUrl = "http://localhost:63866";
    //public const string ServerUrl = "http://virtual.itschool.ssau.ru";
    //public const string ServerUrl = "https://virtual.itschool.ssau.ru";

    public const string CourseDataUrl = "/Render/UnityData";
    public const string StatUrl = "/Render/UnityStat";
    public const string SaveStatisticUrl = "/Render/UnitySave";
    public const string UnityListUrl = "/Render/UnityList";
    public const string UnitySaveRpgUrl = "/Render/UnitySaveRPG";
    public const string GetUsernameUrl = "/Render/GetUserName";
    public const string SaveGameAchievementUrl = "/Render/SaveGameAchievement";
    public const string GetUserCoinsUrl = "/Render/GetUserCoinsUrl";
	public const string GetTask2Url = "/Task/GetTask2";
	public const string CheckTask2Url = "/Task/CheckTask2";
    public const string GetTask1Url = "/Task/GetTask1";
    public const string CheckTask1Url = "/Task/CheckTask1";
    public const string GetTask3Url = "/Task/GetTask3";
    public const string CheckTask3Url = "/Task/CheckTask3";

    public void Get(string url, Action<WWW> onSuccess)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onSuccess));
    }

    public void Post(string url, Dictionary<string, string> post, Action<WWW> onSuccess)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<String, String> postArg in post)
        {
            form.AddField(postArg.Key, postArg.Value);
        }
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, onSuccess));
    }

    private IEnumerator WaitForRequest(WWW www, Action<WWW> onSuccess)
    {
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            onSuccess(www);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}

