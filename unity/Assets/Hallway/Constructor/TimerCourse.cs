using System;
using UnityEngine;

public class TimerCourse : MonoBehaviour
{
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
			var prev = Mathf.FloorToInt(Global.stat.timeSpent);
            Global.stat.timeSpent += Time.deltaTime; //прибавляем время, которое длится один кадр
            if (Mathf.FloorToInt(Global.stat.timeSpent) > prev)
                UpdateClock(); //если началась новая секунда, то надо обновить часы на мониторе
        }
    }

    private void UpdateClock()
    {
		var ts = TimeSpan.FromSeconds(Global.stat.timeSpent);
        TextTime.GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
    }
}
