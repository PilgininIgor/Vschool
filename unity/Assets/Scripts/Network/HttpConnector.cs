using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpConnector : MonoBehaviour
{
    //TODO move to config file
    //private const String SERVER_URL = "http://localhost:25565/ils2";
    private const String ServerUrl = "http://virtual.itschool.ssau.ru/";

    private const String CourseDataUrl = "/Render/UnityData";
    private const String StatUrl = "/Render/UnityStat";
    private const String SaveStatisticUrl = "/Render/UnitySave";
    private const String UnityListUrl = "/Render/UnityList";
    private const String UnitySaveRpgUrl = "/Render/UnitySaveRPG";
    private const String SaveGameAchievementUrl = "/Render/SaveGameAchievement";
    private const String GetGameAchievementsUrl = "/Render/GetGameAchievements";

    public WWW Get(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    public WWW Post(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<String, String> postArg in post)
        {
            form.AddField(postArg.Key, postArg.Value);
        }
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void SaveStatistic(string s)
    {
       	var parameters = new Dictionary<string, string>();
        parameters["s"] = s;
        var www = Post(ServerUrl + SaveStatisticUrl, parameters);
    }

    public void SaveRPG(string s)
    {
        var parameters = new Dictionary<string, string>();
        parameters["s"] = s;
        var www = Post(ServerUrl + UnitySaveRpgUrl, parameters);
    }

    public void LoadCoursesList()
    {
        var www = Get(ServerUrl + UnityListUrl);
        BootstrapParser bootstrapParser = GameObject.Find("Bootstrap").GetComponent<BootstrapParser>();
	    bootstrapParser.CourseConstructor(www.text);
    }

    public void LoadStat(string id)
    {
        var parameters = new Dictionary<string, string>();
        parameters["id"] = id;
        var www = Post(ServerUrl + StatUrl, parameters);
        StatisticParser statisticParser  = GameObject.Find("Bootstrap").GetComponent<StatisticParser>();
	    statisticParser.StatisticDisplay(www.text); 
    }

    public void LoadCourseData(string id)
    {
        var parameters = new Dictionary<string, string>();
        parameters["id"] = id;
        var www = Post(ServerUrl + CourseDataUrl,parameters);
        CourseSelection courseSelection = GameObject.Find("Hallway/Course Selection/CS_Screen").GetComponent<CourseSelection>();
        courseSelection.CourseDisplay(www.text);
    }

    public String GetUserName()
    {
        return "Student";
    }
}

