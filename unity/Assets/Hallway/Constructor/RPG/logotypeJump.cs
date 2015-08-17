using UnityEngine;

public class logotypeJump : MonoBehaviour
{
    public GameObject Bootstrap;
    void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!rpgParser.RPG.logotypeJump)
        {
//            rpgParser.Achievement("Прыжок в логотип!\n+10 очков!", 10);
//            rpgParser.RPG.logotypeJump = true;
        }
    }
}
