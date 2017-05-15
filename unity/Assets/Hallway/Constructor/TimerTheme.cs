using System;
using UnityEngine;

public class TimerTheme : MonoBehaviour
{
    public int theme_num;

    private bool flag = true;
    public GameObject TextTime;

    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
			var tr = Global.stat.themesRuns[Global.theme_num];
            var prev = Mathf.FloorToInt(tr.timeSpent);
            //наращиваем как время этой темы, так и время всего курса
			Global.stat.timeSpent += Time.deltaTime;
            tr.timeSpent += Time.deltaTime;
            if (Mathf.FloorToInt(tr.timeSpent) > prev) UpdateClock();
        }
    }

    private void UpdateClock()
    {
		var tr = Global.stat.themesRuns[Global.theme_num];
        var ts = TimeSpan.FromSeconds(tr.timeSpent);
        TextTime.GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
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
