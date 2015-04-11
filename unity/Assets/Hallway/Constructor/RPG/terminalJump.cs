using UnityEngine;

public class terminalJump : MonoBehaviour
{
    public GameObject Bootstrap;

    void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!rpgParser.RPG.terminalJump)
        {
            rpgParser.Achievement("Прыжок на терминал!\n+10 очков!", 10);
            rpgParser.RPG.terminalJump = true;
        }
    }
}
