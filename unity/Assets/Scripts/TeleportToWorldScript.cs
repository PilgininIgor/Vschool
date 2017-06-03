using UnityEngine;

public class TeleportToWorldScript : MonoBehaviour
{
    public bool be_ready_to_receive;

    void OnTriggerEnter()
    {
        if (!be_ready_to_receive)
        {
//            PhotonNetwork.LoadLevel("world");
            Global.returning = true;
        }
        else
        {
            be_ready_to_receive = false;
        }
    }
}