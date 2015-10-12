using UnityEngine;

public class ReturnToCourse : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (Global.returning)
        {
            BootstrapParser bootstrapParser = GameObject.Find("Bootstrap").GetComponent<BootstrapParser>();
            bootstrapParser.CourseConstructor(Global.course_json);
            var TeleportBoothArray = GameObject.FindGameObjectsWithTag("TeleportBooth");
            for (var i = 0; i < TeleportBoothArray.Length; i++)
            {
                if (TeleportBoothArray[i].transform.position == Global.teleportBoothPos)
                {
                    TeleportBoothArray[i].GetComponent<TeleportToSceneScript>().be_ready_to_receive = true;
                    GameObject Player = GameObject.Find("MainCamera").GetComponent<OrbitCam>().player;
                    Player.transform.position = TeleportBoothArray[i].transform.position;
                    break;
                }
            }
            Global.returning = false;
        }
    }
}
