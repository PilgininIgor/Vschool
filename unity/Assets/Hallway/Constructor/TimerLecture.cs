using UnityEngine;

public class TimerLecture : MonoBehaviour
{
    public int theme_num;
    public int lec_num;
    private StatisticParser sp;
    private bool flag;
    public GameObject Bootstrap;

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
            sp.stat.timeSpent += Time.deltaTime;
            sp.stat.themesRuns[theme_num].timeSpent += Time.deltaTime;
            sp.stat.themesRuns[theme_num].lecturesRuns[lec_num].timeSpent += Time.deltaTime;
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
