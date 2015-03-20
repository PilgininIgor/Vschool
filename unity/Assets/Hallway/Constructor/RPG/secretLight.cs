using UnityEngine;

public class secretLight : MonoBehaviour
{
    GameObject HallwayGroup;

    void OnTriggerEnter()
    {
        HallwayGroup.GetComponent<HallwayAchievements>().Check(name, "");
    }
}
