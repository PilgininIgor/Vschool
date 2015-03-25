using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpConnector : MonoBehaviour
{
    public WWW GET(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post)
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
        throw new NotImplementedException();
    }

    public void SaveRPG(string s)
    {
        throw new NotImplementedException();
    }

    public void LoadCoursesList()
    {
        throw new NotImplementedException();
    }

    public void LoadCourseData(string id)
    {
        throw new NotImplementedException();
    }
}

