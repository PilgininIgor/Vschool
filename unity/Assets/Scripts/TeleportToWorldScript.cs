using UnityEngine;

public class TeleportToWorldScript : MonoBehaviour
{
    void OnTriggerEnter()
    {
        PhotonNetwork.LoadLevel("world");
        Global.returning = true;
    }
}