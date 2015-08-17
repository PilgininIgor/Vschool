using UnityEngine;

public class barrelJump : MonoBehaviour
{

    public GameObject Bootstrap;
    private void OnTriggerEnter()
    {
        RPGParser rpgParser = Bootstrap.GetComponent<RPGParser>();
        if (!rpgParser.RPG.barrelRoll)
        {
//            rpgParser.Achievement("Сделана бочка!\n+10 очков!", 10);
//            rpgParser.RPG.barrelRoll = true;
        }
    }
}
