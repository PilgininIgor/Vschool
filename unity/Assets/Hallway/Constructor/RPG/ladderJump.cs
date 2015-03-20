using UnityEngine;

public class ladderJump : MonoBehaviour
{

    GameObject Bootstrap, HallwayGroup;

    void OnTriggerEnter()
    {
        HallwayGroup.GetComponent<HallwayAchievements>().Check(name, "");
    }
}
