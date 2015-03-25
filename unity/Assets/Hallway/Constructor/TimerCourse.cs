using System;
using UnityEngine;

public class TimerCourse : MonoBehaviour
{
    private StatisticParser sp;
    private bool flag = false;
    public GameObject TextTime;
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
            var prev = Mathf.FloorToInt(sp.stat.timeSpent);
            sp.stat.timeSpent += Time.deltaTime; //прибавляем время, которое длится один кадр
            if (Mathf.FloorToInt(sp.stat.timeSpent) > prev)
                UpdateClock(); //если началась новая секунда, то надо обновить часы на мониторе
        }
    }

    private void UpdateClock()
    {
        var ts = TimeSpan.FromSeconds(sp.stat.timeSpent);
        TextTime.GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
    }
}
