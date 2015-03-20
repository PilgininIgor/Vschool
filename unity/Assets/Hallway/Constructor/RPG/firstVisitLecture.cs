using UnityEngine;
using System.Collections;

public class firstVisitLecture : MonoBehaviour
{

    GameObject Bootstrap;

    void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!rpgParser.RPG.firstVisitLecture)
        {
            rpgParser.Achievement("Первое посещение лекционного зала!\n+10 очков!", 10);
            rpgParser.RPG.firstVisitLecture = true;
        }
    }
}
