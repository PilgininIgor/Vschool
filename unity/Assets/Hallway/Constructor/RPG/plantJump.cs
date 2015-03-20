using UnityEngine;

public class plantJump : MonoBehaviour
{
    GameObject Bootstrap;
    bool triggered;

    void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!triggered)
        {
            triggered = true;
            if (!rpgParser.RPG.plantJump_First)
            {
                rpgParser.Achievement("Прыжок на цветы!\n+10 очков!", 10);
                rpgParser.RPG.plantJump_First = true;
            }
            else if (!rpgParser.RPG.plantJump_Second)
            {
                rpgParser.Achievement("Еще прыжок на цветы!\n+10 очков!", 10);
                rpgParser.RPG.plantJump_Second = true;
            }
        }
    }
}
