using UnityEngine;

public class tableJump : MonoBehaviour
{
    public GameObject Bootstrap;

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
