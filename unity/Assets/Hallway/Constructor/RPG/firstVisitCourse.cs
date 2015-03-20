using UnityEngine;

public class firstVisitCourse : MonoBehaviour
{
    GameObject Bootstrap;
    void OnTriggerEnter()
    {
        StatisticParser stParser = Bootstrap.GetComponent<StatisticParser>();
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!stParser.stat.visited)
        {
            rpgParser.Achievement("Первое посещение курса!\n+10 очков!", 10);
            stParser.stat.visited = true;
            stParser.Save();
        }
    }
}
