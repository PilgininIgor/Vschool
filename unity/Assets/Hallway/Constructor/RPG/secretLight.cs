using UnityEngine;

public class secretLight : MonoBehaviour
{
    public GameObject HallwayGroup;

    void OnTriggerEnter()
    {
        HallwayGroup.GetComponent<HallwayAchievements>().Check(name, "");
    }
}
