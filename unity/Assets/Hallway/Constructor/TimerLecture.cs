using UnityEngine;

public class TimerLecture : MonoBehaviour
{
    public int theme_num;
    public int lec_num;
    private bool flag;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
			Global.stat.timeSpent += Time.deltaTime;
			Global.stat.themesRuns[theme_num].timeSpent += Time.deltaTime;
			Global.stat.themesRuns[theme_num].lecturesRuns[lec_num].timeSpent += Time.deltaTime;
        }
    }

    void OnTriggerEnter()
    {
        flag = true;
    }

    void OnTriggerExit()
    {
        flag = false;
    }
}
