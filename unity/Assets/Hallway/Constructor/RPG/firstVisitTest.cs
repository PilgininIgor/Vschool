using UnityEngine;

public class firstVisitTest : MonoBehaviour
{
    GameObject Bootstrap;
    void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!rpgParser.RPG.firstVisitTest)
        {
            rpgParser.Achievement("Первое посещение комнаты тестирования!\n+10 очков!", 10);
            rpgParser.RPG.firstVisitTest = true;
        }
    }
}
