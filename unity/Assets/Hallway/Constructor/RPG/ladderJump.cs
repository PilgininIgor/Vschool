using UnityEngine;

public class ladderJump : MonoBehaviour
{

    public GameObject Bootstrap, HallwayGroup;

    void OnTriggerEnter()
    {
        HallwayGroup.GetComponent<HallwayAchievements>().Check(name, "");
    }
}
