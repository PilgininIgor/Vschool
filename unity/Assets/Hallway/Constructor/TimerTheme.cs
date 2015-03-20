using System;
using UnityEngine;

public class TimerTheme : MonoBehaviour
{
    public int theme_num;

    private StatisticParser sp;
    private bool flag = false;
    GameObject TextTime;
    GameObject Bootstrap;
    // Use this for initialization
    void Start()
    {
        sp = Bootstrap.GetComponent<StatisticParser>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            var tr = sp.stat.themesRuns[theme_num];
            var prev = Mathf.FloorToInt(tr.timeSpent);
            //наращиваем как время этой темы, так и время всего курса
            sp.stat.timeSpent += Time.deltaTime;
            tr.timeSpent += Time.deltaTime;
            if (Mathf.FloorToInt(tr.timeSpent) > prev) UpdateClock();
        }
    }

    private void UpdateClock()
    {
        var tr = sp.stat.themesRuns[theme_num];
        var ts = TimeSpan.FromSeconds(tr.timeSpent);
        TextTime.GetComponent<TextMesh>().text = ts.ToString("hh:mm:ss");
    }

    void OnTriggerEnter()
    {
        flag = true;
        UpdateClock();
    }

    void OnTriggerExit()
    {
        flag = false;
    }
}
