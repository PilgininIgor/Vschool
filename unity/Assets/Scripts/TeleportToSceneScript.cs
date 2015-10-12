using UnityEngine;

public class TeleportToSceneScript : MonoBehaviour
{
    public string SceneNameRoom;
    public DataStructures.ThemeContent content;
    public int theme_num;
    public int content_num;

    public bool be_ready_to_receive;

    void OnTriggerEnter()
    {
        if (!be_ready_to_receive)
        {
            Global.content = content;
            Global.theme_num = theme_num;
            Global.content_num = content_num;
            Global.teleportBooth = transform;
            PhotonNetwork.LoadLevel(SceneNameRoom);
        }
        else
        {
            be_ready_to_receive = false;
        }
    }
}