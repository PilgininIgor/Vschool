using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpConnector : MonoBehaviour
{
    //TODO move to config file
    public static readonly String ServerUrl = "http://localhost:63866";
    //private const readonly String ServerUrl = "http://virtual.itschool.ssau.ru";

    public static readonly String CourseDataUrl = "/Render/UnityData";
    public static readonly String StatUrl = "/Render/UnityStat";
    public static readonly String SaveStatisticUrl = "/Render/UnitySave";
    public static readonly String UnityListUrl = "/Render/UnityList";
    public static readonly String UnitySaveRpgUrl = "/Render/UnitySaveRPG";
    public static readonly String SaveGameAchievementUrl = "/Render/SaveGameAchievement";
    public static readonly String GetGameAchievementsUrl = "/Render/GetGameAchievements";

    public void Get(string url, System.Action<WWW> onSuccess)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onSuccess));
    }

    public void Post(string url, Dictionary<string, string> post, System.Action<WWW> onSuccess)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<String, String> postArg in post)
        {
            form.AddField(postArg.Key, postArg.Value);
        }
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, onSuccess));
    }

    private IEnumerator WaitForRequest(WWW www, System.Action<WWW> onSuccess)
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

    public void SaveStatistic(string s)
    {
//       	var parameters = new Dictionary<string, string>();
//        parameters["s"] = s;
//        var www = Post(ServerUrl + SaveStatisticUrl, parameters);
    }

    public void SaveRPG(string s)
    {
//        var parameters = new Dictionary<string, string>();
//        parameters["s"] = s;
//        var www = Post(ServerUrl + UnitySaveRpgUrl, parameters);
    }

    public void LoadCoursesList()
    {
//        var www = Get(ServerUrl + UnityListUrl);
//        BootstrapParser bootstrapParser = GameObject.Find("Bootstrap").GetComponent<BootstrapParser>();
//	    bootstrapParser.CourseConstructor(www.text);
    }

    public void LoadStat(string id)
    {
//        var parameters = new Dictionary<string, string>();
//        parameters["id"] = id;
//        var www = Post(ServerUrl + StatUrl, parameters);
//        StatisticParser statisticParser  = GameObject.Find("Bootstrap").GetComponent<StatisticParser>();
//	    statisticParser.StatisticDisplay(www.text); 
    }

    public void LoadCourseData(string id)
    {
//        var parameters = new Dictionary<string, string>();
//        parameters["id"] = id;
//        var www = Post(ServerUrl + CourseDataUrl,parameters);
//        CourseSelection courseSelection = GameObject.Find("Hallway/Course Selection/CS_Screen").GetComponent<CourseSelection>();
//        courseSelection.CourseDisplay(www.text);
    }

    public String GetUserName()
    {
        return "Student";
    }
}

