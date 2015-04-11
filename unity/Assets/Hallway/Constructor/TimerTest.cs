using UnityEngine;
using System.Collections;

public class TimerTest : MonoBehaviour
{
    public int theme_num;
    public int test_num;
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
            //sp.STAT.themesRuns[theme_num].testsRuns[test_num].timeSpent += Time.deltaTime;
            //print(theme_num.ToString()+" "+test_num.ToString()+" "+sp.STAT.themesRuns[theme_num].testsRuns[test_num].timeSpent.ToString());
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
