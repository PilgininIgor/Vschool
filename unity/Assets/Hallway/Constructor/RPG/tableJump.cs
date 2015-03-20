using UnityEngine;

public class tableJump : MonoBehaviour
{
    GameObject Bootstrap;

    void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!rpgParser.RPG.tableJump)
        {
            rpgParser.Achievement("Прыжок на стол!\n+10 очков!", 10);
            rpgParser.RPG.tableJump = true;
        }
    }
}
